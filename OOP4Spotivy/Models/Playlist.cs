using System.Collections.Generic;

public class Playlist : SongCollection
{
    public Person Owner { get; set; }

    public Playlist(Person owner, string title) : base(title)
    {
        Owner = owner;
        playables = new List<iPlayable>();
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
        foreach (var playable in playables)
        {
            playable.Play();
        }
    }

    public override string ToString() => $"{Title} (Owner: {Owner.Naam}, {playables.Count} items)";
}
