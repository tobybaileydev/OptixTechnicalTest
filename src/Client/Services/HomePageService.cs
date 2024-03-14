namespace OptixTechnicalTest.Client.Services;

public class HomePageService(
    IValidationHelper validationHelper,
    IApiRequestService apiRequestService
    ) : IHomePageService
{
    public async Task<MoviePageModel> InitializeAsync()
    {
        return new MoviePageModel
        {
            SearchRecordsModel = new SearchRecordsModel
            {
                SearchRecords = 10
            },
            MaxRecords = await apiRequestService.GetMaxRecordsAsync()
        };
    }

    public async Task<MoviePageModel> GetMoviesAsync(string searchTitle, int searchRecords)
    {
        var moviePageModel = new MoviePageModel
        {
            SearchTitleModel = await validationHelper.ValidateSearchTitleAsync(searchTitle),
            SearchRecordsModel = await validationHelper.ValidateSearchRecordsAsync(searchRecords)
        };

        if (moviePageModel.SearchTitleModel.Invalid || moviePageModel.SearchRecordsModel.Invalid)
        {
            return moviePageModel;
        }

        moviePageModel.MovieDataModels = await apiRequestService.GetMovieDataModelsAsync(searchTitle, searchRecords);
        moviePageModel.HadInitialSearch = true;

        return moviePageModel;
    }
}
