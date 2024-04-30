namespace MayTheFourth.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Episode { get; set; } = string.Empty;
    public string OpeningCrawl { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Producer { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public List<Character> Characters { get; set; } = new();
    public List<Planet> Planets { get; set; } = new();
    public List<Vehicle> Vehicles { get; set; } = new();
    public List<Starship> Starships { get; set; } = new();
}