namespace RockRebel.Models;

[Table("Song")]
public class Song
{
    [PrimaryKey]
    public string? Title { get; set; }
    public string? Artist { get; set; }
    public string? Genre { get; set; }
    public string? Year { get; set; }
    public int Likes { get; set; }
}
