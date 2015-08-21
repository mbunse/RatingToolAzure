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
    public class MobilePartialRating : EntityData
    {
        public string Name { get; set; }

        public float Ratio { get; set; }

        public PartialRating.RiskGroupType RiskGroup { get; set; }

        public float Weight { get; set; }

        public string Comment { get; set; }

    }
}