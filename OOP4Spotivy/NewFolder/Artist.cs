using System.Collections.Generic;

public class Artist
{
    public string Naam { get; set; }
    public List<Album> Albums { get; set; }
    public List<Song> Songs { get; set; }

    public Artist(string naam, List<Album> albums)
    {
        Naam = naam;
        Albums = albums ?? new List<Album>();
        Songs = new List<Song>();
    }

    public void AddSong(Song song)
    {
        Songs.Add(song);
    }

    public void AddAlbum(Album album)
    {
        Albums.Add(album);
    }

    public override string ToString()
    {
        return Naam;
    }
}
