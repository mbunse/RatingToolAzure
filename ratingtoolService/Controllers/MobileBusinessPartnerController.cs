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
    public class MobileBusinessPartnerController : TableController<MobileBusinessPartner>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            RatingtoolContext context = new RatingtoolContext();
            DomainManager = new MobileBusinessPartnerDomainManager(context, Request, Services);
        }

        // GET tables/BpCurrentRating
        public IQueryable<MobileBusinessPartner> GetAllBusinessPartner()
        {
            return Query(); 
        }

        // GET tables/BpCurrentRating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MobileBusinessPartner> GetBusinessPartner(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/BpCurrentRating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MobileBusinessPartner> PatchBusinessPartner(string id, Delta<MobileBusinessPartner> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/BpCurrentRating
        public async Task<IHttpActionResult> PostBusinessPartner(MobileBusinessPartner item)
        {
            MobileBusinessPartner current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/BpCurrentRating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBusinessPartner(string id)
        {
             return DeleteAsync(id);
        }

    }
}