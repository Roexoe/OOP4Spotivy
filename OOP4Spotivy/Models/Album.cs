using System.Collections.Generic;

public class Album : SongCollection
{
    public List<Artist> Artists { get; set; }
    public List<Song> Songs { get; }

    public Album(List<Artist> artists, string title, List<Song> songs) : base(title)
    {
        Artists = artists;
        Songs = songs;
    }

    public List<Artist> ShowArtists() => Artists;
    public List<Song> ShowSongs() => Songs;

    public override string ToString() => $"{Title} ({Artists.Count} artists, {Songs.Count} songs)";
}
