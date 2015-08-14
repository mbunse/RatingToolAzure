using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;

namespace ratingtoolService.DataObjects
{
    public class Rating : EntityData
    {
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
            A0 = 0,
            A1 = 1,
            A2 = 2,
            A3 = 3,
            A4 = 4,
            A5 = 5,
            B1 = 6,
            B2 = 7,
            B3 = 8,
            B4 = 9,
            B5 = 10,
            E = 11
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
        public string BusinessPartnerID { get; set; }
    }
}