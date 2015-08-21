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
    public class MobileRatingSheetDomainManager : MappedEntityDomainManager<MobileRatingSheet, RatingSheetSection>
    {
        private RatingtoolContext context;

        public MobileRatingSheetDomainManager(RatingtoolContext dbContext, HttpRequestMessage request, ApiServices services) 
            : base(dbContext, request, services)
        {
            context = dbContext;
        }

        protected override TKey GetKey<TKey>(string id)
        {
            int ratingId = this.context.RatingSheetSection
                .Where(c => c.Id == id)
                .Select(c => c.RatingSheetSectionId)
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


        public override SingleResult<MobileRatingSheet> Lookup(string id)
        {
            int ratingSheetSectionId = GetKey<int>(id);
            return LookupEntity(p => p.RatingSheetSectionId == ratingSheetSectionId);            
        }

        public override Task<MobileRatingSheet> UpdateAsync(string id, Delta<MobileRatingSheet> patch)
        {
            throw new NotImplementedException();
        }


    }
}