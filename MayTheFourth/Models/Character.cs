namespace MayTheFourth.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Height { get; set; }
    public decimal Mass { get; set; }
    public string HairColor { get; set; } = string.Empty;
    public string SkinColor { get; set; } = string.Empty;
    public string EyeColor { get; set; } = string.Empty;
    public string BirthYear { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public List<Movie> Movies { get; set; } = new();
}