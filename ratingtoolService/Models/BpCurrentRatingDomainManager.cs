using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Data.Entity;
using System.Net.Http;
using ratingtoolService.DataObjects;
using AutoMapper;
using System.Linq;

namespace ratingtoolService.Models
{
    // Base class documentation: https://msdn.microsoft.com/en-us/library/azure/dn952554.aspx
    // Implements IDomainManager
    // MapssedEntityDomainManager provides basic functionality to provide all necessary methods
    // based on with AutoMapper mapped DTOs.
    // We only need to implement three methods and the constructor.
    public class BpCurrentRatingDomainManager : MappedEntityDomainManager<BpCurrentRating, Rating>
    {
        private RatingtoolContext context;

        public BpCurrentRatingDomainManager(RatingtoolContext dbContext, HttpRequestMessage request, ApiServices services) 
            : base(dbContext, request, services)
        {
            context = dbContext;
        }

        protected override TKey GetKey<TKey>(string id)
        {
            int ratingId = this.context.Ratings
                .Where(c => c.Id == id)
                .Select(c => c.RatingId)
                .FirstOrDefault();
                
            if (ratingId == 0)
            {
                throw new HttpResponseException(Request.CreateNotFoundResponse());
            }
            return (TKey) (object)ratingId;
        }

        public override Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }


        public override SingleResult<BpCurrentRating> Lookup(string id)
        {
            int ratingId = GetKey<int>(id);
            return LookupEntity(p => p.RatingId == ratingId);            
        }

        public override Task<BpCurrentRating> UpdateAsync(string id, Delta<BpCurrentRating> patch)
        {
            throw new NotImplementedException();
        }


    }
}