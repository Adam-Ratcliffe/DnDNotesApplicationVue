using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace DnDNotesAppVue.Server.Models
{
    public record class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public Note()
        {
            this.Id = null;
            this.Name = null;
        }

        public Note(string name)
        {
            this.Name = name;
        }

        public Note(int id, string name)
        {
            this.Id = id.ToString();
            this.Name = name;
        }
    }
}
