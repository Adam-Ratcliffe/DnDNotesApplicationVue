using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using DnDNotesAppVue.Server.Models;
using System.IO;

namespace DnDNotesAppVue.Services;

public class MongoDBService
{

    private readonly IMongoCollection<Note> _noteCollection;
    private readonly IMongoCollection<Character> _characterCollection;
    private const string _passwordFileName = "password.txt";

    public MongoDBService(IOptions<NotesDatabase> mongoDBSettings)
    {
        StreamReader sr = new StreamReader(_passwordFileName);

        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI.Replace("#", sr.ReadLine()));
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _noteCollection = database.GetCollection<Note>(mongoDBSettings.Value.CollectionName);
        _characterCollection = database.GetCollection<Character>(mongoDBSettings.Value.CharacterCollection);

        sr.Close();
    }
    #region Generic Notes Methods
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
    #endregion

    #region Character Notes Methods
    public async Task<List<Character>> GetAllCharactersAsync()
    {
        return await _characterCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Character> GetCharacter(string id)
    {
        FilterDefinition<Character> filter = Builders<Character>.Filter.Eq("Id", id);
        return await _characterCollection.Find(filter).FirstAsync();
    }

    public async Task UpdateCharacter(Character character)
    {
        FilterDefinition<Character> filter = Builders<Character>.Filter.Eq("Id", character.Id);
        await _characterCollection.ReplaceOneAsync(filter, character);
    }

    public async Task CreateCharacterAsync(Character character)
    {
        await _characterCollection.InsertOneAsync(character);
        return;
    }

    public async Task DeleteCharacterAsync(string id)
    {
        FilterDefinition<Character> filter = Builders<Character>.Filter.Eq("Id", id);
        await _characterCollection.DeleteOneAsync(filter);
    }
    #endregion
}