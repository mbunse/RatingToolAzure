using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using ratingtoolService.DataObjects;
using ratingtoolService.Models;

namespace ratingtoolService.Controllers
{
    public class PartialRatingController : TableController<PartialRating>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ratingtoolContext context = new ratingtoolContext();
            DomainManager = new EntityDomainManager<PartialRating>(context, Request, Services);
        }

        // GET tables/PartialRating
        public IQueryable<PartialRating> GetAllPartialRating()
        {
            return Query(); 
        }

        // GET tables/PartialRating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PartialRating> GetPartialRating(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PartialRating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PartialRating> PatchPartialRating(string id, Delta<PartialRating> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PartialRating
        public async Task<IHttpActionResult> PostPartialRating(PartialRating item)
        {
            PartialRating current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PartialRating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePartialRating(string id)
        {
             return DeleteAsync(id);
        }

    }
}