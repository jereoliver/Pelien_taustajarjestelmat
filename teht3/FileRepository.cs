using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;


public class FileRepository : IRepository
{
    public List<Player> listOfPlayers = new List<Player>();

    public async Task<Player> Create(Player player)
    {
        listOfPlayers.Add(player);
        string[] arrayOfStrings = new string[listOfPlayers.Count()];

        for (int i = 0; i < listOfPlayers.Count(); i++)
        {
            arrayOfStrings[i] = JsonConvert.SerializeObject(listOfPlayers[i]);
        }
        await File.WriteAllLinesAsync("game-dev.txt", arrayOfStrings);
        return player;
    }

    public async Task<Player> Delete(Guid id)
    {
        Player playerToDelete = new Player();
        foreach (Player player in listOfPlayers)
        {
            if (player.Id == id)
            {
                playerToDelete = player;
                // poista kyseinen pelaaja listasta
                // listOfPlayers.Remove()
            }
        }
        return playerToDelete;
    }

    public async Task<Player> Get(Guid id)
    {
        Player playerToGet = new Player();
        foreach (Player player in listOfPlayers)
        {
            if (player.Id == id)
            {
                playerToGet = player;

            }
        }
        return playerToGet;
    }

    public async Task<Player[]> GetAll()
    {
        return listOfPlayers.ToArray();
    }

    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        Player playerToModify = new Player();
        foreach (Player oldplayer in listOfPlayers)
        {
            if (oldplayer.Id == id)
            {

                oldplayer.Score = player.Score;
                playerToModify = oldplayer;
            }

            string[] arrayOfStrings = new string[listOfPlayers.Count()];

            for (int i = 0; i < listOfPlayers.Count(); i++)
            {
                arrayOfStrings[i] = JsonConvert.SerializeObject(listOfPlayers[i]);
            }
            await File.WriteAllLinesAsync("game-dev.txt", arrayOfStrings);
        }
        return playerToModify;
    }
}

