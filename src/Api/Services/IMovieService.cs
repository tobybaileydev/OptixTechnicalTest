namespace OptixTechnicalTest.Services;

public interface IMovieService
{
    Task<List<MovieDataModel>> GetMoviesByTitleAsync(string title, int resultsCount);
}
