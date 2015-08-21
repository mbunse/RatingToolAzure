using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ratingtoolService.Models
{
    public class RatingSheetSection
    {

        [Key]
        public int RatingSheetSectionId { get; set; }

        public string Id { get; set; }

        [Required()]
        public string Name { get; set; }

        public PartialRating.RiskGroupType RiskGroup { get; set; }

        public virtual ICollection<PartialRating> PartialRatingsInSection { get; set; }

        [Required()]
        public int RatingID { get; set; }

        //Navigation property
        public virtual Rating Rating { get; set; }


    }
}