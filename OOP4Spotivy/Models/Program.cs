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
            var gebruikers = new List<Person> { hoofdgebruiker };
            var albums = new List<Album>();
            var songs = new List<Song>();

            // Voorbeeldartiesten
            var artist1 = new Artist("The Beatles", new List<Album>());
            var artist2 = new Artist("Queen", new List<Album>());
            var artist3 = new Artist("Michael Jackson", new List<Album>());

            // Voorbeeldliedjes
            var song1 = new Song("Happy Song", new List<Artist> { artist1 }, 180, Genres.Pop);
            var song2 = new Song("Sad Song", new List<Artist> { artist2 }, 120, Genres.Rock);
            songs.Add(song1);
            songs.Add(song2);

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

            var album1 = new Album(new List<Artist> { artist1 }, "Greatest Hits Vol. 1", albumSongs1);
            var album2 = new Album(new List<Artist> { artist2, artist3 }, "Rock Classics", albumSongs2);
            albums.Add(album1);
            albums.Add(album2);

            songs.AddRange(albumSongs1);
            songs.AddRange(albumSongs2);

            var client = new Client(gebruikers, albums, songs)
            {
                ActiveUser = hoofdgebruiker
            };
            client.AllSongs = songs;
            client.AllAlbums = albums;

            while (true)
            {
                Console.WriteLine("1. Maak een nieuwe afspeellijst aan");
                Console.WriteLine("2. Voeg een liedje toe aan een afspeellijst");
                Console.WriteLine("3. Verwijder een liedje uit een afspeellijst");
                Console.WriteLine("4. Speel een liedje uit een afspeellijst af");
                Console.WriteLine("5. Voeg een album toe aan een afspeellijst");
                Console.WriteLine("6. Speel een liedje uit de algemene lijst af");
                Console.WriteLine("7. Toon huidig afgespeeld nummer");
                Console.WriteLine("8. Pauzeer huidig nummer");
                Console.WriteLine("9. Speel huidig nummer verder");
                Console.WriteLine("11. Speel een afspeellijst af (volgorde of shuffle)");
                Console.WriteLine("12. Speel een album af (volgorde of shuffle)");
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
                        Console.WriteLine($"{i + 1}. {client.AllSongs[i].Title}");
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
                        Console.WriteLine($"{i + 1}. {(playables[i] is Song s ? s.Title : "[Niet een los liedje]")}");
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
                        Console.WriteLine($"{i + 1}. {(playables[i] is Song s ? s.Title : "[Niet een los liedje]")}");
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
                        Console.WriteLine($"{i + 1}. {client.AllAlbums[i].Title}");
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
                        Console.WriteLine($"{i + 1}. {client.AllSongs[i].Title}");
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
                        Console.WriteLine($"{i + 1}. {client.AllAlbums[i].Title}");
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
