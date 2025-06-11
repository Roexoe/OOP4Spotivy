using System;
using System.Collections.Generic;

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

            // Voorbeeldliedjes
            var song1 = new Song("Happy Song", new List<Artist>(), 180, Genres.Pop);
            var song2 = new Song("Sad Song", new List<Artist>(), 200, Genres.Rock);
            songs.Add(song1);
            songs.Add(song2);

            var client = new Client(gebruikers, albums, songs)
            {
                ActiveUser = hoofdgebruiker
            };
            client.AllSongs = songs; // Zorg dat AllSongs gevuld is

            while (true)
            {
                Console.WriteLine("1. Maak een nieuwe afspeellijst aan");
                Console.WriteLine("2. Voeg een liedje toe aan een afspeellijst");
                Console.WriteLine("3. Verwijder een liedje uit een afspeellijst");
                Console.WriteLine("4. Speel een liedje uit een afspeellijst af");
                Console.WriteLine("0. Afsluiten");
                Console.Write("Kies een optie: ");
                string keuze = Console.ReadLine();


                // Keuze 1: Maak een nieuwe afspeellijst aan


                if (keuze == "1")
                {
                    Console.Write("Geef een naam voor de nieuwe afspeellijst: ");
                    string naam = Console.ReadLine();
                    client.CreatePlaylist(naam);
                }


                // Keuze 2: Voeg een liedje toe


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
                    {
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    }
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
                        Console.WriteLine($"{i + 1}. {client.AllSongs[i].Title}");
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

                // Keuze 3: Verwijder een liedje uit je afspeellijst


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
                    {
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    }
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
                        if (playables[i] is Song song)
                            Console.WriteLine($"{i + 1}. {song.Title}");
                        else
                            Console.WriteLine($"{i + 1}. [Niet een los liedje]");
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


                // KEUZE 4: Speel een liedje uit een afspeellijst af



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
                    {
                        Console.WriteLine($"{i + 1}. {playlists[i].Title}");
                    }
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
                        if (playables[i] is Song song)
                            Console.WriteLine($"{i + 1}. {song.Title}");
                        else
                            Console.WriteLine($"{i + 1}. [Niet een los liedje]");
                    }
                    if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > playables.Count)
                    {
                        Console.WriteLine("Ongeldige keuze.");
                        continue;
                    }
                    var teSpelen = playables[songIndex - 1];
                    teSpelen.Play();
                }

                else if (keuze == "0")
                {
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}
