using System.Collections.Generic;

public class Client
{
    public iPlayable CurrentlyPlaying { get; set; }
    public int CurrentTime { get; set; }
    public bool Playing { get; set; }
    public bool Shuffle { get; set; }
    public bool Repeat { get; set; }
    public SuperUser ActiveUser { get; set; }
    public List<Album> AllAlbums { get; set; }
    public List<Song> AllSongs { get; set; }
    public List<Person> AllUsers { get; set; }

    public Client(List<Person> users, List<Album> albums, List<Song> songs) { }
    public void SetActiveUser(Person person) { }
    public void ShowAllAlbums() { }
    public void SelectAlbum(int index) { }
    public void ShowAllSongs() { }
    public void SelectSong(int index) { }
    public void ShowAllUsers() { }
    public void SelectUser(int index) { }
    public void ShowUserPlaylists() { }
    public void SelectUserPlaylist(int index) { }
    public void Play() { }
    public void Pause() { }
    public void Stop() { }
    public void NextSong() { }
    public void SetShuffle(bool shuffle) { }
    public void SetRepeat(bool repeat) { }
    public void CreatePlaylist(string title)
    {
        if (ActiveUser != null)
        {
            ActiveUser.CreatePlaylist(title);
                Console.WriteLine($"Afspeellijst '{title}' aangemaakt.");
        }
    }

    public void SelectPlaylist(int index) { }
    public void RemovePlaylist(int index) { }
    public void AddToPlaylist(int index) { }
    public void ShowSongsInPlaylist(int index) { }
    public void RemoveFromPlaylist(int index) { }
    public void ShowFriends() { }
    public void SelectFriend(int index) { }
    public void AddFriend(int index) { }
    public void RemoveFriend(int index) { }
}
