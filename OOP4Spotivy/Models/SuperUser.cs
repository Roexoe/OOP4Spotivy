public class SuperUser : Person
{
    public SuperUser(string naam) : base(naam) { }

    public void AddFriend(Person person) { }
    public void RemoveFriend(Person person) { }
    public Playlist CreatePlayList(string title) { return null; }
    public void RemovePlayList(int index) { }
    public void AddToPlayList(iPlayable playable) { }
    public void RemoveFromPlayList(iPlayable playable) { }
}
