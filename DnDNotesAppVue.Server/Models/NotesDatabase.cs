namespace DnDNotesAppVue.Server.Models
{
    public class NotesDatabase
    {
        public string ConnectionURI { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;
        public string CharacterCollection { get; set; } = null!;
    }
}
