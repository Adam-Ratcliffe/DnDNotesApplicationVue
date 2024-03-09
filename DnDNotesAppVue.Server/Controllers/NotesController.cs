using Microsoft.AspNetCore.Mvc;
using DnDNotesAppVue.Server.Models;
using System.IO;
using DnDNotesAppVue.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DnDNotesAppVue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        const string filename = "test.txt";
        MongoDBService _mongoDBService;
        List<Note>? notes;

        public NotesController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        #region
        /*[HttpGet("getall")]
        public List<Note> LoadAllNotes()
        {
            StreamReader streamReader = new StreamReader(filename);
            notes = new List<Note>();
            string[] lines = streamReader.ReadToEnd().Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                notes.Add(new Note(i, lines[i]));
            }

            return notes;
        }

        // GET: api/<NotesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NotesController>/5
        [HttpGet("{id}")]
        public Note Get(int id)
        {
            return new Note("test");
        }

        // POST api/<NotesController>
        [HttpPost]
        public void Post([FromBody] Note value)
        {
            if (!System.IO.File.Exists(filename))
            {
                System.IO.File.Create(filename);
            }
            StreamWriter writer = new StreamWriter(filename);
            

            writer.WriteLine(value.Name);
            writer.Close();
        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
        #endregion

        [HttpGet("getall")]
        public async Task<List<Note>> GetAll() 
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            await _mongoDBService.CreateAsync(note);
            return CreatedAtAction(nameof(GetAll), new { id = note.Id }, note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}
