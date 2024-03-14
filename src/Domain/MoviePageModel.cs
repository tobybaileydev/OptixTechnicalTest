namespace OptixTechnicalTest.Domain;

public class MoviePageModel
{
    public List<MovieDataModel> MovieDataModels { get; set; } = [];
    public SearchRecordsModel SearchRecordsModel { get; set; } = new();
    public SearchTitleModel SearchTitleModel { get; set; } = new();
    public int MaxRecords { get; set; }
    public bool HadInitialSearch { get; set; }
}
