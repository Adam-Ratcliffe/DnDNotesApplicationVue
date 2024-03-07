using Microsoft.AspNetCore.Mvc;
using DnDNotesAppVue.Server.Models;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DnDNotesAppVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        const string filename = "test.txt";
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
        }
    }
}
