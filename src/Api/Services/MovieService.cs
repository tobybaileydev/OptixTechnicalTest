namespace OptixTechnicalTest.Services;

public class MovieService(
    IDataHelper dataHelper,
    IOptions<AppConfig> options
    ) : IMovieService
{
    public async Task<List<MovieDataModel>> GetMoviesByTitleAsync(string title, int resultsCount)
    {
        if (string.IsNullOrEmpty(title) || resultsCount == 0) 
        { 
            return []; 
        }

        if (resultsCount > options.Value.MaxSearchResults)
        {
            resultsCount = options.Value.MaxSearchResults;
        }

        var movies = await dataHelper.GetTopResultsByLikeTitleAsync(title, resultsCount);

        foreach ( var movie in movies )
        {
            movie.Genres = movie.Genre.Split(",").Select(x => x.Trim()).ToList();
            movie.Genre = string.Empty;
        }

        return movies;
    }
}
