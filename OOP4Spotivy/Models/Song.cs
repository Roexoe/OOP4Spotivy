using System;
using System.Collections.Generic;
using System.Threading;

public class Song : iPlayable
{
    public string Title { get; set; }
    public List<Artist> Artists { get; set; }
    public Genres SongGenre { get; set; }
    public int Duur { get; set; }

    private int _remainingTime;
    private bool _isPaused;
    private bool _isPlaying;
    private Thread? _playThread;

    public Song(string title, List<Artist> artists, int duur, Genres genre)
    {
        Title = title;
        Artists = artists;
        Duur = duur;
        SongGenre = genre;
        _remainingTime = duur;
        _isPaused = false;
        _isPlaying = false;
    }

    public void Play()
    {
        if (_isPlaying)
        {
            if (_isPaused)
            {
                _isPaused = false;
                Console.WriteLine($"Verder met afspelen: '{Title}'");
            }
            else
            {
                Console.WriteLine($"Liedje '{Title}' speelt al.");
            }
            return;
        }

        if (_remainingTime <= 0)
            _remainingTime = Duur;

        _isPaused = false;
        _isPlaying = true;

        string artiesten = Artists != null && Artists.Count > 0
            ? string.Join(", ", Artists.ConvertAll(a => a.Naam))
            : "Onbekend";
        Console.WriteLine($"Start met afspelen: '{Title}' - Artiest(en): {artiesten} - Genre: {SongGenre}");

        _playThread = new Thread(() =>
        {
            while (_remainingTime > 0)
            {
                if (_isPaused)
                {
                    Thread.Sleep(200); // Kort wachten, zodat pauzeren snel werkt
                    continue;
                }
                Thread.Sleep(1000);
                _remainingTime--;
            }
            _isPlaying = false;
            Console.WriteLine($"Liedje '{Title}' is afgelopen.");
        });
        _playThread.IsBackground = true;
        _playThread.Start();
    }

    public void Pause()
    {
        if (_isPlaying && !_isPaused && _remainingTime > 0)
        {
            _isPaused = true;
            Console.WriteLine($"Liedje '{Title}' is gepauzeerd. Nog {_remainingTime} seconden over.");
        }
        else if (_remainingTime == 0)
        {
            Console.WriteLine($"Liedje '{Title}' is al afgelopen.");
        }
        else
        {
            Console.WriteLine("Er wordt momenteel geen nummer afgespeeld of het is al gepauzeerd.");
        }
    }

    public void Next() { }
    public void Stop()
    {
        _isPaused = false;
        _isPlaying = false;
        _remainingTime = Duur;
        Console.WriteLine($"Liedje '{Title}' is gestopt.");
    }

    public int Length => _remainingTime;

    public override string ToString()
    {
        return Title;
    }
}
