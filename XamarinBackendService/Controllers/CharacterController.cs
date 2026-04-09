using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using XamarinBackendService.DataObjects;
using XamarinBackendService.Models;

namespace XamarinBackendService.Controllers
{
    [ApiController]
    [Route("tables/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly XamarinBackendContext _context;

        public CharacterController(XamarinBackendContext context)
        {
            _context = context;
        }

        // GET tables/Character
        [HttpGet]
        [EnableQuery]
        public IQueryable<Character> GetAllCharacters()
        {
            return _context.Characters
                .Include(c => c.Weapons)
                .Include(c => c.Appearances);
        }

        // GET tables/Character/{id}
        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IQueryable<Character>> GetCharacter(string id)
        {
            return Ok(_context.Characters
                .Include(c => c.Weapons)
                .Include(c => c.Appearances)
                .Where(c => c.Id == id));
        }
    }
}
