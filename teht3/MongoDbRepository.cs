using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Newtonsoft.Json;

public class MongoDbRepository : IRepository
{
    private readonly IMongoCollection<Player> _playerCollection;
    private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;
    public MongoDbRepository()
    {
        MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("game");
        _playerCollection = database.GetCollection<Player>("players");
        _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
    }
    public async Task<Player> Create(Player player)
    {
        await _playerCollection.InsertOneAsync(player);
        return player;
    }

    public async Task<Item> CreateItem(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();

        if (player == null)
        {
            throw new NotFoundException("Player not found.");
        }

        if (player.itemList == null)
            player.itemList = new List<Item>();
        player.itemList.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }

    public async Task<Player> Delete(Guid id)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
        return await _playerCollection.FindOneAndDeleteAsync(filter);
    }

    public async Task<Item> DeleteItem(Guid playerId, Guid itemId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        Item itemToRemove = new Item();

        for (int j = 0; j < player.itemList.Count; j++)
        {
            if (player.itemList[j].itemId == itemId)
            {
                itemToRemove = player.itemList[j];
                player.itemList.RemoveAt(j);
                await _playerCollection.ReplaceOneAsync(filter, player);
                return itemToRemove;
            }
        }
        return null;
    }


    public async Task<Player> Get(Guid id)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
        return await _playerCollection.Find(filter).FirstAsync();
    }

    public async Task<Player[]> GetAll()
    {
        // List<Player> players = await _playerCollection.Find(new BsonDocument()).ToListAsync(); // ei toiminut, valitti guid:sta, kääntääkö mongodb/bson guidit binary-muotoon tms?
        var listofplayers = _bsonDocumentCollection.ToJson();
        List<Player> list = JsonConvert.DeserializeObject<List<Player>>(listofplayers);

        return list.ToArray();
    }

    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        return player.itemList.ToArray();
    }

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        for (int i = 0; i < player.itemList.Count; i++)
        {
            if (player.itemList[i].itemId == itemId)
                return player.itemList[i];
        }
        return null;
    }

    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
        Player player2 = await _playerCollection.Find(filter).FirstAsync();
        player2.Score = player.Score;
        await _playerCollection.ReplaceOneAsync(filter, player2);
        return player2;
    }

    public async Task<Item> UpdateItem(Guid playerId, Guid itemId, ModifiedItem item)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();

        for (int i = 0; i < player.itemList.Count; i++)
        {
            if (player.itemList[i].itemId == itemId)
            {
                player.itemList[i].level = item.level;
                await _playerCollection.ReplaceOneAsync(filter, player);
                return player.itemList[i];
            }

        }
        return null;
    }
}