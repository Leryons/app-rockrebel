namespace RockRebel.Services;

public class Database
{
    private readonly SQLiteConnection sQLite;

    public Database()
    {
        try
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Songs.db");
            sQLite = new SQLiteConnection(path);
            sQLite.CreateTable<Song>();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    public SQLiteConnection sQLiteConnection => sQLite;


    public async Task<List<Song>> GetSongs()
    {
        return sQLite.Table<Song>().ToList();
    }

    public async Task<List<Song>> FilteredSong(string genre)
    {
        return sQLite.Table<Song>()
                           .Where(s => s.Genre == genre)
                           .ToList();
    }

    public async Task AddLike(string songTitle)
    {
        try
        {
            var song = sQLite.Table<Song>().Where(s => s.Title == songTitle).FirstOrDefault();

            if (song != null)
            {
                song.Likes++;

                sQLite.Update(song);

                Debug.WriteLine($"Likes incremented for song: {song.Title}");

                var updatedSong = sQLite.Table<Song>().Where(s => s.Title == songTitle).FirstOrDefault();
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

    public async Task<Song> PopularSong()
    {
        var popularSong = sQLite.Table<Song>().OrderByDescending(s => s.Likes).FirstOrDefault();

        return popularSong;
    }
}