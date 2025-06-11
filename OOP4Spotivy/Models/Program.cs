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

            var client = new Client(gebruikers, albums, songs)
            {
                ActiveUser = hoofdgebruiker
            };

            while (true)
            {
                Console.WriteLine("1. Maak een nieuwe afspeellijst aan");
                Console.WriteLine("0. Afsluiten");
                Console.Write("Kies een optie: ");
                string keuze = Console.ReadLine();

                if (keuze == "1")
                {
                    Console.Write("Geef een naam voor de nieuwe afspeellijst: ");
                    string naam = Console.ReadLine();
                    client.CreatePlaylist(naam);
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
