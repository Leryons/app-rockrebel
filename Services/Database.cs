namespace RockRebel.Services;

public class Database
{
    private readonly SQLiteAsyncConnection sQLite;

    public Database()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Songs.db3");
            sQLite = new SQLiteAsyncConnection(path);
            sQLite.CreateTableAsync<Song>().Wait();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    public SQLiteAsyncConnection sQLiteAsyncConnection => sQLite;

    public async Task<List<Song>> GetSongs()
    {
        return await sQLite.Table<Song>().ToListAsync();
    }

    public async Task<List<Song>> FilteredSong(string genre)
    {
        return await sQLite.Table<Song>()
                           .Where(s => s.Genre == genre)
                           .ToListAsync();
    }

    public async Task<int> SaveSong(Song song)
    {
        if (song.Id != 0)
        {
            return await sQLite.UpdateAsync(song);
        }
        else
        {
            return await sQLite.InsertAsync(song);
        }
    }
}
