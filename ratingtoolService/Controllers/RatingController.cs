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
    public class RatingController : TableController<Rating>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ratingtoolContext context = new ratingtoolContext();
            DomainManager = new EntityDomainManager<Rating>(context, Request, Services);
        }

        // GET tables/Rating
        public IQueryable<Rating> GetAllRating()
        {
            return Query(); 
        }

        // GET tables/Rating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Rating> GetRating(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Rating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Rating> PatchRating(string id, Delta<Rating> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Rating
        public async Task<IHttpActionResult> PostRating(Rating item)
        {
            Rating current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Rating/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteRating(string id)
        {
             return DeleteAsync(id);
        }

    }
}