namespace RockRebel.Services;

public class SongServices
{
    private List<Song> songs;

    public SongServices()
    {
        songs = [];
    }

    public async Task<List<Song>?> GetSongs()
    {
        try
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "SongList.json");

            if (File.Exists(jsonPath))
            {
                var jsonContent = await File.ReadAllTextAsync(jsonPath);

                var deserializedSongs = JsonConvert.DeserializeObject<List<Song>>(jsonContent);

                if (deserializedSongs != null)
                {
                    songs = deserializedSongs;
                }

                //foreach (var song in songs)
                //{
                //    Debug.WriteLine($"Title: {song.Title}");
                //    Debug.WriteLine($"Artist: {song.Artist}");
                //    Debug.WriteLine($"Genre: {song.Genre}");
                //    Debug.WriteLine($"Year: {song.Year}");
                //    Debug.WriteLine("");
                //}
            }

            else
            {
                Debug.WriteLine("File not found.");
            }

            return songs;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}
