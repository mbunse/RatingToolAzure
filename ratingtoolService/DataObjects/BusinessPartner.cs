using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ratingtoolService.DataObjects
{
    public class BusinessPartner : EntityData
    {
        [StringLength(50)]
        public string ShortName { get; set;  }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}