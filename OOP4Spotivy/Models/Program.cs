using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP4Spotivy.NewFolder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mainUser = new SuperUser("Hoofdgebruiker");
            // Voeg 2 hardcoded gebruikers toe
            var user1 = new SuperUser("Pim");
            var user2 = new SuperUser("Max");

            var users = new List<Person> { mainUser, user1, user2 };
            var albums = new List<Album>();
            var songs = new List<Song>();

            // Vriendschapsverzoeken bijhouden
            var friendRequests = new Dictionary<string, List<Person>>();
            foreach (var user in users)
            {
                friendRequests[user.Naam] = new List<Person>();
            }

            // Voorbeeldartiesten
            var artist1 = new Artist("The Beatles", new List<Album>());
            var artist2 = new Artist("Queen", new List<Album>());
            var artist3 = new Artist("Michael Jackson", new List<Album>());
            var artist4 = new Artist("Taylor Swift", new List<Album>());
            var artist5 = new Artist("Ed Sheeran", new List<Album>());
            var artist6 = new Artist("Adele", new List<Album>());

            // Voorbeeldliedjes
            var song1 = new Song("Happy Song", new List<Artist> { artist1 }, 180, Genres.Pop);
            var song2 = new Song("Sad Song", new List<Artist> { artist2 }, 120, Genres.Rock);
            var song3 = new Song("Shape of You", new List<Artist> { artist5 }, 233, Genres.Pop);
            var song4 = new Song("Rolling in the Deep", new List<Artist> { artist6 }, 228, Genres.Pop);
            var song5 = new Song("Blank Space", new List<Artist> { artist4 }, 231, Genres.Pop);
            var song6 = new Song("Thinking Out Loud", new List<Artist> { artist5 }, 281, Genres.Pop);
            var song7 = new Song("Hello", new List<Artist> { artist6 }, 295, Genres.Pop);
            var song8 = new Song("Bad Blood", new List<Artist> { artist4 }, 211, Genres.Pop);

            songs.AddRange(new[] { song1, song2, song3, song4, song5, song6, song7, song8 });

            // Voorbeeldalbums met liedjes
            var albumSongs1 = new List<Song>
            {
                new Song("Album 1 Track 1", new List<Artist> { artist1 }, 160, Genres.Pop),
                new Song("Album 1 Track 2", new List<Artist> { artist1 }, 170, Genres.Pop),
                new Song("Album 1 Track 3", new List<Artist> { artist1, artist2 }, 150, Genres.Rock)
            };

            var albumSongs2 = new List<Song>
            {
                new Song("Album 2 Track 1", new List<Artist> { artist2 }, 130, Genres.Rock),
                new Song("Album 2 Track 2", new List<Artist> { artist2 }, 140, Genres.Rock),
                new Song("Album 2 Track 3", new List<Artist> { artist2, artist3 }, 125, Genres.Pop)
            };
            var albumSongs3 = new List<Song>
            {
                new Song("Love Story", new List<Artist> { artist4 }, 230, Genres.Pop),
                new Song("You Belong With Me", new List<Artist> { artist4 }, 220, Genres.Pop),
                new Song("Wildest Dreams", new List<Artist> { artist4 }, 210, Genres.Pop)
            };

            var albumSongs4 = new List<Song>
            {
                new Song("Photograph", new List<Artist> { artist5 }, 258, Genres.Pop),
                new Song("Perfect", new List<Artist> { artist5 }, 263, Genres.Pop),
                new Song("Castle on the Hill", new List<Artist> { artist5 }, 261, Genres.Rock)
            };

            var albumSongs5 = new List<Song>
            {
                new Song("Someone Like You", new List<Artist> { artist6 }, 285, Genres.Pop),
                new Song("Set Fire to the Rain", new List<Artist> { artist6 }, 242, Genres.Pop),
                new Song("Skyfall", new List<Artist> { artist6 }, 285, Genres.Pop)
            };

            var album1 = new Album(new List<Artist> { artist1 }, "Greatest Hits Vol. 1", albumSongs1);
            var album2 = new Album(new List<Artist> { artist2, artist3 }, "Rock Classics", albumSongs2);
            var album3 = new Album(new List<Artist> { artist4 }, "1989", albumSongs3);
            var album4 = new Album(new List<Artist> { artist5 }, "Divide", albumSongs4);
            var album5 = new Album(new List<Artist> { artist6 }, "21", albumSongs5);

            albums.AddRange(new[] { album1, album2, album3, album4, album5 });

            songs.AddRange(albumSongs1);
            songs.AddRange(albumSongs2);
            songs.AddRange(albumSongs3);
            songs.AddRange(albumSongs4);
            songs.AddRange(albumSongs5);

            // Maak een voorbeeldplaylist voor elke gebruiker
            user1.CreatePlaylist("Pim Hardcoded");
            user1.Playlists[0].Add(song5);
            user1.Playlists[0].Add(song7);

            user2.CreatePlaylist("Max Hardcoded");
            user2.Playlists[0].Add(song2);
            user2.Playlists[0].Add(albumSongs4[2]); // Castle on the Hill

            var client = new Client(users, albums, songs)
            {
                ActiveUser = mainUser
            };
            client.AllSongs = songs;
            client.AllAlbums = albums;

            while (true)
            {
                Console.WriteLine("\n===== SPOTIVY MENU =====");
                Console.WriteLine($"Ingelogde gebruiker: {client.ActiveUser.Naam}");
                Console.WriteLine("1. Maak een nieuwe afspeellijst aan");
                Console.WriteLine("2. Voeg een liedje toe aan een afspeellijst");
                Console.WriteLine("3. Verwijder een liedje uit een afspeellijst");
                Console.WriteLine("4. Speel een liedje uit een afspeellijst af");
                Console.WriteLine("5. Voeg een album toe aan een afspeellijst");
                Console.WriteLine("6. Speel een liedje uit de algemene lijst af");
                Console.WriteLine("7. Toon huidig afgespeeld nummer");
                Console.WriteLine("8. Pauzeer huidig nummer");
                Console.WriteLine("9. Speel huidig nummer verder");
                Console.WriteLine("10. Wissel van gebruiker");
                Console.WriteLine("11. Speel een afspeellijst af (volgorde of shuffle)");
                Console.WriteLine("12. Speel een album af (volgorde of shuffle)");
                Console.WriteLine("13. Vrienden beheren");  // Nieuwe optie
                Console.WriteLine("0. Afsluiten");
                Console.Write("Kies een optie: ");
                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Geef een naam voor de nieuwe afspeellijst: ");
                    string? name = Console.ReadLine();
                    if (!string.IsNullOrEmpty(name))
                        client.ActiveUser.CreatePlaylist(name);
                    else
                        Console.WriteLine("Ongeldige naam voor afspeellijst.");
                }
                else if (choice == "2")
                {
                    var playlists = client.ActiveUser.Playlists;
                    if (playlists.Count == 0)
                    {
                        Console.WriteLine("Je hebt nog geen afspeellijsten.");
                        continue;
                    }
                    Console.WriteLine("Kies een afspeellijst:");
                    for (int i = 0; i < playlists.Count; i++)
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlists.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedPlaylist = playlists[playlistIndex - 1];

                    if (client.AllSongs.Count == 0)
                    {
                        Console.WriteLine("Er zijn geen liedjes beschikbaar.");
                        continue;
                    }
                    Console.WriteLine("Kies een liedje om toe te voegen:");
                    for (int i = 0; i < client.AllSongs.Count; i++)
                    {
                        var song = client.AllSongs[i];
                        string artists = song.Artists != null && song.Artists.Count > 0
                            ? string.Join(", ", song.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {song.Title} - Artiest(en): {artists}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > client.AllSongs.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedSong = client.AllSongs[songIndex - 1];
                    selectedPlaylist.Add(selectedSong);
                    Console.WriteLine($"Liedje '{selectedSong.Title}' toegevoegd aan afspeellijst '{selectedPlaylist.Title}'.");
                }
                else if (choice == "3")
                {
                    var playlists = client.ActiveUser.Playlists;
                    if (playlists.Count == 0)
                    {
                        Console.WriteLine("Je hebt nog geen afspeellijsten.");
                        continue;
                    }
                    Console.WriteLine("Kies een afspeellijst:");
                    for (int i = 0; i < playlists.Count; i++)
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlists.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedPlaylist = playlists[playlistIndex - 1];
                    var playables = selectedPlaylist.ShowPlayables();
                    if (playables.Count == 0)
                    {
                        Console.WriteLine("Deze afspeellijst bevat geen liedjes.");
                        continue;
                    }
                    Console.WriteLine("Kies een liedje om te verwijderen:");
                    for (int i = 0; i < playables.Count; i++)
                    {
                        if (playables[i] is Song s)
                        {
                            string artists = s.Artists != null && s.Artists.Count > 0
                                ? string.Join(", ", s.Artists.Select(a => a.Naam))
                                : "Onbekend";
                            Console.WriteLine($"{i + 1}. {s.Title} - Artiest(en): {artists}");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. [Niet een los liedje]");
                        }
                    }
                    if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > playables.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var toRemove = playables[songIndex - 1];
                    selectedPlaylist.Remove(toRemove);
                    Console.WriteLine("Liedje verwijderd uit de afspeellijst.");
                }
                else if (choice == "4")
                {
                    var playlists = client.ActiveUser.Playlists;
                    if (playlists.Count == 0)
                    {
                        Console.WriteLine("Je hebt nog geen afspeellijsten.");
                        continue;
                    }
                    Console.WriteLine("Kies een afspeellijst:");
                    for (int i = 0; i < playlists.Count; i++)
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlists.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedPlaylist = playlists[playlistIndex - 1];
                    var playables = selectedPlaylist.ShowPlayables();
                    if (playables.Count == 0)
                    {
                        Console.WriteLine("Deze afspeellijst bevat geen liedjes.");
                        continue;
                    }
                    Console.WriteLine("Kies een liedje om af te spelen:");
                    for (int i = 0; i < playables.Count; i++)
                    {
                        if (playables[i] is Song s)
                        {
                            string artists = s.Artists != null && s.Artists.Count > 0
                                ? string.Join(", ", s.Artists.Select(a => a.Naam))
                                : "Onbekend";
                            Console.WriteLine($"{i + 1}. {s.Title} - Artiest(en): {artists}");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. [Niet een los liedje]");
                        }
                    }
                    if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > playables.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var toPlay = playables[songIndex - 1];
                    client.CurrentlyPlaying = toPlay;
                    toPlay.Play();
                }
                else if (choice == "5")
                {
                    var playlists = client.ActiveUser.Playlists;
                    if (playlists.Count == 0)
                    {
                        Console.WriteLine("Je hebt nog geen afspeellijsten.");
                        continue;
                    }
                    Console.WriteLine("Kies een afspeellijst:");
                    for (int i = 0; i < playlists.Count; i++)
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlists.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedPlaylist = playlists[playlistIndex - 1];
                    if (client.AllAlbums == null || client.AllAlbums.Count == 0)
                    {
                        Console.WriteLine("Er zijn geen albums beschikbaar.");
                        continue;
                    }
                    Console.WriteLine("Kies een album om toe te voegen:");
                    for (int i = 0; i < client.AllAlbums.Count; i++)
                    {
                        var album = client.AllAlbums[i];
                        string artists = album.Artists != null && album.Artists.Count > 0
                            ? string.Join(", ", album.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {album.Title} - Artiest(en): {artists}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int albumIndex) || albumIndex < 1 || albumIndex > client.AllAlbums.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedAlbum = client.AllAlbums[albumIndex - 1];
                    var albumSongs = selectedAlbum.ShowPlayables();
                    foreach (var playable in albumSongs)
                        selectedPlaylist.Add(playable);
                    Console.WriteLine($"Alle liedjes van album '{selectedAlbum.Title}' zijn toegevoegd aan afspeellijst '{selectedPlaylist.Title}'.");
                }
                else if (choice == "6")
                {
                    if (client.AllSongs.Count == 0)
                    {
                        Console.WriteLine("Er zijn geen liedjes beschikbaar.");
                        continue;
                    }
                    Console.WriteLine("Kies een liedje om af te spelen:");
                    for (int i = 0; i < client.AllSongs.Count; i++)
                    {
                        var song = client.AllSongs[i];
                        string artists = song.Artists != null && song.Artists.Count > 0
                            ? string.Join(", ", song.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {song.Title} - Artiest(en): {artists}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > client.AllSongs.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var toPlay = client.AllSongs[songIndex - 1];
                    client.CurrentlyPlaying = toPlay;
                    toPlay.Play();
                }
                else if (choice == "7")
                {
                    if (client.CurrentlyPlaying is Song current)
                    {
                        string artists = current.Artists != null && current.Artists.Count > 0
                            ? string.Join(", ", current.Artists.ConvertAll(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"Speelt nu af: '{current.Title}' - Artiest(en): {artists} - Genre: {current.SongGenre} - Nog {current.Length} seconden");
                    }
                    else
                        Console.WriteLine("Er wordt momenteel geen nummer afgespeeld.");
                }
                else if (choice == "8")
                {
                    if (client.CurrentlyPlaying is Song current)
                        current.Pause();
                    else
                        Console.WriteLine("Er wordt momenteel geen nummer afgespeeld.");
                }
                else if (choice == "9")
                {
                    if (client.CurrentlyPlaying is Song current)
                        current.Play();
                    else
                        Console.WriteLine("Er wordt momenteel geen nummer afgespeeld.");
                }
                else if (choice == "10") // Wissel van gebruiker
                {
                    Console.WriteLine("\nKies een gebruiker om mee in te loggen:");
                    for (int i = 0; i < users.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {users[i].Naam}");
                    }

                    if (!int.TryParse(Console.ReadLine(), out int userIndex) || userIndex < 1 || userIndex > users.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }

                    var selectedUser = users[userIndex - 1] as SuperUser;
                    if (selectedUser != null)
                    {
                        client.ActiveUser = selectedUser;
                        Console.WriteLine($"Ingelogd als: {client.ActiveUser.Naam}");
                    }
                    else
                    {
                        Console.WriteLine("De geselecteerde gebruiker is geen SuperUser.");
                    }
                }
                else if (choice == "11")
                {
                    var playlists = client.ActiveUser.Playlists;
                    if (playlists.Count == 0)
                    {
                        Console.WriteLine("Je hebt nog geen afspeellijsten.");
                        continue;
                    }
                    Console.WriteLine("Kies een afspeellijst:");
                    for (int i = 0; i < playlists.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlists.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedPlaylist = playlists[playlistIndex - 1];

                    // Controleer of de afspeellijst items bevat
                    var playables = selectedPlaylist.ShowPlayables();
                    if (playables.Count == 0)
                    {
                        Console.WriteLine("Deze afspeellijst bevat geen liedjes.");
                        continue;
                    }

                    Console.WriteLine("1. Speel op volgorde af");
                    Console.WriteLine("2. Speel in willekeurige volgorde af");
                    var subChoice = Console.ReadLine();

                    // Bepaal de afspeellijst (normaal of shuffled)
                    List<iPlayable> playList;
                    if (subChoice == "1")
                    {
                        playList = playables;
                        Console.WriteLine("Normale afspeelvolgorde:");
                    }
                    else if (subChoice == "2")
                    {
                        playList = new List<iPlayable>(playables);
                        var rnd = new Random();
                        int n = playList.Count;

                        // Fisher-Yates shuffle algoritme
                        while (n > 1)
                        {
                            n--;
                            int k = rnd.Next(n + 1);
                            (playList[n], playList[k]) = (playList[k], playList[n]);
                        }
                        Console.WriteLine("Willekeurige afspeelvolgorde:");
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }

                    // Toon de afspeelvolgorde
                    for (int i = 0; i < playList.Count; i++)
                    {
                        if (playList[i] is Song s)
                            Console.WriteLine($"{i + 1}. {s.Title} - {string.Join(", ", s.Artists.Select(a => a.Naam))}");
                        else
                            Console.WriteLine($"{i + 1}. {playList[i]}");
                    }

                    // Start het afspelen
                    int currentIndex = 0;
                    bool stop = false;
                    bool isPaused = false;

                    while (!stop && playList.Count > 0 && currentIndex < playList.Count)
                    {
                        var current = playList[currentIndex];
                        client.CurrentlyPlaying = current;

                        // Toon duidelijke informatie over het huidige nummer en waar we zijn in de afspeellijst
                        Console.WriteLine("\n---------------------------------------------------");
                        Console.WriteLine($"HUIDIGE POSITIE: {currentIndex + 1} van {playList.Count}");

                        if (current is Song s)
                        {
                            string artists = string.Join(", ", s.Artists.Select(a => a.Naam));
                            Console.WriteLine($"NU SPEELT: '{s.Title}' - Artiest(en): {artists} - Genre: {s.SongGenre} - Duur: {s.Length} sec");

                            if (currentIndex < playList.Count - 1 && playList[currentIndex + 1] is Song nextSong)
                            {
                                string nextArtists = string.Join(", ", nextSong.Artists.Select(a => a.Naam));
                                Console.WriteLine($"VOLGENDE: '{nextSong.Title}' - {nextArtists}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"NU SPEELT: {current}");
                        }
                        Console.WriteLine("---------------------------------------------------");

                        // Speel het nummer af als niet gepauzeerd
                        if (!isPaused)
                        {
                            current.Play();
                        }

                        // Toon besturingsknoppen
                        Console.WriteLine("\nBediening:");
                        Console.WriteLine("1. Volgende");
                        Console.WriteLine("2. Vorige");
                        Console.WriteLine("3. Pauze");
                        Console.WriteLine("4. Verder spelen");
                        Console.WriteLine("5. Stoppen");
                        Console.Write("Kies een optie: ");
                        var playChoice = Console.ReadLine();

                        switch (playChoice)
                        {
                            case "1":
                                if (currentIndex < playList.Count - 1)
                                {
                                    currentIndex++;
                                    isPaused = false;
                                }
                                else
                                {
                                    Console.WriteLine("Einde van de afspeellijst.");
                                    stop = true;
                                }
                                break;
                            case "2":
                                if (currentIndex > 0)
                                {
                                    currentIndex--;
                                    isPaused = false;
                                }
                                else
                                {
                                    Console.WriteLine("Dit is het eerste nummer.");
                                }
                                break;
                            case "3":
                                if (current is Song song)
                                {
                                    song.Pause();
                                    isPaused = true;
                                }
                                break;
                            case "4":
                                if (current is Song currentSong)
                                {
                                    currentSong.Play();
                                    isPaused = false;
                                }
                                break;
                            case "5":
                                stop = true;
                                break;
                            default:
                                Console.WriteLine("Ongeldige keuze.");
                                break;
                        }
                    }
                }
                else if (choice == "12")
                {
                    if (client.AllAlbums == null || client.AllAlbums.Count == 0)
                    {
                        Console.WriteLine("Er zijn geen albums beschikbaar.");
                        continue;
                    }
                    Console.WriteLine("Kies een album:");
                    for (int i = 0; i < client.AllAlbums.Count; i++)
                    {
                        var album = client.AllAlbums[i];
                        string artists = album.Artists != null && album.Artists.Count > 0
                            ? string.Join(", ", album.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {album.Title} - Artiest(en): {artists}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int albumIndex) || albumIndex < 1 || albumIndex > client.AllAlbums.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var selectedAlbum = client.AllAlbums[albumIndex - 1];
                    var playables = selectedAlbum.ShowPlayables();
                    if (playables.Count == 0)
                    {
                        Console.WriteLine("Dit album bevat geen liedjes.");
                        continue;
                    }

                    Console.WriteLine("1. Speel op volgorde af");
                    Console.WriteLine("2. Speel in willekeurige volgorde af");
                    var subChoice = Console.ReadLine();

                    List<iPlayable> playList;
                    if (subChoice == "1")
                    {
                        playList = playables;
                        Console.WriteLine("Normale afspeelvolgorde:");
                    }
                    else if (subChoice == "2")
                    {
                        playList = new List<iPlayable>(playables);
                        var rnd = new Random();
                        int n = playList.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rnd.Next(n + 1);
                            (playList[n], playList[k]) = (playList[k], playList[n]);
                        }
                        Console.WriteLine("Willekeurige afspeelvolgorde:");
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }

                    for (int i = 0; i < playList.Count; i++)
                    {
                        if (playList[i] is Song s)
                            Console.WriteLine($"{i + 1}. {s.Title} - {string.Join(", ", s.Artists.Select(a => a.Naam))}");
                        else
                            Console.WriteLine($"{i + 1}. {playList[i]}");
                    }

                    int currentIndex = 0;
                    bool stop = false;
                    bool isPaused = false;

                    while (!stop && playList.Count > 0 && currentIndex < playList.Count)
                    {
                        var current = playList[currentIndex];
                        client.CurrentlyPlaying = current;

                        Console.WriteLine("\n--------------------------------------------------");
                        Console.WriteLine($"HUIDIGE POSITIE: {currentIndex + 1} van {playList.Count}");

                        if (current is Song s)
                        {
                            string artists = string.Join(", ", s.Artists.Select(a => a.Naam));
                            Console.WriteLine($"NU SPEELT: '{s.Title}' - Artiest(en): {artists} - Genre: {s.SongGenre} - Duur: {s.Length} sec");

                            if (currentIndex < playList.Count - 1 && playList[currentIndex + 1] is Song nextSong)
                            {
                                string nextArtists = string.Join(", ", nextSong.Artists.Select(a => a.Naam));
                                Console.WriteLine($"VOLGENDE: '{nextSong.Title}' - {nextArtists}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"NU SPEELT: {current}");
                        }
                        Console.WriteLine("--------------------------------------------------");

                        if (!isPaused)
                        {
                            current.Play();
                        }

                        Console.WriteLine("\nBediening:");
                        Console.WriteLine("1. Volgende");
                        Console.WriteLine("2. Vorige");
                        Console.WriteLine("3. Pauze");
                        Console.WriteLine("4. Verder spelen");
                        Console.WriteLine("5. Stoppen");
                        Console.Write("Kies een optie: ");
                        var playChoice = Console.ReadLine();

                        switch (playChoice)
                        {
                            case "1":
                                if (currentIndex < playList.Count - 1)
                                {
                                    currentIndex++;
                                    isPaused = false;
                                }
                                else
                                {
                                    Console.WriteLine("Einde van het album.");
                                    stop = true;
                                }
                                break;
                            case "2":
                                if (currentIndex > 0)
                                {
                                    currentIndex--;
                                    isPaused = false;
                                }
                                else
                                {
                                    Console.WriteLine("Dit is het eerste nummer.");
                                }
                                break;
                            case "3":
                                if (current is Song song)
                                {
                                    song.Pause();
                                    isPaused = true;
                                }
                                break;
                            case "4":
                                if (current is Song currentSong)
                                {
                                    currentSong.Play();
                                    isPaused = false;
                                }
                                break;
                            case "5":
                                stop = true;
                                break;
                            default:
                                Console.WriteLine("Ongeldige keuze.");
                                break;
                        }
                    }
                }
                else if (choice == "13") // Nieuwe optie: Vrienden beheren
                {
                    Console.WriteLine("\n===== VRIENDEN BEHEREN =====");
                    Console.WriteLine("1. Toon mijn vrienden");
                    Console.WriteLine("2. Vriendschapsverzoek sturen");
                    Console.WriteLine("3. Vriendschapsverzoeken bekijken");
                    Console.WriteLine("4. Vriendschapsverzoek accepteren");
                    Console.WriteLine("5. Vriendschapsverzoek weigeren");
                    Console.WriteLine("6. Verwijder een vriend");
                    Console.WriteLine("7. Bekijk speellijsten van een vriend");
                    Console.WriteLine("8. Vergelijk een speellijst met die van een vriend");
                    Console.WriteLine("0. Terug naar hoofdmenu");
                    Console.Write("Kies een optie: ");

                    var friendChoice = Console.ReadLine();

                    // 1. Toon mijn vrienden
                    if (friendChoice == "1")
                    {
                        var friends = client.ActiveUser.Friends;
                        if (friends.Count == 0)
                        {
                            Console.WriteLine("Je hebt nog geen vrienden.");
                            continue;
                        }

                        Console.WriteLine("\nJouw vrienden:");
                        for (int i = 0; i < friends.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {friends[i].Naam}");
                        }
                    }

                    // 2. Vriendschapsverzoek sturen
                    else if (friendChoice == "2")
                    {
                        // Toon alle gebruikers behalve jezelf
                        var potentialFriends = users.Where(g => g.Naam != client.ActiveUser.Naam).ToList();

                        if (potentialFriends.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen andere gebruikers om vriendschapsverzoeken naar te sturen.");
                            continue;
                        }

                        Console.WriteLine("\nKies een gebruiker om een vriendschapsverzoek naar te sturen:");
                        for (int i = 0; i < potentialFriends.Count; i++)
                        {
                            var isAlreadyFriend = client.ActiveUser.Friends.Any(f => f.Naam == potentialFriends[i].Naam);
                            var status = isAlreadyFriend ? " (Al vriend)" : "";

                            // Controleer of er al een verzoek is gestuurd naar deze gebruiker
                            var hasPendingRequest = friendRequests[potentialFriends[i].Naam].Any(p => p.Naam == client.ActiveUser.Naam);
                            if (hasPendingRequest)
                            {
                                status = " (Verzoek verstuurd)";
                            }

                            Console.WriteLine($"{i + 1}. {potentialFriends[i].Naam}{status}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int friendIndex) || friendIndex < 1 || friendIndex > potentialFriends.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedFriend = potentialFriends[friendIndex - 1];

                        // Controleer of deze persoon al een vriend is
                        if (client.ActiveUser.Friends.Any(f => f.Naam == selectedFriend.Naam))
                        {
                            Console.WriteLine($"{selectedFriend.Naam} is al jouw vriend.");
                            continue;
                        }

                        // Controleer of er al een verzoek is gestuurd naar deze gebruiker
                        if (friendRequests[selectedFriend.Naam].Any(p => p.Naam == client.ActiveUser.Naam))
                        {
                            Console.WriteLine($"Je hebt al een vriendschapsverzoek gestuurd naar {selectedFriend.Naam}.");
                            continue;
                        }

                        // Voeg een vriendschapsverzoek toe
                        friendRequests[selectedFriend.Naam].Add(client.ActiveUser);
                        Console.WriteLine($"Vriendschapsverzoek verstuurd naar {selectedFriend.Naam}.");
                    }

                    // 3. Vriendschapsverzoeken bekijken
                    else if (friendChoice == "3")
                    {
                        var currentRequests = friendRequests[client.ActiveUser.Naam];

                        if (currentRequests.Count == 0)
                        {
                            Console.WriteLine("Je hebt geen openstaande vriendschapsverzoeken.");
                            continue;
                        }

                        Console.WriteLine("\nOpenstaande vriendschapsverzoeken:");
                        for (int i = 0; i < currentRequests.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {currentRequests[i].Naam}");
                        }
                    }

                    // 4. Vriendschapsverzoek accepteren
                    else if (friendChoice == "4")
                    {
                        var currentRequests = friendRequests[client.ActiveUser.Naam];

                        if (currentRequests.Count == 0)
                        {
                            Console.WriteLine("Je hebt geen openstaande vriendschapsverzoeken.");
                            continue;
                        }

                        Console.WriteLine("\nKies een vriendschapsverzoek om te accepteren:");
                        for (int i = 0; i < currentRequests.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {currentRequests[i].Naam}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int requestIndex) || requestIndex < 1 || requestIndex > currentRequests.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedRequest = currentRequests[requestIndex - 1];

                        // Vriendschapsverzoek accepteren: voeg elkaar toe aan vriendlijsten
                        client.ActiveUser.Friends.Add(selectedRequest);
                        selectedRequest.Friends.Add(client.ActiveUser);

                        // Verwijder het verzoek
                        currentRequests.RemoveAt(requestIndex - 1);

                        Console.WriteLine($"Je bent nu vrienden met {selectedRequest.Naam}!");
                    }

                    // 5. Vriendschapsverzoek weigeren
                    else if (friendChoice == "5")
                    {
                        var currentRequests = friendRequests[client.ActiveUser.Naam];

                        if (currentRequests.Count == 0)
                        {
                            Console.WriteLine("Je hebt geen openstaande vriendschapsverzoeken.");
                            continue;
                        }

                        Console.WriteLine("\nKies een vriendschapsverzoek om te weigeren:");
                        for (int i = 0; i < currentRequests.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {currentRequests[i].Naam}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int requestIndex) || requestIndex < 1 || requestIndex > currentRequests.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedRequest = currentRequests[requestIndex - 1];

                        // Verwijder het verzoek
                        currentRequests.RemoveAt(requestIndex - 1);

                        Console.WriteLine($"Je hebt het vriendschapsverzoek van {selectedRequest.Naam} geweigerd.");
                    }

                    // 6. Verwijder een vriend
                    else if (friendChoice == "6")
                    {
                        var friends = client.ActiveUser.Friends;

                        if (friends.Count == 0)
                        {
                            Console.WriteLine("Je hebt nog geen vrienden om te verwijderen.");
                            continue;
                        }

                        Console.WriteLine("\nKies een vriend om te verwijderen:");
                        for (int i = 0; i < friends.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {friends[i].Naam}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int friendIndex) || friendIndex < 1 || friendIndex > friends.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedFriend = friends[friendIndex - 1];

                        // Wederzijds vriendschappen verwijderen
                        client.ActiveUser.Friends.Remove(selectedFriend);
                        selectedFriend.Friends.Remove(client.ActiveUser);

                        Console.WriteLine($"Je bent niet meer bevriend met {selectedFriend.Naam}.");
                    }
                    // 7. Bekijk speellijsten van een vriend
                    else if (friendChoice == "7")
                    {
                        var friends = client.ActiveUser.Friends;
                        if (friends.Count == 0)
                        {
                            Console.WriteLine("Je hebt nog geen vrienden.");
                            continue;
                        }

                        Console.WriteLine("\nKies een vriend om diens speellijsten te bekijken:");
                        for (int i = 0; i < friends.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {friends[i].Naam}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int friendIndex) || friendIndex < 1 || friendIndex > friends.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedFriend = friends[friendIndex - 1];
                        var playlists = selectedFriend.Playlists;

                        if (playlists.Count == 0)
                        {
                            Console.WriteLine($"{selectedFriend.Naam} heeft geen speellijsten.");
                            continue;
                        }

                        Console.WriteLine($"\nSpeellijsten van {selectedFriend.Naam}:");
                        for (int i = 0; i < playlists.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                        }

                        Console.Write("Kies een speellijst om de nummers te bekijken: ");
                        if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlists.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedPlaylist = playlists[playlistIndex - 1];
                        var playables = selectedPlaylist.ShowPlayables();

                        if (playables.Count == 0)
                        {
                            Console.WriteLine("Deze speellijst bevat geen nummers.");
                            continue;
                        }

                        Console.WriteLine($"\nNummers in '{selectedPlaylist.Title}':");
                        for (int i = 0; i < playables.Count; i++)
                        {
                            if (playables[i] is Song s)
                            {
                                string artists = s.Artists != null && s.Artists.Count > 0
                                    ? string.Join(", ", s.Artists.Select(a => a.Naam))
                                    : "Onbekend";
                                Console.WriteLine($"{i + 1}. {s.Title} - Artiest(en): {artists}");
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. [Niet een los liedje]");
                            }
                        }
                    }
                    // 8. Vergelijk een speellijst met die van een vriend
                    else if (friendChoice == "8")
                    {
                        var friends = client.ActiveUser.Friends;
                        if (friends.Count == 0)
                        {
                            Console.WriteLine("Je hebt nog geen vrienden.");
                            continue;
                        }

                        Console.WriteLine("\nKies een vriend om mee te vergelijken:");
                        for (int i = 0; i < friends.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {friends[i].Naam}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int friendIndex) || friendIndex < 1 || friendIndex > friends.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedFriend = friends[friendIndex - 1];
                        var friendPlaylists = selectedFriend.Playlists;
                        if (friendPlaylists.Count == 0)
                        {
                            Console.WriteLine($"{selectedFriend.Naam} heeft geen speellijsten.");
                            continue;
                        }

                        Console.WriteLine($"\nSpeellijsten van {selectedFriend.Naam}:");
                        for (int i = 0; i < friendPlaylists.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {friendPlaylists[i].Title}");
                        }
                        Console.Write("Kies een speellijst van je vriend: ");
                        if (!int.TryParse(Console.ReadLine(), out int friendPlaylistIndex) || friendPlaylistIndex < 1 || friendPlaylistIndex > friendPlaylists.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }
                        var friendPlaylist = friendPlaylists[friendPlaylistIndex - 1];
                        var friendSongs = friendPlaylist.ShowPlayables().OfType<Song>().ToList();
                        if (friendSongs.Count == 0)
                        {
                            Console.WriteLine("Deze speellijst bevat geen nummers.");
                            continue;
                        }

                        // Kies eigen speellijst
                        var myPlaylists = client.ActiveUser.Playlists;
                        if (myPlaylists.Count == 0)
                        {
                            Console.WriteLine("Je hebt zelf geen speellijsten.");
                            continue;
                        }
                        Console.WriteLine("\nJouw speellijsten:");
                        for (int i = 0; i < myPlaylists.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {myPlaylists[i].Title}");
                        }
                        Console.Write("Kies een van je eigen speellijsten: ");
                        if (!int.TryParse(Console.ReadLine(), out int myPlaylistIndex) || myPlaylistIndex < 1 || myPlaylistIndex > myPlaylists.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }
                        var myPlaylist = myPlaylists[myPlaylistIndex - 1];
                        var mySongs = myPlaylist.ShowPlayables().OfType<Song>().ToList();
                        if (mySongs.Count == 0)
                        {
                            Console.WriteLine("Jouw speellijst bevat geen nummers.");
                            continue;
                        }

                        // Vergelijk op basis van titel en artiesten
                        var matching = mySongs.Where(ms =>
                            friendSongs.Any(vs =>
                                string.Equals(ms.Title, vs.Title, StringComparison.OrdinalIgnoreCase) &&
                                ms.Artists.Count == vs.Artists.Count &&
                                ms.Artists.All(a => vs.Artists.Any(va => va.Naam == a.Naam))
                            )).ToList();

                        Console.WriteLine($"\nAantal overeenkomende nummers: {matching.Count}");
                        if (matching.Count > 0)
                        {
                            Console.WriteLine("Overeenkomende nummers:");
                            foreach (var song in matching)
                            {
                                string artists = string.Join(", ", song.Artists.Select(a => a.Naam));
                                Console.WriteLine($"- {song.Title} - {artists}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Er zijn geen overeenkomende nummers gevonden.");
                        }
                    }

                    // 0. Terug naar hoofdmenu
                    else if (friendChoice == "0")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze, probeer opnieuw.");
                    }
                }
                else if (choice == "0")
                {
                    Console.WriteLine("Bedankt voor het gebruiken van Spotivy. Tot ziens!");
                    break;
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze, probeer opnieuw.");
                }
                Console.WriteLine();
            }
        }
    }
}
