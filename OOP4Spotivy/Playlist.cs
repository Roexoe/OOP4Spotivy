public class Playlist : SongCollection
{
    public Person Owner { get; set; }

    public Playlist(Person owner, string title) : base(title) { }
    public void Add(iPlayable playable) { }
    public void Remove(iPlayable playable) { }
    public override string ToString() => base.ToString();
}
