using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;

namespace ratingtoolService.Models
{
    public class Rating
    {
        // Needed to map to DTO which derives from EntityData that provides
        // Id field with GUID.
        public string Id { get; set; }

        [Key]
        public int RatingId { get; set; }

        public enum Status {
            InProgress = 0,
            Approved = 1,
            Withdrawn = 2,
        }

        public Status RatingStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValidUntil { get; set; }

        public enum InternalRatingClass
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4
        }

        public InternalRatingClass RatingClass { get; set; }

        public enum RatingMethodType
        {
            COR = 1,
            SOV = 2
        }

        public RatingMethodType RatingMethod { get; set; }

        public virtual ICollection<PartialRating> PartialRatings { get; set; }

        [Required]
        public int BusinessPartnerID { get; set; }

        public virtual BusinessPartner BusinessPartner { get; set;}
    }
}