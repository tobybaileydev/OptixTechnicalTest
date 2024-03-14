namespace OptixTechnicalTest.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class MoviesController(
    IMovieService movieService,
    IOptions<AppConfig> options) : ControllerBase
{
    [HttpGet]
    [Route("GetByTitleAsync")]
    public async Task<IActionResult> GetByTitleAsync(string title, int resultsCount)
    {
		try
        {
            var movies = await movieService.GetMoviesByTitleAsync(title, resultsCount);
            return Ok(movies);
        }
        catch (Exception ex)
		{
            return BadRequest(ex.ToString());
		}
    }

    [HttpGet]
    [Route("GetMaxRecordsAsync")]
    public async Task<IActionResult> GetMaxRecordsAsync()
    {
        return Ok(options.Value.MaxSearchResults);
    }
}
