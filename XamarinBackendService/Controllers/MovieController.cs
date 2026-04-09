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
    public class MovieController : ControllerBase
    {
        private readonly XamarinBackendContext _context;

        public MovieController(XamarinBackendContext context)
        {
            _context = context;
        }

        // GET tables/Movie
        [HttpGet]
        [EnableQuery]
        public IQueryable<Movie> GetAllMovies()
        {
            return _context.Movies
                .Include(m => m.Characters);
        }

        // GET tables/Movie/{id}
        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IQueryable<Movie>> GetMovie(string id)
        {
            return Ok(_context.Movies
                .Include(m => m.Characters)
                .Where(m => m.Id == id));
        }
    }
}
