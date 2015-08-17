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
    public class RatingSheetDomainManager : MappedEntityDomainManager<RatingSheet, PartialRating>
    {
        private RatingtoolContext context;

        public RatingSheetDomainManager(RatingtoolContext dbContext, HttpRequestMessage request, ApiServices services) 
            : base(dbContext, request, services)
        {
            context = dbContext;
        }

        protected override TKey GetKey<TKey>(string id)
        {
            int ratingId = this.context.PartialRatings
                .Where(c => c.Id == id)
                .Select(c => c.PartialRatingId)
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


        public override SingleResult<RatingSheet> Lookup(string id)
        {
            int partialRatingId = GetKey<int>(id);
            return LookupEntity(p => p.PartialRatingId == partialRatingId);            
        }

        public override Task<RatingSheet> UpdateAsync(string id, Delta<RatingSheet> patch)
        {
            throw new NotImplementedException();
        }


    }
}