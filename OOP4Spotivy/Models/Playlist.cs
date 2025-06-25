using System;
using System.Collections.Generic;
using System.Threading;

public class Playlist : SongCollection
{
    public Person Owner { get; set; }
    public bool Repeat { get; set; } = false;

    public Playlist(Person owner, string title) : base(title)
    {
        Owner = owner;
    }

    /// <summary>
    /// Adds a playable item (song or other) to the playlist.
    /// </summary>
    public void Add(iPlayable playable)
    {
        playables.Add(playable);
    }

    /// <summary>
    /// Removes a playable item from the playlist.
    /// </summary>
    public void Remove(iPlayable playable)
    {
        playables.Remove(playable);
    }

    public override string ToString() => $"{Title} (Owner: {Owner.Naam}, {playables.Count} items)";

    /// <summary>
    /// Plays all songs in the playlist in order. If Repeat is true, repeats the playlist.
    /// </summary>
    public void Play()
    {
        if (playables.Count == 0)
        {
            Console.WriteLine("Deze afspeellijst bevat geen liedjes.");
            return;
        }

        Thread playlistThread = new Thread(() =>
        {
            do
            {
                foreach (var playable in playables)
                {
                    if (playable is Song song)
                    {
                        song.Stop(); // Reset song state
                        song.Play();
                        // Wait until the song is finished
                        while (song.Length > 0)
                        {
                            Thread.Sleep(500);
                        }
                    }
                }
                if (Repeat)
                {
                    Console.WriteLine($"Afspeellijst '{Title}' wordt herhaald.");
                }
            } while (Repeat);
            Console.WriteLine($"Afspeellijst '{Title}' is afgelopen.");
        });
        playlistThread.IsBackground = true;
        playlistThread.Start();
    }
}

