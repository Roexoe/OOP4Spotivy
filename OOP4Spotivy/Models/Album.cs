using System.Collections.Generic;
using OOP4Spotivy; // Of de namespace waar Album in staat


public class Album : SongCollection
{
    public List<Artist> Artists { get; set; }

    public Album(List<Artist> artists, string title, List<Song> songs) : base(title)
    {
        Artists = artists;
        if (songs != null)
        {
            foreach (var song in songs)
            {
                playables.Add(song);
            }
        }
    }

    public List<Artist> ShowArtists()
    {
        return Artists;
    }

    public override string ToString()
    {
        return $"{Title} (Album)";
    }
}
