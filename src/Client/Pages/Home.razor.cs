namespace OptixTechnicalTest.Client.Pages;

public partial class Home
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Inject]
    private IValidationHelper ValidationHelper { get; set; }
    [Inject]
    private IHomePageService HomePageService { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private bool _loading;
    private bool _searchLoading;
    private MoviePageModel _model = new();

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        _model = await HomePageService.InitializeAsync();
        _loading = false;
    }

    private async Task SearchOnClickAsync()
    {
        _searchLoading = true;
        _model = await HomePageService.GetMoviesAsync(_model.SearchTitleModel.SearchTitle, _model.SearchRecordsModel.SearchRecords);
        _searchLoading = false;
    }

    private async Task SearchTitleOnChangeAsync(string searchTitle)
    {
        _model.SearchTitleModel = await ValidationHelper.ValidateSearchTitleAsync(searchTitle);
    }

    private async Task SearchRecordsOnChangeAsync(int searchRecords)
    {
        _model.SearchRecordsModel = await ValidationHelper.ValidateSearchRecordsAsync(searchRecords);
    }
}
