using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

public class PlayerListHolder
{
    public List<Player> listOfPlayers = new List<Player>();
}

public class FileRepository : IRepository
{


    public async Task<Player> Create(Player player)
    {
        PlayerListHolder players = await ReadFile();
        players.listOfPlayers.Add(player);
        File.WriteAllText("game-dev.txt", JsonConvert.SerializeObject(players));
        return player;
    }


    public async Task<Player> Delete(Guid id)
    {
        PlayerListHolder players = await ReadFile();

        Player playerToDelete = new Player();
        for (int i = 0; i < players.listOfPlayers.Count; i++)
        {
            if (players.listOfPlayers[i].Id == id)
            {
                playerToDelete = players.listOfPlayers[i];
                players.listOfPlayers.RemoveAt(i);
                File.WriteAllText("game-dev.txt", JsonConvert.SerializeObject(players));
                return playerToDelete;
            }
        }

        return null;
    }

    public async Task<Player> Get(Guid id)
    {
        PlayerListHolder players = await ReadFile();

        Player playerToGet = new Player();
        foreach (Player player in players.listOfPlayers)
        {
            if (player.Id == id)
            {
                playerToGet = player;
                return playerToGet;
            }
        }
        return null;
    }

    public async Task<Player[]> GetAll()
    {
        PlayerListHolder players = await ReadFile();
        return players.listOfPlayers.ToArray();
    }

    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        PlayerListHolder players = await ReadFile();
        Player playerToModify = new Player();
        foreach (Player oldplayer in players.listOfPlayers)
        {
            if (oldplayer.Id == id)
            {
                oldplayer.Score = player.Score;
                playerToModify = oldplayer;
                File.WriteAllText("game-dev.txt", JsonConvert.SerializeObject(players));
            }
        }
        return playerToModify;
    }
    public async Task<PlayerListHolder> ReadFile()
    {
        var players = new PlayerListHolder();
        string json = await File.ReadAllTextAsync("game-dev.txt");

        if (File.ReadAllText("game-dev.txt").Length != 0)
        {
            return JsonConvert.DeserializeObject<PlayerListHolder>(json);
        }
        return players;
    }
}

