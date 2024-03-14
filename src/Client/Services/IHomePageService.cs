namespace OptixTechnicalTest.Client.Services;

public interface IHomePageService
{
    Task<MoviePageModel> InitializeAsync();
    Task<MoviePageModel> GetMoviesAsync(string searchTitle, int searchRecords);
}
