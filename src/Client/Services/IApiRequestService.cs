namespace OptixTechnicalTest.Client.Services;

public interface IApiRequestService
{
    Task<int> GetMaxRecordsAsync();
    Task<List<MovieDataModel>> GetMovieDataModelsAsync(string searchTitle, int searchRecords);
}
