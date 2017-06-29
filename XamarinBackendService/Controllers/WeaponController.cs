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
    public class WeaponController : TableController<Weapon>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinBackendContext context = new XamarinBackendContext();
            DomainManager = new EntityDomainManager<Weapon>(context, Request);
        }

        // GET tables/Weapon
        [QueryableExpand("Characters")]
        public IQueryable<Weapon> GetAllWeapons()
        {
            return Query();
        }

        // GET tables/Weapon/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [QueryableExpand("Characters")]
        public SingleResult<Weapon> GetWeapon(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Weapon/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task<Weapon> PatchWeapon(string id, Delta<Weapon> patch)
        //{
        //    return UpdateAsync(id, patch);
        //}

        // POST tables/Weapon
        //public async Task<IHttpActionResult> PostWeapon(Weapon item)
        //{
        //    Weapon current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // DELETE tables/Weapon/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task DeleteWeapon(string id)
        //{
        //    return DeleteAsync(id);
        //}
    }
}