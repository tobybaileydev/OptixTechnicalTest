using System.Globalization;

namespace OptixTechnicalTest.Integration.Tests.Utilities;

internal class InitializeHelper
{
    internal void RebuildDatabase()
    {
        DropDatabase(TestConstants.ServerConnectionString, TestConstants.DatabaseName);
        CreateDatabase(TestConstants.ServerConnectionString, TestConstants.DatabaseName);
        RunScripts(TestConstants.DatabaseConnectionString, TestConstants.DatabaseSchemaScriptPath);
    }

    private void DropDatabase(string connectionString, string databaseName)
    {
        var script =
            @$"IF DB_ID('{databaseName}') IS NOT NULL
            BEGIN
                ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            	DROP DATABASE [{databaseName}];
            END";
        var connection = new SqlConnection(connectionString);
        var server = new Microsoft.SqlServer.Management.Smo.Server(new ServerConnection(connection));
        server.ConnectionContext.ExecuteNonQuery(script);
    }

    private void CreateDatabase(string connectionString, string databaseName)
    {
        var script =
            @$"IF DB_ID('{databaseName}') IS NULL
            BEGIN
            	CREATE DATABASE [{databaseName}];
            END";
        var connection = new SqlConnection(connectionString);
        var server = new Microsoft.SqlServer.Management.Smo.Server(new ServerConnection(connection));
        server.ConnectionContext.ExecuteNonQuery(script);
    }

    private void RunScripts(string connectionString, string scriptPath)
    {
        var fileInfo = new FileInfo(scriptPath);
        var script = fileInfo.OpenText().ReadToEnd();
        var connection = new SqlConnection(connectionString);
        var server = new Microsoft.SqlServer.Management.Smo.Server(new ServerConnection(connection));
        server.ConnectionContext.ExecuteNonQuery(script);
    }

    internal void PopulateDatabase()
    {
        var sql = @"INSERT INTO [Movie] ([Release_Date], [Title], [Overview], [Popularity], [Vote_Count], [Vote_Average], [Original_Language], [Genre], [Poster_Url])
                    VALUES (@Release_Date, @Title, @Overview, @Popularity, @Vote_Count, @Vote_Average, @Original_Language, @Genre, @Poster_Url);";

        var sqlConnection = new SqlConnection(TestConstants.DatabaseConnectionString);        

        try
        {
            using (TextFieldParser csvParser = new TextFieldParser(TestConstants.SeedDataScriptPath))
            {
                csvParser.SetDelimiters([","]);
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    var fields = csvParser.ReadFields();
                    var bob = csvParser.LineNumber;

                    if (fields == null)
                    {
                        throw new ArgumentNullException(nameof(fields));
                    }

                    if (fields.Length != 9)
                    {
                        throw new InvalidDataException($"There should have been 9 fields but there were {fields.Length} on Line {csvParser.LineNumber}");
                    }

                    var dataModel = new MovieDataModel
                    {
                        Release_Date = DateTime.ParseExact(fields[0], "yyyy-MM-dd", CultureInfo.CurrentCulture),
                        Title = fields[1],
                        Overview = fields[2],
                        Popularity = Convert.ToDecimal(fields[3]),
                        Vote_Count = Convert.ToInt32(fields[4]),
                        Vote_Average = Convert.ToDecimal(fields[5]),
                        Original_Language = fields[6],
                        Genre = fields[7],
                        Poster_Url = fields[8]
                    };

                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Release_Date", dataModel.Release_Date);
                    dynamicParameters.Add("@Title", dataModel.Title);
                    dynamicParameters.Add("@Overview", dataModel.Overview);
                    dynamicParameters.Add("@Popularity", dataModel.Popularity);
                    dynamicParameters.Add("@Vote_Count", dataModel.Vote_Count);
                    dynamicParameters.Add("@Vote_Average", dataModel.Vote_Average);
                    dynamicParameters.Add("@Original_Language", dataModel.Original_Language);
                    dynamicParameters.Add("@Genre", dataModel.Genre);
                    dynamicParameters.Add("@Poster_Url", dataModel.Poster_Url);

                    try
                    {
                        sqlConnection.Open();
                        sqlConnection.Execute(sql, dynamicParameters);
                        sqlConnection.Close();
                    }
                    catch (Exception ex)
                    {

                        throw new Exception($"Error on Line {csvParser.LineNumber}. Exception was {ex}");
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }        
    }
}
