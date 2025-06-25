using System.Collections.Generic;

public class Artist
{
    public string Naam { get; set; }
    public List<Album> Albums { get; set; }
    public List<Song> Songs { get; set; }

    public Artist(string naam, List<Album> albums)
    {
        Naam = naam;
        Albums = albums;
        Songs = new List<Song>();
    }

    /// <summary>
    /// Adds a song to the artist's list of songs.
    /// </summary>
    public void AddSong(Song song)
    {
        Songs.Add(song);
    }

    /// <summary>
    /// Adds an album to the artist's list of albums.
    /// </summary>
    public void AddAlbum(Album album)
    {
        Albums.Add(album);
    }

    public override string ToString()
    {
        return Naam;
    }
}