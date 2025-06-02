using System.Collections.Generic;

public class SongCollection
{
    public string Title { get; set; }
    public List<iPlayable> playables { get; set; }

    public SongCollection(string title) { }
    public override string ToString() => base.ToString();
    public List<iPlayable> ShowPlayables() { return null; }
}
