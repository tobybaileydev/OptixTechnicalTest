namespace OptixTechnicalTest.Helpers;

public class DataHelper(
    IOptions<AppConfig> options
    ) : IDataHelper
{
    private SqlConnection Connection => new(options.Value.SqlConnectionString);

    public async Task<List<MovieDataModel>> GetTopResultsByLikeTitleAsync(string title, int resultsCount)
    {
        var sql = @"SELECT TOP(@ResultsCount) *
                    FROM [Movie] (NOLOCK)
                    WHERE [Title] LIKE '%' + @Title + '%'";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@Title", title);
        dynamicParameters.Add("@ResultsCount", resultsCount);

        var connection = Connection;

        await connection.OpenAsync();
        var movies = await connection.QueryAsync<MovieDataModel>(sql, dynamicParameters);
        await connection.CloseAsync();

        return movies.ToList();
    }
}
