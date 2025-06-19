using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP4Spotivy.NewFolder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var hoofdgebruiker = new SuperUser("Hoofdgebruiker");
            // Voeg 2 hardcoded gebruikers toe
            var gebruiker1 = new SuperUser("Pim");
            var gebruiker2 = new SuperUser("Max");

            var gebruikers = new List<Person> { hoofdgebruiker, gebruiker1, gebruiker2 };
            var albums = new List<Album>();
            var songs = new List<Song>();

            // Vriendschapsverzoeken bijhouden
            var friendRequests = new Dictionary<string, List<Person>>();
            foreach (var gebruiker in gebruikers)
            {
                friendRequests[gebruiker.Naam] = new List<Person>();
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
            gebruiker1.CreatePlaylist("Alice's Favorieten");
            gebruiker1.Playlists[0].Add(song5);
            gebruiker1.Playlists[0].Add(song7);

            gebruiker2.CreatePlaylist("Bob's Rock Playlist");
            gebruiker2.Playlists[0].Add(song2);
            gebruiker2.Playlists[0].Add(albumSongs4[2]); // Castle on the Hill

            var client = new Client(gebruikers, albums, songs)
            {
                ActiveUser = hoofdgebruiker
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
                string? keuze = Console.ReadLine();

                if (keuze == "1")
                {
                    Console.Write("Geef een naam voor de nieuwe afspeellijst: ");
                    string? naam = Console.ReadLine();
                    if (!string.IsNullOrEmpty(naam))
                        client.ActiveUser.CreatePlaylist(naam);
                    else
                        Console.WriteLine("Ongeldige naam voor afspeellijst.");
                }
                else if (keuze == "2")
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
                    var gekozenPlaylist = playlists[playlistIndex - 1];

                    if (client.AllSongs.Count == 0)
                    {
                        Console.WriteLine("Er zijn geen liedjes beschikbaar.");
                        continue;
                    }
                    Console.WriteLine("Kies een liedje om toe te voegen:");
                    for (int i = 0; i < client.AllSongs.Count; i++)
                    {
                        var song = client.AllSongs[i];
                        string artiesten = song.Artists != null && song.Artists.Count > 0
                            ? string.Join(", ", song.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {song.Title} - Artiest(en): {artiesten}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > client.AllSongs.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var gekozenSong = client.AllSongs[songIndex - 1];
                    gekozenPlaylist.Add(gekozenSong);
                    Console.WriteLine($"Liedje '{gekozenSong.Title}' toegevoegd aan afspeellijst '{gekozenPlaylist.Title}'.");
                }
                else if (keuze == "3")
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
                    var gekozenPlaylist = playlists[playlistIndex - 1];
                    var playables = gekozenPlaylist.ShowPlayables();
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
                            string artiesten = s.Artists != null && s.Artists.Count > 0
                                ? string.Join(", ", s.Artists.Select(a => a.Naam))
                                : "Onbekend";
                            Console.WriteLine($"{i + 1}. {s.Title} - Artiest(en): {artiesten}");
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
                    var teVerwijderen = playables[songIndex - 1];
                    gekozenPlaylist.Remove(teVerwijderen);
                    Console.WriteLine("Liedje verwijderd uit de afspeellijst.");
                }
                else if (keuze == "4")
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
                    var gekozenPlaylist = playlists[playlistIndex - 1];
                    var playables = gekozenPlaylist.ShowPlayables();
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
                            string artiesten = s.Artists != null && s.Artists.Count > 0
                                ? string.Join(", ", s.Artists.Select(a => a.Naam))
                                : "Onbekend";
                            Console.WriteLine($"{i + 1}. {s.Title} - Artiest(en): {artiesten}");
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
                    var teSpelen = playables[songIndex - 1];
                    client.CurrentlyPlaying = teSpelen;
                    teSpelen.Play();
                }
                else if (keuze == "5")
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
                    var gekozenPlaylist = playlists[playlistIndex - 1];
                    if (client.AllAlbums == null || client.AllAlbums.Count == 0)
                    {
                        Console.WriteLine("Er zijn geen albums beschikbaar.");
                        continue;
                    }
                    Console.WriteLine("Kies een album om toe te voegen:");
                    for (int i = 0; i < client.AllAlbums.Count; i++)
                    {
                        var album = client.AllAlbums[i];
                        string artiesten = album.Artists != null && album.Artists.Count > 0
                            ? string.Join(", ", album.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {album.Title} - Artiest(en): {artiesten}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int albumIndex) || albumIndex < 1 || albumIndex > client.AllAlbums.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var gekozenAlbum = client.AllAlbums[albumIndex - 1];
                    var albumSongs = gekozenAlbum.ShowPlayables();
                    foreach (var playable in albumSongs)
                        gekozenPlaylist.Add(playable);
                    Console.WriteLine($"Alle liedjes van album '{gekozenAlbum.Title}' zijn toegevoegd aan afspeellijst '{gekozenPlaylist.Title}'.");
                }
                else if (keuze == "6")
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
                        string artiesten = song.Artists != null && song.Artists.Count > 0
                            ? string.Join(", ", song.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {song.Title} - Artiest(en): {artiesten}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > client.AllSongs.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var teSpelen = client.AllSongs[songIndex - 1];
                    client.CurrentlyPlaying = teSpelen;
                    teSpelen.Play();
                }
                else if (keuze == "7")
                {
                    if (client.CurrentlyPlaying is Song huidig)
                    {
                        string artiesten = huidig.Artists != null && huidig.Artists.Count > 0
                            ? string.Join(", ", huidig.Artists.ConvertAll(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"Speelt nu af: '{huidig.Title}' - Artiest(en): {artiesten} - Genre: {huidig.SongGenre} - Nog {huidig.Length} seconden");
                    }
                    else
                        Console.WriteLine("Er wordt momenteel geen nummer afgespeeld.");
                }
                else if (keuze == "8")
                {
                    if (client.CurrentlyPlaying is Song huidig)
                        huidig.Pause();
                    else
                        Console.WriteLine("Er wordt momenteel geen nummer afgespeeld.");
                }
                else if (keuze == "9")
                {
                    if (client.CurrentlyPlaying is Song huidig)
                        huidig.Play();
                    else
                        Console.WriteLine("Er wordt momenteel geen nummer afgespeeld.");
                }
                else if (keuze == "10") // Wissel van gebruiker
                {
                    Console.WriteLine("\nKies een gebruiker om mee in te loggen:");
                    for (int i = 0; i < gebruikers.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {gebruikers[i].Naam}");
                    }

                    if (!int.TryParse(Console.ReadLine(), out int userIndex) || userIndex < 1 || userIndex > gebruikers.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }

                    var selectedUser = gebruikers[userIndex - 1] as SuperUser;
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
                else if (keuze == "11")
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
                    var gekozenPlaylist = playlists[playlistIndex - 1];

                    // Controleer of de afspeellijst items bevat
                    var playables = gekozenPlaylist.ShowPlayables();
                    if (playables.Count == 0)
                    {
                        Console.WriteLine("Deze afspeellijst bevat geen liedjes.");
                        continue;
                    }

                    Console.WriteLine("1. Speel op volgorde af");
                    Console.WriteLine("2. Speel in willekeurige volgorde af");
                    var subkeuze = Console.ReadLine();

                    // Bepaal de afspeellijst (normaal of shuffled)
                    List<iPlayable> afspeellijst;
                    if (subkeuze == "1")
                    {
                        afspeellijst = playables;
                        Console.WriteLine("Normale afspeelvolgorde:");
                    }
                    else if (subkeuze == "2")
                    {
                        afspeellijst = new List<iPlayable>(playables);
                        var rnd = new Random();
                        int n = afspeellijst.Count;

                        // Fisher-Yates shuffle algoritme
                        while (n > 1)
                        {
                            n--;
                            int k = rnd.Next(n + 1);
                            (afspeellijst[n], afspeellijst[k]) = (afspeellijst[k], afspeellijst[n]);
                        }
                        Console.WriteLine("Willekeurige afspeelvolgorde:");
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }

                    // Toon de afspeelvolgorde
                    for (int i = 0; i < afspeellijst.Count; i++)
                    {
                        if (afspeellijst[i] is Song s)
                            Console.WriteLine($"{i + 1}. {s.Title} - {string.Join(", ", s.Artists.Select(a => a.Naam))}");
                        else
                            Console.WriteLine($"{i + 1}. {afspeellijst[i]}");
                    }

                    // Start het afspelen
                    int currentIndex = 0;
                    bool stop = false;
                    bool isPaused = false;

                    while (!stop && afspeellijst.Count > 0 && currentIndex < afspeellijst.Count)
                    {
                        var huidig = afspeellijst[currentIndex];
                        client.CurrentlyPlaying = huidig;

                        // Toon duidelijke informatie over het huidige nummer en waar we zijn in de afspeellijst
                        Console.WriteLine("\n---------------------------------------------------");
                        Console.WriteLine($"HUIDIGE POSITIE: {currentIndex + 1} van {afspeellijst.Count}");

                        if (huidig is Song s)
                        {
                            string artiesten = string.Join(", ", s.Artists.Select(a => a.Naam));
                            Console.WriteLine($"NU SPEELT: '{s.Title}' - Artiest(en): {artiesten} - Genre: {s.SongGenre} - Duur: {s.Length} sec");

                            if (currentIndex < afspeellijst.Count - 1 && afspeellijst[currentIndex + 1] is Song nextSong)
                            {
                                string nextArtists = string.Join(", ", nextSong.Artists.Select(a => a.Naam));
                                Console.WriteLine($"VOLGENDE: '{nextSong.Title}' - {nextArtists}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"NU SPEELT: {huidig}");
                        }
                        Console.WriteLine("---------------------------------------------------");

                        // Speel het nummer af als niet gepauzeerd
                        if (!isPaused)
                        {
                            huidig.Play();
                        }

                        // Toon besturingsknoppen
                        Console.WriteLine("\nBediening:");
                        Console.WriteLine("1. Volgende");
                        Console.WriteLine("2. Vorige");
                        Console.WriteLine("3. Pauze");
                        Console.WriteLine("4. Verder spelen");
                        Console.WriteLine("5. Stoppen");
                        Console.Write("Kies een optie: ");
                        var afspeelKeuze = Console.ReadLine();

                        switch (afspeelKeuze)
                        {
                            case "1":
                                if (currentIndex < afspeellijst.Count - 1)
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
                                if (huidig is Song song)
                                {
                                    song.Pause();
                                    isPaused = true;
                                }
                                break;
                            case "4":
                                if (huidig is Song currentSong)
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
                else if (keuze == "12")
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
                        string artiesten = album.Artists != null && album.Artists.Count > 0
                            ? string.Join(", ", album.Artists.Select(a => a.Naam))
                            : "Onbekend";
                        Console.WriteLine($"{i + 1}. {album.Title} - Artiest(en): {artiesten}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int albumIndex) || albumIndex < 1 || albumIndex > client.AllAlbums.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var gekozenAlbum = client.AllAlbums[albumIndex - 1];
                    var playables = gekozenAlbum.ShowPlayables();
                    if (playables.Count == 0)
                    {
                        Console.WriteLine("Dit album bevat geen liedjes.");
                        continue;
                    }

                    Console.WriteLine("1. Speel op volgorde af");
                    Console.WriteLine("2. Speel in willekeurige volgorde af");
                    var subkeuze = Console.ReadLine();

                    List<iPlayable> afspeellijst;
                    if (subkeuze == "1")
                    {
                        afspeellijst = playables;
                        Console.WriteLine("Normale afspeelvolgorde:");
                    }
                    else if (subkeuze == "2")
                    {
                        afspeellijst = new List<iPlayable>(playables);
                        var rnd = new Random();
                        int n = afspeellijst.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rnd.Next(n + 1);
                            (afspeellijst[n], afspeellijst[k]) = (afspeellijst[k], afspeellijst[n]);
                        }
                        Console.WriteLine("Willekeurige afspeelvolgorde:");
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }

                    for (int i = 0; i < afspeellijst.Count; i++)
                    {
                        if (afspeellijst[i] is Song s)
                            Console.WriteLine($"{i + 1}. {s.Title} - {string.Join(", ", s.Artists.Select(a => a.Naam))}");
                        else
                            Console.WriteLine($"{i + 1}. {afspeellijst[i]}");
                    }

                    int currentIndex = 0;
                    bool stop = false;
                    bool isPaused = false;

                    while (!stop && afspeellijst.Count > 0 && currentIndex < afspeellijst.Count)
                    {
                        var huidig = afspeellijst[currentIndex];
                        client.CurrentlyPlaying = huidig;

                        Console.WriteLine("\n--------------------------------------------------");
                        Console.WriteLine($"HUIDIGE POSITIE: {currentIndex + 1} van {afspeellijst.Count}");

                        if (huidig is Song s)
                        {
                            string artiesten = string.Join(", ", s.Artists.Select(a => a.Naam));
                            Console.WriteLine($"NU SPEELT: '{s.Title}' - Artiest(en): {artiesten} - Genre: {s.SongGenre} - Duur: {s.Length} sec");

                            if (currentIndex < afspeellijst.Count - 1 && afspeellijst[currentIndex + 1] is Song nextSong)
                            {
                                string nextArtists = string.Join(", ", nextSong.Artists.Select(a => a.Naam));
                                Console.WriteLine($"VOLGENDE: '{nextSong.Title}' - {nextArtists}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"NU SPEELT: {huidig}");
                        }
                        Console.WriteLine("--------------------------------------------------");

                        if (!isPaused)
                        {
                            huidig.Play();
                        }

                        Console.WriteLine("\nBediening:");
                        Console.WriteLine("1. Volgende");
                        Console.WriteLine("2. Vorige");
                        Console.WriteLine("3. Pauze");
                        Console.WriteLine("4. Verder spelen");
                        Console.WriteLine("5. Stoppen");
                        Console.Write("Kies een optie: ");
                        var afspeelKeuze = Console.ReadLine();

                        switch (afspeelKeuze)
                        {
                            case "1":
                                if (currentIndex < afspeellijst.Count - 1)
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
                                if (huidig is Song song)
                                {
                                    song.Pause();
                                    isPaused = true;
                                }
                                break;
                            case "4":
                                if (huidig is Song currentSong)
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
                else if (keuze == "13") // Nieuwe optie: Vrienden beheren
                {
                    Console.WriteLine("\n===== VRIENDEN BEHEREN =====");
                    Console.WriteLine("1. Toon mijn vrienden");
                    Console.WriteLine("2. Vriendschapsverzoek sturen");
                    Console.WriteLine("3. Vriendschapsverzoeken bekijken");
                    Console.WriteLine("4. Vriendschapsverzoek accepteren");
                    Console.WriteLine("5. Vriendschapsverzoek weigeren");
                    Console.WriteLine("6. Verwijder een vriend");
                    Console.WriteLine("0. Terug naar hoofdmenu");
                    Console.Write("Kies een optie: ");

                    var vriendenKeuze = Console.ReadLine();

                    // 1. Toon mijn vrienden
                    if (vriendenKeuze == "1")
                    {
                        var vrienden = client.ActiveUser.Friends;
                        if (vrienden.Count == 0)
                        {
                            Console.WriteLine("Je hebt nog geen vrienden.");
                            continue;
                        }

                        Console.WriteLine("\nJouw vrienden:");
                        for (int i = 0; i < vrienden.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {vrienden[i].Naam}");
                        }
                    }

                    // 2. Vriendschapsverzoek sturen
                    else if (vriendenKeuze == "2")
                    {
                        // Toon alle gebruikers behalve jezelf
                        var potentieleVrienden = gebruikers.Where(g => g.Naam != client.ActiveUser.Naam).ToList();

                        if (potentieleVrienden.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen andere gebruikers om vriendschapsverzoeken naar te sturen.");
                            continue;
                        }

                        Console.WriteLine("\nKies een gebruiker om een vriendschapsverzoek naar te sturen:");
                        for (int i = 0; i < potentieleVrienden.Count; i++)
                        {
                            var isAlreadyFriend = client.ActiveUser.Friends.Any(f => f.Naam == potentieleVrienden[i].Naam);
                            var status = isAlreadyFriend ? " (Al vriend)" : "";

                            // Controleer of er al een verzoek is gestuurd naar deze gebruiker
                            var hasPendingRequest = friendRequests[potentieleVrienden[i].Naam].Any(p => p.Naam == client.ActiveUser.Naam);
                            if (hasPendingRequest)
                            {
                                status = " (Verzoek verstuurd)";
                            }

                            Console.WriteLine($"{i + 1}. {potentieleVrienden[i].Naam}{status}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int friendIndex) || friendIndex < 1 || friendIndex > potentieleVrienden.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedFriend = potentieleVrienden[friendIndex - 1];

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
                    else if (vriendenKeuze == "3")
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
                    else if (vriendenKeuze == "4")
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
                    else if (vriendenKeuze == "5")
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
                    else if (vriendenKeuze == "6")
                    {
                        var vrienden = client.ActiveUser.Friends;

                        if (vrienden.Count == 0)
                        {
                            Console.WriteLine("Je hebt nog geen vrienden om te verwijderen.");
                            continue;
                        }

                        Console.WriteLine("\nKies een vriend om te verwijderen:");
                        for (int i = 0; i < vrienden.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {vrienden[i].Naam}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int friendIndex) || friendIndex < 1 || friendIndex > vrienden.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            continue;
                        }

                        var selectedFriend = vrienden[friendIndex - 1];

                        // Wederzijds vriendschappen verwijderen
                        client.ActiveUser.Friends.Remove(selectedFriend);
                        selectedFriend.Friends.Remove(client.ActiveUser);

                        Console.WriteLine($"Je bent niet meer bevriend met {selectedFriend.Naam}.");
                    }

                    // 0. Terug naar hoofdmenu
                    else if (vriendenKeuze == "0")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze, probeer opnieuw.");
                    }
                }
                else if (keuze == "0")
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
