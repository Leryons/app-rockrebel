namespace RockRebel.Services;

public class SongServices
{
    private List<Song> songs;
    private readonly Database database;

    public SongServices(Database database)
    {
        songs = [];
        this.database = database;
    }

    public async Task<List<Song>?> GetSongs()
    {
        try
        {
            songs = await database.GetSongs();

            return songs;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null;
        }

    }

    public async Task<List<Song>?> FilteredSong(string genre)
    {
        try
        {
            return await database.FilteredSong(genre);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null;
        }
    }

    public async Task AddLike(string title)
    {
        try
        {
            await database.AddLike(title);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    public async Task<Song> PopularSong()
    {
        return await database.PopularSong();
    }

}
