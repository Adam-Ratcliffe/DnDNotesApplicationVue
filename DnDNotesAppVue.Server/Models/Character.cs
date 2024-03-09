using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace DnDNotesAppVue.Server.Models
{
    public enum Race
    {
        Aasimar,
        Dragonborne,
        Drow,
        Dwarf,
        Githzerai,
        Gnome,
        Goblin,
        HighElf,
        Human,
        Tiefling,
        Warforged
    }
    public class Character
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id {  get; set; }
        public string? Name { get; set; }
        public string? Age {  get; set; }
        public string? Race { get; set; }
        public string? ProfileURL {  get; set; }
        public string? Occupation {  get; set; }
        public string? History {  get; set; }
        public string? PhysicalDescription {  get; set; }

        public Character()
        {
            Id = null;
            Name = null;
            Age = null;
        }
    }
}
