using System;
using System.Collections.Generic;


namespace teht2
{
    class Program
    {
        static void Main(string[] args)
        {
            // InstantiateMillionPlayersAndSearchForDublicateGuids();   // tehtava 1
            // CreatePlayerWithVariousItemsAndGetBestItem();            // tehtava 2


        }

        private static void CreatePlayerWithVariousItemsAndGetBestItem()
        {
            Player player = new Player();
            Random rnd = new Random();
            List<Item> itemit = new List<Item>();
            for (int i = 0; i < 10; i++) // luodaan 10 itemiä
            {
                Item newItem = new Item();
                newItem.Id = Guid.NewGuid();
                newItem.Level = rnd.Next(1, 100); // arpoo jokaiselle itemille leveliksi jotain väliltä 1-100
                itemit.Add(newItem);
            }
            player.Items = itemit;

            Item bestItem = player.GetHighestLevelItem();
            Console.WriteLine(bestItem.Id + " " + bestItem.Level);
        }

        private static void InstantiateMillionPlayersAndSearchForDublicateGuids()
        {
            int pelaajien_lkm = 10000;
            Player[] players = new Player[pelaajien_lkm];
            Guid[] ids = new Guid[pelaajien_lkm];

            for (int i = 0; i < pelaajien_lkm; i++)
            {
                players[i] = new Player();
                players[i].Id = Guid.NewGuid();
                if (i > 0)
                {
                    // if (i == 300)
                    //     players[i].Id = players[244].Id; // Tämä oli tälläinen jolla testasin että jos dataan pakkosyöttää dublikaatin niin tunnistaahan ohjelma sen ja tunnisti.

                    int pos = Array.IndexOf(ids, players[i].Id);
                    if (pos == -1) // eli jos id:tä ei löydy jo joltain pelaajalta
                    {
                        ids[i] = players[i].Id;

                    }
                    else
                    {
                        Console.WriteLine("Dublikaatti löytynyt!");
                    }

                }
                else
                {
                    ids[i] = players[i].Id; // asetetaan listan ensimmäinen arvo aina players-listasta, ei tehdä tarkistusta koska edellistä alkiota listassa ei ole. 
                }
            }
        }
    }
}
