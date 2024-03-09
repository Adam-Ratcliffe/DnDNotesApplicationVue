using Microsoft.AspNetCore.Mvc;
using DnDNotesAppVue.Server.Models;
using DnDNotesAppVue.Services;


namespace DnDNotesAppVue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : Controller
    {
        MongoDBService _mongoDBService;
        public CharacterController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet("races")]
        public Array GetAllRaces()
        {
            return Enum.GetNames(typeof(Race));
        }

        [HttpGet("all")]
        public async Task<List<Character>> GetAllCharacters()
        {
            return await _mongoDBService.GetAllCharactersAsync();
        }

        [HttpGet("{id}")]
        public async Task<Character> GetCharacter(string id)
        {
            return await _mongoDBService.GetCharacter(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] Character character)
        {
            await _mongoDBService.CreateCharacterAsync(character);
            return CreatedAtAction(nameof(GetAllCharacters), new { id = character.Id }, character);
        }

        [HttpPut]
        public async Task UpdateCharacter([FromBody] Character character)
        {
            await _mongoDBService.UpdateCharacter(character);
            return;
        }

        [HttpDelete("{id}")]
        public async Task DeleteCharacter(string id)
        {
            await _mongoDBService.DeleteCharacterAsync(id);
            return;
        }
    }
}
