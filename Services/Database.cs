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

    public async Task AddLike(string songTitle)
    {
        try
        {
            var song = await sQLite.Table<Song>().Where(s => s.Title == songTitle).FirstOrDefaultAsync();

            if (song != null)
            {
                song.Likes++;

                await sQLite.UpdateAsync(song);

                Debug.WriteLine($"Likes incremented for song: {song.Title}");

                var updatedSong = await sQLite.Table<Song>().Where(s => s.Title == songTitle).FirstOrDefaultAsync();
                if (updatedSong != null && updatedSong.Likes == song.Likes)
                {
                    Debug.WriteLine("Update confirmed.");
                }
                else
                {
                    Debug.WriteLine("Update not confirmed.");

                }
            }
            else
            {
                Debug.WriteLine("Song not found.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating likes: {ex.Message}");
        }
    }
}
