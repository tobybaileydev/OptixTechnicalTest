namespace OptixTechnicalTest.Client.Helpers;

public interface IValidationHelper
{
    Task<SearchTitleModel> ValidateSearchTitleAsync(string searchTitle);
    Task<SearchRecordsModel> ValidateSearchRecordsAsync(int searchRecords);
}
