using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using XamarinBackendService.DataObjects;
using XamarinBackendService.Models;
using XamarinBackendService.Helpers;

namespace XamarinBackendService.Controllers
{
    public class MovieController : TableController<Movie>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinBackendContext context = new XamarinBackendContext();
            DomainManager = new EntityDomainManager<Movie>(context, Request);
        }

        // GET tables/Movie
        [QueryableExpand("Characters")]
        public IQueryable<Movie> GetAllMovies()
        {
            return Query();
        }

        // GET tables/Movie/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [QueryableExpand("Characters")]
        public SingleResult<Movie> GetMovie(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Movie/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task<Movie> PatchMovie(string id, Delta<Movie> patch)
        //{
        //    return UpdateAsync(id, patch);
        //}

        // POST tables/Movie
        //public async Task<IHttpActionResult> PostMovie(Movie item)
        //{
        //    Movie current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // DELETE tables/Movie/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task DeleteMovie(string id)
        //{
        //    return DeleteAsync(id);
        //}
    }
}