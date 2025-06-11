using System.Collections.Generic;

public class Person
{
    public string Naam { get; set; }
    public List<Person> Friends { get; set; }
    public List<Playlist> Playlists { get; set; }

    public Person(string naam) { }
    public List<Person> ShowFriends() { return null; }
    public List<Playlist> ShowPlaylists() { return null; }
    public Playlist SelectPlaylist(int index) { return null; }
    public override string ToString() => base.ToString();
}
