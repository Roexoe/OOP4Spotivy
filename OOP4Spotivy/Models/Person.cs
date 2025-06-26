using System.Collections.Generic;

public class Person
{
    public string Name { get; set; }
    public List<Person> Friends { get; set; }
    public List<Playlist> Playlists { get; set; }

    public Person(string name)
    {
        Name = name;
        Friends = new List<Person>();
        Playlists = new List<Playlist>();
    }

    /// <summary>
    /// Returns the list of friends for this person.
    /// </summary>
    public List<Person> ShowFriends()
    {
        return Friends;
    }

    /// <summary>
    /// Returns the list of playlists for this person.
    /// </summary>
    public List<Playlist> ShowPlaylists()
    {
        return Playlists;
    }

    /// <summary>
    /// Selects a playlist by index.
    /// </summary>
    public Playlist SelectPlaylist(int index)
    {
        if (index >= 0 && index < Playlists.Count)
            return Playlists[index];
        return new Playlist(this, "Default Playlist"); // Return a non-null default value
    }

    /// <summary>
    /// Creates a new playlist with the given title and adds it to the user's playlists.
    /// </summary>
    public Playlist CreatePlaylist(string title)
    {
        var playlist = new Playlist(this, title);
        Playlists.Add(playlist);
        return playlist;
    }

    public override string ToString()
    {
        return Name;
    }
}
