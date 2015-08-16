using Microsoft.WindowsAzure.Mobile.Service;
using System.ComponentModel.DataAnnotations;

namespace ratingtoolService.Models
{
    public class PartialRating 
    {
        public int Id { get; set; }
        [Required()]
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0.00")]
        public float Ratio { get; set; }

        public enum RatingSectionType
        {
            Quantitive = 1,
            Qualitative = 2,
            Environment = 3,
            Politics = 4,
            Economy = 5
        }

        [Required()]
        public RatingSectionType RatingSection { get; set; }

        public enum RiskGroupType
        {
            A = 1,
            B = 2,
            C = 3,
            D = 4
        }

        public RiskGroupType RiskGroup { get; set; }

        [Required()]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="0.00")]
        [Editable(false)]
        [Range(0,1)]
        public float Weight { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Required()]
        public int RatingID { get; set; }
    }
}