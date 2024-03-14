using FluentAssertions;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace OptixTechnicalTest.Integration.Tests;

[TestClass]
public class DataHelperTests
{
    private readonly IOptions<AppConfig> options;
    public DataHelperTests()
    {
        options = Options.Create(new AppConfig
        {
            SqlConnectionString = TestConstants.DatabaseConnectionString
        });
    }

    private IDataHelper CreateSut => new DataHelper(options);

    [TestMethod]
    public async Task GetTopResultsByLikeTitleAsync_SearchForExactTitle_CorrectDataReturned()
    {
        var sut = CreateSut;

        var movies = await sut.GetTopResultsByLikeTitleAsync("Spider-Man: No Way Home", 1000);

        movies.Should().HaveCount(1);
        var movie = movies.First();

        movie.Release_Date.Should().Be(Convert.ToDateTime("15 dec 2021"));
        movie.Title.Should().Be("Spider-Man: No Way Home");
        movie.Overview.Should().Be("Peter Parker is unmasked and no longer able to separate his normal life from the high-stakes of being a super-hero. When he asks for help from Doctor Strange the stakes become even more dangerous, forcing him to discover what it truly means to be Spider-Man.");
        movie.Popularity.Should().Be(5083.954m);
        movie.Vote_Count.Should().Be(8940);
        movie.Vote_Average.Should().Be(8.3m);
        movie.Original_Language.Should().Be("en");
        movie.Genre.Should().Be("Action, Adventure, Science Fiction");
        movie.Poster_Url.Should().Be("https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg");
    }

    [TestMethod]
    public async Task GetTopResultsByLikeTitleAsync_SearchForTitleBelowResultCount_ReturnsAllRecords()
    {
        var sut = CreateSut;

        var movies = await sut.GetTopResultsByLikeTitleAsync("Home", 1000);

        movies.Should().HaveCount(44);
    }

    [TestMethod]
    public async Task GetTopResultsByLikeTitleAsync_SearchForTitleAboveResultCount_ReturnsReducedRecords()
    {
        var sut = CreateSut;

        var movies = await sut.GetTopResultsByLikeTitleAsync("Home", 20);

        movies.Should().HaveCount(20);
    }

    [TestMethod]
    public async Task GetTopResultsByLikeTitleAsync_SearchForNonexistantTitle_ReturnsBlankList()
    {
        var sut = CreateSut;

        var movies = await sut.GetTopResultsByLikeTitleAsync("zazazazaazazazazazazazazaz", 20);

        movies.Should().HaveCount(0);
    }
}