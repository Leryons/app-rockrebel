namespace RockRebel.Models;

public class Song
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Artist { get; set; }
    public string? Genre { get; set; }
    public string? Year { get; set; }
    public int Likes { get; set; }
}
