using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using DnDNotesAppVue.Server.Models;

namespace DnDNotesAppVue.Services;

public class MongoDBService
{

    private readonly IMongoCollection<Note> _noteCollection;

    public MongoDBService(IOptions<NotesDatabase> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _noteCollection = database.GetCollection<Note>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Note>> GetAsync()
    {
        return await _noteCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task CreateAsync(Note note) 
    {
        await _noteCollection.InsertOneAsync(note);
        return;
    }
    public async Task DeleteAsync(string id) 
    {
        FilterDefinition<Note> filter = Builders<Note>.Filter.Eq("Id", id);
        await _noteCollection.DeleteOneAsync(filter);
    }

}