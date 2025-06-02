using System.Collections.Generic;

public class Artist
{
    public string Naam { get; set; }
    public List<Album> Albums { get; set; }
    public List<Song> Songs { get; set; }

    public Artist(string naam, List<Album> albums) { }
    public void AddSong(Song song) { }
    public void AddAlbum(Album album) { }
    public override string ToString() => base.ToString();
}
