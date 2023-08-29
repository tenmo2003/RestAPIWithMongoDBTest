using MongoAPI.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace MongoAPI.Services;

public class MongoDBService {
    private readonly IMongoCollection<Test> _testCollection;

    public MongoDBService(IOptions<MongoDBSettings> settings) {
        MongoClient client = new(settings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(settings.Value.DatabaseName);
        _testCollection = database.GetCollection<Test>(settings.Value.CollectionName);
    }

    public async Task CreateAsync(Test test) {
        await _testCollection.InsertOneAsync(test);
        return;
    }

    public async Task<List<Test>> GetAsync() {
        return await _testCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateAsync(string id, Test test) {
        await _testCollection.ReplaceOneAsync(x => x.Id == id, test);
    }

    public async Task DeleteAsync(string id) {
        await _testCollection.DeleteOneAsync(x => x.Id == id);
    }
}
