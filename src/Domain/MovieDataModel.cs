namespace OptixTechnicalTest.Domain;

public class MovieDataModel
{
    public DateTime Release_Date { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public decimal Popularity { get; set; }
    public int Vote_Count { get; set; }
    public decimal Vote_Average { get; set; }
    public string Original_Language { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = [];
    public string Poster_Url { get; set; } = string.Empty;
}
