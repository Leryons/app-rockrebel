﻿namespace RockRebel.ViewModels;

public partial class SongViewModel : ObservableObject
{
    private readonly SongServices _songServices;

    // Lists
    public ObservableCollection<Song> Songs { get; set; } = [];
    public ObservableCollection<string> Genres { get; set; } = [];

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _selectedGenre;

    [ObservableProperty]
    private string _popSong;

    public SongViewModel(SongServices songServices)
    {
        this._songServices = songServices;
        LoadSongs();
        FavoriteSong();
        _songServices.PutSongsOnBdd();
    }

    private async void LoadSongs()
    {
        var songs = await _songServices.GetSongs();

        if (songs != null)
        {
            foreach (var song in songs)
            {
                Songs.Add(song);
            }

            var genres = songs.Select(s => s.Genre).Distinct().ToList();
            foreach (var genre in genres)
            {
                Genres.Add(genre);
                Debug.WriteLine(genre);
            }
        }
    }

    partial void OnSelectedGenreChanged(string value)
    {
        Debug.WriteLine($"Selected genre changed to: {value}");
        FilterSongsCommand.Execute(value);
    }

    [RelayCommand]
    public async Task FilterSongs(string genre)
    {
        if (string.IsNullOrEmpty(genre))
        {
            Debug.WriteLine("No genre selected.");
            return;
        }

        Debug.WriteLine($"Filtering songs by genre: {genre}");

        var filteredSongs = await _songServices.FilteredSong(genre);

        if (filteredSongs != null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Songs.Clear();

                foreach (var song in filteredSongs)
                {
                    Songs.Add(song);
                    Debug.WriteLine($"Added song: {song.Title}");
                }

                Debug.WriteLine("Filtered songs list:");
                foreach (var song in Songs)
                {
                    Debug.WriteLine($"{song.Title} - {song.Genre}");
                }
            });
        }
        else
        {
            Debug.WriteLine("No filtered songs found.");
        }
    }

    [RelayCommand]
    public async Task AddLike(string songTitle)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            Debug.WriteLine($"Adding like to song with ID: {songTitle}");
            await _songServices.AddLike(songTitle);
        });
    }

    private async void FavoriteSong()
    {
        var song = await _songServices.PopularSong();
        if (song != null)
        {
            _popSong = song.Title;
            Debug.WriteLine($"Most Popular song is {_popSong}");
        }
    }

    [RelayCommand]
    private async void RefreshData()
    {
        if (IsBusy)
        {
            Debug.WriteLine("Already refreshing data.");  
            return;
        }
        try
        {
            IsBusy = true;

            _songService.RefreshList();
        }
        finally
        {
            IsBusy = false;
        }

    }
}