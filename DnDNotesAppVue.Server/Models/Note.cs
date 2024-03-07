namespace DnDNotesAppVue.Server.Models
{
    public record class Note
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public Note(string name)
        {
            this.Name = name;
        }
    }
}
