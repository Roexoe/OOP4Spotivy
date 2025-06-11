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
            var song1 = new Song("Song A", new List<Artist>(), 180, Genres.Pop);
            var song2 = new Song("Song B", new List<Artist>(), 200, Genres.Rock);
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
                Console.WriteLine("0. Afsluiten");
                Console.Write("Kies een optie: ");
                string keuze = Console.ReadLine();

                if (keuze == "1")
                {
                    Console.Write("Geef een naam voor de nieuwe afspeellijst: ");
                    string naam = Console.ReadLine();
                    client.CreatePlaylist(naam);
                }
                else if (keuze == "2")
                {
                    // Playlists van de gebruiker
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

                    // Beschikbare liedjes
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

                    // Liedje toevoegen aan lijst
                    gekozenPlaylist.Add(gekozenSong);
                    Console.WriteLine($"Liedje '{gekozenSong.Title}' toegevoegd aan afspeellijst '{gekozenPlaylist.Title}'.");
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
