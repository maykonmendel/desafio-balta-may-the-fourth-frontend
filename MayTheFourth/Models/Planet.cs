namespace MayTheFourth.Models;

public class Planet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RotationPeriod { get; set; } = string.Empty;
    public int OrbitalPeriod { get; set; } 
    public string Diameter { get; set; } = string.Empty;
    public string Climate { get; set; } = string.Empty;
    public string Gravity { get; set; } = string.Empty;
    public string Terrain { get; set; } = string.Empty;
    public string SurfaceWater { get; set; } = string.Empty;
    public string Population { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<Character> Characters { get; set; } = new();
    public List<Movie> Movies { get; set; } = new();
}