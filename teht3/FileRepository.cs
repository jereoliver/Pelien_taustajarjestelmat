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

    public async Task<Item> CreateItem(Guid playerId, Item item)
    {
        PlayerListHolder players = await ReadFile();
        Player playerToGetItem = new Player();
        for (int i = 0; i < players.listOfPlayers.Count; i++)
        {
            if (players.listOfPlayers[i].Id == playerId)
            {
                playerToGetItem = players.listOfPlayers[i];
            }
        }
        if (playerToGetItem.itemList == null)
            playerToGetItem.itemList = new List<Item>();
        playerToGetItem.itemList.Add(item);
        File.WriteAllText("game-dev.txt", JsonConvert.SerializeObject(players));
        return item;
    }

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        PlayerListHolder players = await ReadFile();
        Item itemToGet = new Item();
        for (int i = 0; i < players.listOfPlayers.Count; i++)
        {
            if (players.listOfPlayers[i].Id == playerId)
            {
                for (int j = 0; j < players.listOfPlayers[i].itemList.Count; j++)
                {
                    if (players.listOfPlayers[i].itemList[j].itemId == itemId)
                    {
                        itemToGet = players.listOfPlayers[i].itemList[j];
                        return itemToGet;
                    }
                }
            }
        }
        return null;
    }
    public async Task<Item> DeleteItem(Guid playerId, Guid itemId)
    {
        PlayerListHolder players = await ReadFile();
        Item itemToRemove = new Item();
        for (int i = 0; i < players.listOfPlayers.Count; i++)
        {
            if (players.listOfPlayers[i].Id == playerId)
            {
                for (int j = 0; j < players.listOfPlayers[i].itemList.Count; j++)
                {
                    if (players.listOfPlayers[i].itemList[j].itemId == itemId)
                    {
                        itemToRemove = players.listOfPlayers[i].itemList[j];
                        players.listOfPlayers[i].itemList.RemoveAt(j);
                        File.WriteAllText("game-dev.txt", JsonConvert.SerializeObject(players));
                        return itemToRemove;
                    }
                }
            }
        }
        return null;
    }

    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        PlayerListHolder players = await ReadFile();
        for (int i = 0; i < players.listOfPlayers.Count; i++)
        {
            if (players.listOfPlayers[i].Id == playerId)
            {
                return players.listOfPlayers[i].itemList.ToArray();
            }
        }
        return null;
    }

    public async Task<Item> UpdateItem(Guid playerId, Guid itemId, ModifiedItem item)
    {
        PlayerListHolder players = await ReadFile();

        for (int i = 0; i < players.listOfPlayers.Count; i++)
        {
            if (players.listOfPlayers[i].Id == playerId)
            {
                for (int j = 0; j < players.listOfPlayers[i].itemList.Count; j++)
                {
                    if (players.listOfPlayers[i].itemList[j].itemId == itemId)
                    {
                        players.listOfPlayers[i].itemList[j].level = item.level; // tässä nyt on päätetty että updateitemilla muutetaan itemin leveliä
                        File.WriteAllText("game-dev.txt", JsonConvert.SerializeObject(players));
                        return players.listOfPlayers[i].itemList[j];
                    }
                }
            }
        }
        return null;
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

