using System;
using System.Collections.Generic;
using System.Linq;

public class Album : SongCollection, iPlayable
{
    public List<Artist> Artists { get; set; }

    public Album(List<Artist> artists, string title, List<Song> songs) : base(title)
    {
        Artists = artists;
        foreach (var song in songs)
            playables.Add(song);
    }

    public void Add(iPlayable playable)
    {
        playables.Add(playable);
    }

    public void Remove(iPlayable playable)
    {
        playables.Remove(playable);
    }

    public void Play()
    {
        if (playables.Count == 0)
        {
            Console.WriteLine("Dit album bevat geen liedjes.");
            return;
        }
        Console.WriteLine($"Nu aan het afspelen: {playables[0]}");
        playables[0].Play();
        // Stop na het eerste nummer, zodat bediening via menu kan
    }

    public void PlayShuffled()
    {
        if (playables.Count == 0)
        {
            Console.WriteLine("Dit album bevat geen liedjes.");
            return;
        }
        var rnd = new Random();
        var shuffled = new List<iPlayable>(playables);
        int n = shuffled.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            (shuffled[n], shuffled[k]) = (shuffled[k], shuffled[n]);
        }
        Console.WriteLine($"Nu aan het afspelen: {shuffled[0]}");
        shuffled[0].Play();
        // Stop na het eerste nummer, zodat bediening via menu kan
    }

    public void Pause() { /* eventueel implementeren */ }
    public void Next() { /* eventueel implementeren */ }
    public void Stop() { /* eventueel implementeren */ }
    public int Length => playables.Sum(p => p.Length);

    public override string ToString() => $"{Title} (Artists: {string.Join(", ", Artists.Select(a => a.Naam))}, {playables.Count} items)";
}
