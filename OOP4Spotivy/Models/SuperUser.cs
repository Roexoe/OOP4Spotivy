public class SuperUser : Person
{
    public SuperUser(string naam) : base(naam) { }

    public void AddFriend(Person person) { }
    public void RemoveFriend(Person person) { }
    public new Playlist CreatePlaylist(string title)
    {
        // Roep de CreatePlaylist methode van de basisklasse aan
        return base.CreatePlaylist(title);
    }
    public void RemovePlayList(int index) { }
    public void AddToPlayList(iPlayable playable) { }
    public void RemoveFromPlayList(iPlayable playable) { }
}
