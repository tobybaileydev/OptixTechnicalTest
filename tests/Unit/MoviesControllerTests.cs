namespace OptixTechnicalTest.Unit.Tests;

[TestClass]
public class MoviesControllerTests
{
    private readonly IMovieService movieService;
    private readonly IOptions<AppConfig> options;
    public MoviesControllerTests()
    {
        movieService = Substitute.For<IMovieService>();
        options = Options.Create(new AppConfig
        {
            MaxSearchResults = 10
        });
    }

    private MoviesController CreateSut => new MoviesController(movieService, options);

    [TestMethod]
    public async Task GetByTitleAsync_ServiceReturnsSuccessfully_ReturnsOk()
    {
        var sut = CreateSut;

        movieService.GetMoviesByTitleAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(Task.FromResult(new List<MovieDataModel>
            {
                new() {
                    Title = "Test Title One"
                },
                new() {
                    Title = "Test Title Two"
                }
            }));

        var response = await sut.GetByTitleAsync("Title", 1);

        Assert.IsNotNull(response);
        var objectResult = response as ObjectResult;
        Assert.IsNotNull(objectResult);
        objectResult.StatusCode.Should().Be(200);
        var objectResultValue = objectResult.Value;
        Assert.IsNotNull(objectResultValue);
        var movies = objectResultValue as List<MovieDataModel>;
        Assert.IsNotNull(movies);
        movies.Should().HaveCount(2);
        movies[0].Title.Should().Be("Test Title One");
        movies[1].Title.Should().Be("Test Title Two");

        await movieService.Received(1).GetMoviesByTitleAsync("Title", 1);
    }

    [TestMethod]
    public async Task GetByTitleAsync_ServiceThrowsError_ReturnsBadRequest()
    {
        var sut = CreateSut;

        movieService.GetMoviesByTitleAsync(Arg.Any<string>(), Arg.Any<int>())
            .Throws(new Exception("Something broke"));

        var response = await sut.GetByTitleAsync("Title", 1);

        Assert.IsNotNull(response);
        var objectResult = response as ObjectResult;
        Assert.IsNotNull(objectResult);
        objectResult.StatusCode.Should().Be(400);
        var objectResultValue = objectResult.Value;
        Assert.IsNotNull(objectResultValue);
        var message = objectResultValue as string;
        Assert.IsNotNull(message);
        message.Should().Contain("Something broke");

        await movieService.Received(1).GetMoviesByTitleAsync("Title", 1);
    }

    [TestMethod]
    public async Task GetMaxRecordsAsync_ReturnsMaxRecords()
    {
        var sut = CreateSut;

        var response = await sut.GetMaxRecordsAsync();

        Assert.IsNotNull(response);
        var objectResult = response as ObjectResult;
        Assert.IsNotNull(objectResult);
        objectResult.StatusCode.Should().Be(200);
        var objectResultValue = objectResult.Value;
        Assert.IsNotNull(objectResultValue);
        var maxRecords = objectResultValue as int?;
        Assert.IsNotNull(maxRecords);
        maxRecords.Should().Be(10);
    }
}