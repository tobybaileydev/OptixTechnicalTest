namespace OptixTechnicalTest.Unit.Tests;

[TestClass]
public class MovieServiceTests
{
    private readonly IDataHelper dataHelper;
    private readonly IOptions<AppConfig> options;
    public MovieServiceTests()
    {
        dataHelper = Substitute.For<IDataHelper>();
        options = Options.Create(new AppConfig
        {
            MaxSearchResults = 5
        });
    }

    private IMovieService CreateSut => new MovieService(dataHelper, options);

    [TestMethod]
    public async Task GetMoviesByTitleAsync_BlankTitle_ReturnsBlankMovies()
    {
        var sut = CreateSut;

        var movies = await sut.GetMoviesByTitleAsync("", 1);

        movies.Should().HaveCount(0);
    }

    [TestMethod]
    public async Task GetMoviesByTitleAsync_ZeroResultsCount_ReturnsBlankMovies()
    {
        var sut = CreateSut;

        var movies = await sut.GetMoviesByTitleAsync("Title", 0);

        movies.Should().HaveCount(0);
    }

    [TestMethod]
    public async Task GetMoviesByTitleAsync_ZeroResultsCountAndBlankTitle_ReturnsBlankMovies()
    {
        var sut = CreateSut;

        var movies = await sut.GetMoviesByTitleAsync("", 0);

        movies.Should().HaveCount(0);
    }

    [TestMethod]
    public async Task GetMoviesByTitleAsync_ResultsCountWithLimit_ReturnsMoviesWithUnalteredResultsCount()
    {
        var sut = CreateSut;

        dataHelper.GetTopResultsByLikeTitleAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(Task.FromResult(new List<MovieDataModel>
            {
                new() {
                    Title = "Test Title One",
                },
                new() {
                    Title = "Test Title Two",
                }
            }));

        var movies = await sut.GetMoviesByTitleAsync("Title", 1);

        movies.Should().HaveCount(2);
        movies[0].Title.Should().Be("Test Title One");
        movies[1].Title.Should().Be("Test Title Two");

        await dataHelper.Received(1).GetTopResultsByLikeTitleAsync("Title", 1);
    }

    [TestMethod]
    public async Task GetMoviesByTitleAsync_ResultsCountAboveLimit_ReturnsMoviesWithAlteredResultsCount()
    {
        var sut = CreateSut;

        dataHelper.GetTopResultsByLikeTitleAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(Task.FromResult(new List<MovieDataModel>
            {
                new() {
                    Title = "Test Title One",
                },
                new() {
                    Title = "Test Title Two",
                }
            }));

        var movies = await sut.GetMoviesByTitleAsync("Title", 10);

        movies.Should().HaveCount(2);
        movies[0].Title.Should().Be("Test Title One");
        movies[1].Title.Should().Be("Test Title Two");

        await dataHelper.Received(1).GetTopResultsByLikeTitleAsync("Title", 5);
    }

    [TestMethod]
    public async Task GetMoviesByTitleAsync_ResultsHaveGenereLists_ReturnsMoviesWithSplitGenres()
    {
        var sut = CreateSut;

        dataHelper.GetTopResultsByLikeTitleAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(Task.FromResult(new List<MovieDataModel>
            {
                new() {
                    Title = "Test Title One",
                    Genre = "Genre One"
                },
                new() {
                    Title = "Test Title Two",
                    Genre = "Genre One, Genre Two"
                }
            }));

        var movies = await sut.GetMoviesByTitleAsync("Title", 10);

        movies.Should().HaveCount(2);
        movies[0].Title.Should().Be("Test Title One");
        movies[0].Genres.Should().HaveCount(1);
        movies[0].Genres[0].Should().Be("Genre One");
        movies[1].Title.Should().Be("Test Title Two");
        movies[1].Genres.Should().HaveCount(2);
        movies[1].Genres[0].Should().Be("Genre One");
        movies[1].Genres[1].Should().Be("Genre Two");

        await dataHelper.Received(1).GetTopResultsByLikeTitleAsync("Title", 5);
    }
}
