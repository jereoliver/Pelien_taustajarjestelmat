using System;
using System.Collections.Generic;
using System.Linq;


namespace teht2
{
    class Program
    {
        static void Main(string[] args)
        {
            // InstantiateMillionPlayersAndSearchForDublicateGuids();           // tehtava 1
            // CreatePlayerWithVariousItemsAndGetBestItem();                    // tehtava 2

            Player player = CreateAPlayerWithItems();                   // tätä käytetään tehtävissä 3,4,5,6

            // Item[] arrayOfItems = GetItems(player);                         // tehtava 3
            // PrintArray(arrayOfItems);
            // arrayOfItems = GetItemsWIthLINQ(player);
            // PrintArray(arrayOfItems);

            // Item firstItem = FirstItem(player);                              // tehtava 4
            // PrintItem(firstItem);
            // firstItem = FirstItemWithLINQ(player);
            // PrintItem(firstItem);

            // Action<Item> x = PrintItem;                                    // tehtava 5
            // ProcessEachItem(player, x);

            // Action<Item> z = x => Console.WriteLine(x.Id + " " + x.Level); // tehtava 6
            // ProcessEachItem(player, z);

            DoGenericsAssignment();                                            // tehtava 7 
        }

        private static void DoGenericsAssignment()
        {
            List<Player> players = Create1000Players();
            Game<Player> newGame = new Game<Player>(players);
            IPlayer[] top10 = newGame.GetTop10Players();
            PrintTop10(top10);
            List<PlayerForAnotherGame> playersForAnotherGame = Create1000PlayersForAnotherGame();
            Game<PlayerForAnotherGame> newGameWithOtherPlayers = new Game<PlayerForAnotherGame>(playersForAnotherGame);
            top10 = newGameWithOtherPlayers.GetTop10Players();
            PrintTop10(top10);
        }

        private static List<Player> Create1000Players()
        {
            int pelaajien_lkm = 1000;
            List<Player> players = new List<Player>();
            Random rnd = new Random();

            for (int i = 0; i < pelaajien_lkm; i++)
            {
                players.Add(new Player());
                players[i].Id = Guid.NewGuid();
                players[i].Score = rnd.Next(0, 500);
            }
            return players;
        }

        private static List<PlayerForAnotherGame> Create1000PlayersForAnotherGame()
        {
            int pelaajien_lkm = 1000;
            List<PlayerForAnotherGame> players = new List<PlayerForAnotherGame>();
            Random rnd = new Random();

            for (int i = 0; i < pelaajien_lkm; i++)
            {
                players.Add(new PlayerForAnotherGame());
                players[i].Id = Guid.NewGuid();
                players[i].Score = rnd.Next(1000, 10000);
            }
            return players;
        }

        private static void PrintTop10(IPlayer[] lista)
        {
            foreach (IPlayer player in lista)
            {
                Console.WriteLine(player.Score);
            }
            Console.WriteLine(" ");
        }

        private static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach (Item item in player.Items)
            {
                process(item);
            }
        }

        private static void PrintItem(Item item)
        {
            Console.WriteLine(item.Id + " " + item.Level);
        }

        private static Item FirstItem(Player player)
        {
            if (player.Items[0] != null)
            {
                Item firstItem = player.Items[0];
                return firstItem;
            }
            else
            {
                return null;
            }
        }

        private static Item FirstItemWithLINQ(Player player)
        {
            Item firstItem = player.Items.First();
            if (firstItem != null)
                return firstItem;
            else
                return null;
        }

        private static void PrintArray(Item[] arrayOfItems)
        {
            foreach (Item item in arrayOfItems)
            {
                Console.WriteLine(item.Id + " lvl: " + item.Level);
            }
            Console.WriteLine("");
        }

        private static Item[] GetItems(Player player)
        {
            int x = player.Items.Count;
            Item[] arrayOfItems = new Item[x];

            for (int i = 0; i < x; i++)
            {
                arrayOfItems[i] = player.Items[i];
            }
            return arrayOfItems;
        }

        private static Item[] GetItemsWIthLINQ(Player player)
        {
            Item[] arrayOfItems = player.Items.ToArray();
            return arrayOfItems;
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

        private static Player CreateAPlayerWithItems()
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
            return player;
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
