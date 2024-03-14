namespace OptixTechnicalTest.Integration.Tests.Utilities;

internal class TestConstants
{
    internal const string ServerName = @"(local)\TEST";
    internal const string DatabaseName = "OptixTechnicalTestDatabase";
    internal const string ServerConnectionString = $"Server={ServerName};Trusted_Connection=True;";
    internal const string DatabaseConnectionString = $"Server={ServerName};Database={DatabaseName};Trusted_Connection=True;";
    internal const string DatabaseSchemaScriptPath = @"..\..\..\..\..\database\Schema.sql";
    internal const string SeedDataScriptPath = @"..\..\..\..\..\database\mymoviedb.csv";
}
