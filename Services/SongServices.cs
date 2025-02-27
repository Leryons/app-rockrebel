﻿namespace RockRebel.Services;

public class SongServices
{
    private List<Song> songs;
    private readonly Database database;
    private readonly HttpClient http;

    public SongServices(Database database)
    {
        songs = [];
        this.database = database;
        http = new HttpClient();
    }

    public async Task<List<Song>> PutSongsOnBdd()
    {
        var url = "https://raw.githubusercontent.com/Leryons/Songs/refs/heads/main/SongsList.json";

        Debug.WriteLine("Fetching songs from: " + url);

        HttpResponseMessage response = await http.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        var jArray = JArray.Parse(responseBody);

        if (songs == null)
        {
            songs = new List<Song>();
        }
        else
        {
            songs.Clear();
        }

        foreach (var prop in jArray)
        {
            var song = new Song
            {
                Title = prop["title"]?.ToString(),
                Artist = prop["artist"]?.ToString(),
                Genre = prop["genre"]?.ToString(),
            };

            if (song.Title != null && song.Artist != null && song.Genre != null)
            {
                songs.Add(song);
                Debug.WriteLine(song.Title + "    NICE");
                try
                {
                    database.sQLiteConnection.InsertOrReplace(song);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error inserting song: " + ex.Message);
                }
            }
            else
            {
                Debug.WriteLine("Invalid song data: " + prop.ToString());
            }
        }

        return songs;
    }


    public async Task<List<Song>?> GetSongs()
    {
        try
        {
            if(songs.Count == 0)
            {
                songs = await database.GetSongs();
            }

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

    public async Task<List<Song>> RefreshList()
    {
        try
        {
            if (songs.Count == 0)
            {
                songs = await database.GetSongs();
            }
            else
            {
                songs.Clear();
                songs = await database.GetSongs();
            }
            return songs;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return new List<Song>(); // Return an empty list in case of an exception
        }
    }
}
