using System.Collections.Generic;

public class SongCollection
{
    public string Title { get; set; }
    public List<iPlayable> playables { get; set; }

    public SongCollection(string title)
    {
        Title = title;
        playables = new List<iPlayable>();
    }

    public override string ToString()
    {
        return Title;
    }

    public List<iPlayable> ShowPlayables()
    {
        return playables;
    }
}
