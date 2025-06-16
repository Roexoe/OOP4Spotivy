using System.Collections.Generic;

public class Playlist : SongCollection
{
    public Person Owner { get; set; }

    public Playlist(Person owner, string title) : base(title)
    {
        Owner = owner;
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
            Console.WriteLine("Deze afspeellijst bevat geen liedjes.");
            return;
        }
        for (int i = 0; i < playables.Count; i++)
        {
            Console.WriteLine($"Nu aan het afspelen: {playables[i]}");
            playables[i].Play();
            // Stop na het eerste nummer, zodat bediening via menu kan
            break;
        }
    }

    public void PlayShuffled()
    {
        if (playables.Count == 0)
        {
            Console.WriteLine("Deze afspeellijst bevat geen liedjes.");
            return;
        }
        var rnd = new System.Random();
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

    public override string ToString() => $"{Title} (Owner: {Owner.Naam}, {playables.Count} items)";
}
