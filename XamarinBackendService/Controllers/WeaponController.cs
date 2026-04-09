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
    public class WeaponController : ControllerBase
    {
        private readonly XamarinBackendContext _context;

        public WeaponController(XamarinBackendContext context)
        {
            _context = context;
        }

        // GET tables/Weapon
        [HttpGet]
        [EnableQuery]
        public IQueryable<Weapon> GetAllWeapons()
        {
            return _context.Weapons
                .Include(w => w.Characters);
        }

        // GET tables/Weapon/{id}
        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IQueryable<Weapon>> GetWeapon(string id)
        {
            return Ok(_context.Weapons
                .Include(w => w.Characters)
                .Where(w => w.Id == id));
        }
    }
}
