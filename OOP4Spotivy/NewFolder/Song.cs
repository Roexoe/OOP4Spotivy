using System.Collections.Generic;

public class Song
{
    public string Title { get; set; }
    public List<Artist> Artists { get; set; }
    public Genres SongGenre { get; set; }
    public int Duur { get; set; }

    public Song(string title, List<Artist> artists, int duur, Genres genre) { }
    public override string ToString() => base.ToString();
}
