﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using ratingtoolService.DataObjects;
using ratingtoolService.Models;

namespace ratingtoolService.Controllers
{
    public class RatingSheetController : TableController<RatingSheet>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            RatingtoolContext context = new RatingtoolContext();
            DomainManager = new RatingSheetDomainManager(context, Request, Services);
        }

        // Should be called via tables/RatingSheet?$filter=(ratingGuid+eq+'95e6ec59-9ce7-4096-8bcd-02315e29991f')
        // GET tables/RatingSheet
        public IQueryable<RatingSheet> GetAllRatingSheet()
        {
            return Query(); 
        }

        // GET tables/RatingSheet/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<RatingSheet> GetRatingSheet(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/RatingSheet/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<RatingSheet> PatchRatingSheet(string id, Delta<RatingSheet> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/RatingSheet
        public async Task<IHttpActionResult> PostRatingSheet(RatingSheet item)
        {
            RatingSheet current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/RatingSheet/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteRatingSheet(string id)
        {
             return DeleteAsync(id);
        }

    }
}