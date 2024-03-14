namespace OptixTechnicalTest.Helpers;

public interface IDataHelper
{
    Task<List<MovieDataModel>> GetTopResultsByLikeTitleAsync(string title, int resultsCount);
}
