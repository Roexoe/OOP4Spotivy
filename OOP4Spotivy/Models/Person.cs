using System.Collections.Generic;

public class Person
{
    public string Naam { get; set; }
    public List<Person> Friends { get; set; }
    public List<Playlist> Playlists { get; set; }

    public Person(string naam)
    {
        Naam = naam;
        Friends = new List<Person>();
        Playlists = new List<Playlist>();
    }

    public List<Person> ShowFriends()
    {
        return Friends;
    }

    public List<Playlist> ShowPlaylists()
    {
        return Playlists;
    }

    public Playlist SelectPlaylist(int index)
    {
        if (index >= 0 && index < Playlists.Count)
            return Playlists[index];
        return null;
    }

    public override string ToString()
    {
        return Naam;
    }
}
