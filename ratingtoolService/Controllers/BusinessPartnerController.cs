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
    public class BusinessPartnerController : TableController<BusinessPartner>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ratingtoolContext context = new ratingtoolContext();
            DomainManager = new EntityDomainManager<BusinessPartner>(context, Request, Services);
        }

        // GET tables/BusinessPartner
        public IQueryable<BusinessPartner> GetAllBusinessPartner()
        {
            return Query(); 
        }

        // GET tables/BusinessPartner/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<BusinessPartner> GetBusinessPartner(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/BusinessPartner/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<BusinessPartner> PatchBusinessPartner(string id, Delta<BusinessPartner> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/BusinessPartner
        public async Task<IHttpActionResult> PostBusinessPartner(BusinessPartner item)
        {
            BusinessPartner current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/BusinessPartner/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBusinessPartner(string id)
        {
             return DeleteAsync(id);
        }

    }
}