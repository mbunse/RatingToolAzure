using Microsoft.WindowsAzure.Mobile.Service;
using ratingtoolService.Models;
using System.ComponentModel.DataAnnotations.Schema;

/**
This data object should be provided by the API instead of only the 
    Business Partner object and, separatly, all Ratings for a given 
    Business Partner. Instead, the API shall provide the Business Partner
    date together with the current rating.
**/
namespace ratingtoolService.DataObjects
{
    public class BpCurrentRating : EntityData
    {
        public string ShortName { get; set; }

        public int BusinessPartnerId { get; set; }

        public int RatingId { get; set; }

        //Id of BusinessPartner for a given rating
        public string RatingBpId { get; set; }

        public Rating.InternalRatingClass RatingClass { get; set; }

        public Rating.RatingMethodType RatingMethod { get; set; }
    }
}