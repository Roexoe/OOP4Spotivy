using System;
using System.Collections.Generic;

/// <summary>
/// Represents the main client for the Spotivy application, managing users, albums, songs, and playback state.
/// </summary>
public class Client
{
    /// <summary>
    /// Gets or sets the currently playing item.
    /// </summary>
    public iPlayable CurrentlyPlaying { get; set; }

    /// <summary>
    /// Gets or sets the current playback time (in seconds).
    /// </summary>
    public int CurrentTime { get; set; }

    /// <summary>
    /// Gets or sets whether playback is active.
    /// </summary>
    public bool Playing { get; set; }

    /// <summary>
    /// Gets or sets whether shuffle mode is enabled.
    /// </summary>
    public bool Shuffle { get; set; }

    /// <summary>
    /// Gets or sets whether repeat mode is enabled.
    /// </summary>
    public bool Repeat { get; set; }

    /// <summary>
    /// Gets or sets the active super user.
    /// </summary>
    public SuperUser ActiveUser { get; set; }

    /// <summary>
    /// Gets or sets the list of all albums.
    /// </summary>
    public List<Album> AllAlbums { get; set; }

    /// <summary>
    /// Gets or sets the list of all songs.
    /// </summary>
    public List<Song> AllSongs { get; set; }

    /// <summary>
    /// Gets or sets the list of all users.
    /// </summary>
    public List<Person> AllUsers { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Client"/> class.
    /// </summary>
    /// <param name="users">The list of users.</param>
    /// <param name="albums">The list of albums.</param>
    /// <param name="songs">The list of songs.</param>
    public Client(List<Person> users, List<Album> albums, List<Song> songs)
    {
        AllUsers = users;
        AllAlbums = albums;
        AllSongs = songs;
    }

    /// <summary>
    /// Sets the active user.
    /// </summary>
    /// <param name="person">The person to set as active user.</param>
    public void SetActiveUser(Person person) { }

    /// <summary>
    /// Shows all albums.
    /// </summary>
    public void ShowAllAlbums() { }

    /// <summary>
    /// Selects an album by index.
    /// </summary>
    /// <param name="index">The index of the album.</param>
    public void SelectAlbum(int index) { }

    /// <summary>
    /// Shows all songs.
    /// </summary>
    public void ShowAllSongs() { }

    /// <summary>
    /// Selects a song by index.
    /// </summary>
    /// <param name="index">The index of the song.</param>
    public void SelectSong(int index) { }

    /// <summary>
    /// Shows all users.
    /// </summary>
    public void ShowAllUsers() { }

    /// <summary>
    /// Selects a user by index.
    /// </summary>
    /// <param name="index">The index of the user.</param>
    public void SelectUser(int index) { }

    /// <summary>
    /// Shows all playlists of the active user.
    /// </summary>
    public void ShowUserPlaylists() { }

    /// <summary>
    /// Selects a playlist of the active user by index.
    /// </summary>
    /// <param name="index">The index of the playlist.</param>
    public void SelectUserPlaylist(int index) { }

    /// <summary>
    /// Starts playback.
    /// </summary>
    public void Play() { }

    /// <summary>
    /// Pauses playback.
    /// </summary>
    public void Pause() { }

    /// <summary>
    /// Stops playback.
    /// </summary>
    public void Stop() { }

    /// <summary>
    /// Skips to the next song.
    /// </summary>
    public void NextSong() { }

    /// <summary>
    /// Sets shuffle mode.
    /// </summary>
    /// <param name="shuffle">True to enable shuffle, false to disable.</param>
    public void SetShuffle(bool shuffle) { }

    /// <summary>
    /// Sets repeat mode.
    /// </summary>
    /// <param name="repeat">True to enable repeat, false to disable.</param>
    public void SetRepeat(bool repeat) { }

    /// <summary>
    /// Creates a new playlist for the active user.
    /// </summary>
    /// <param name="title">The title of the new playlist.</param>
    public void CreatePlaylist(string title)
    {
        if (ActiveUser != null)
        {
            ActiveUser.CreatePlaylist(title);
            Console.WriteLine($"Afspeellijst '{title}' aangemaakt.");
        }
    }

    /// <summary>
    /// Selects a playlist by index.
    /// </summary>
    /// <param name="index">The index of the playlist.</param>
    public void SelectPlaylist(int index) { }

    /// <summary>
    /// Removes a playlist by index.
    /// </summary>
    /// <param name="index">The index of the playlist to remove.</param>
    public void RemovePlaylist(int index) { }

    /// <summary>
    /// Adds a song to a playlist by index.
    /// </summary>
    /// <param name="index">The index of the playlist.</param>
    public void AddToPlaylist(int index) { }

    /// <summary>
    /// Shows songs in a playlist by index.
    /// </summary>
    /// <param name="index">The index of the playlist.</param>
    public void ShowSongsInPlaylist(int index) { }

    /// <summary>
    /// Removes a song from a playlist by index.
    /// </summary>
    /// <param name="index">The index of the playlist.</param>
    public void RemoveFromPlaylist(int index) { }

    /// <summary>
    /// Shows all friends of the active user.
    /// </summary>
    public void ShowFriends() { }

    /// <summary>
    /// Selects a friend by index.
    /// </summary>
    /// <param name="index">The index of the friend.</param>
    public void SelectFriend(int index) { }

    /// <summary>
    /// Adds a friend by index.
    /// </summary>
    /// <param name="index">The index of the friend to add.</param>
    public void AddFriend(int index) { }

    /// <summary>
    /// Removes a friend by index.
    /// </summary>
    /// <param name="index">The index of the friend to remove.</param>
    public void RemoveFriend(int index) { }
}
