namespace OptixTechnicalTest.Client.Helpers;

public class ValidationHelper : IValidationHelper
{
    public async Task<SearchTitleModel> ValidateSearchTitleAsync(string searchTitle)
    {
        var searchTitleModel = new SearchTitleModel
        {
            SearchTitle = searchTitle
        };

        if (string.IsNullOrEmpty(searchTitle))
        {
            searchTitleModel.Invalid = true;
            searchTitleModel.Message = "Insert a Movie Title";
        }

        return searchTitleModel;
    }

    public async Task<SearchRecordsModel> ValidateSearchRecordsAsync(int searchRecords)
    {
        var searchRecordsModel = new SearchRecordsModel
        {
            SearchRecords = searchRecords
        };

        if (searchRecords <= 0)
        {
            searchRecordsModel.Invalid = true;
            searchRecordsModel.Message = "Maximum Movies Returned must be above zero";
        }

        return searchRecordsModel;
    }
}
