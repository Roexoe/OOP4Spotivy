using System.Collections.Generic;

public class Album : SongCollection
{
    public List<Artist> Artists { get; set; }

    public Album(List<Artist> artists, string title, List<Song> songs) : base(title) { }
    public List<Artist> ShowArtists() { return null; }
    public override string ToString() => base.ToString();
}
