using System.Collections.Generic;

public class Song : iPlayable
{
    public string Title { get; set; }
    public List<Artist> Artists { get; set; }
    public Genres SongGenre { get; set; }
    public int Duur { get; set; }

    public Song(string title, List<Artist> artists, int duur, Genres genre)
    {
        Title = title;
        Artists = artists;
        Duur = duur;
        SongGenre = genre;
    }

    public void Play()
    {
        string artiesten = Artists != null && Artists.Count > 0
            ? string.Join(", ", Artists.ConvertAll(a => a.Naam))
            : "Onbekend";
        Console.WriteLine($"Speelt nu af: '{Title}' - Artiest(en): {artiesten} - Genre: {SongGenre} - Duur: {Duur} seconden");
    }



    public void Pause() { }
    public void Next() { }
    public void Stop() { }
    public int Length => Duur;

    public override string ToString()
    {
        return Title;
    }
}
