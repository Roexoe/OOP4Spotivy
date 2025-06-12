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
            var song2 = new Song("Sad Song", new List<Artist> { artist2 }, 200, Genres.Rock);
            songs.Add(song1);
            songs.Add(song2);

            // Voorbeeldalbums met liedjes
            var albumSongs1 = new List<Song>
            {
                new Song("Album 1 Track 1", new List<Artist> { artist1 }, 210, Genres.Pop),
                new Song("Album 1 Track 2", new List<Artist> { artist1 }, 195, Genres.Pop),
                new Song("Album 1 Track 3", new List<Artist> { artist1, artist2 }, 180, Genres.Rock)
            };

            var albumSongs2 = new List<Song>
            {
                new Song("Album 2 Track 1", new List<Artist> { artist2 }, 220, Genres.Rock),
                new Song("Album 2 Track 2", new List<Artist> { artist2 }, 190, Genres.Rock),
                new Song("Album 2 Track 3", new List<Artist> { artist2, artist3 }, 250, Genres.Pop)
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
                Console.WriteLine("\n===== SPOTIVY MENU =====");
                Console.WriteLine("1. Maak een nieuwe afspeellijst aan");
                Console.WriteLine("2. Voeg een liedje toe aan een afspeellijst");
                Console.WriteLine("3. Verwijder een liedje uit een afspeellijst");
                Console.WriteLine("4. Speel een liedje uit een afspeellijst af");
                Console.WriteLine("5. Voeg een album toe aan een afspeellijst");
                Console.WriteLine("6. Speel een liedje uit de algemene lijst af");
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
                    teSpelen.Play();
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
