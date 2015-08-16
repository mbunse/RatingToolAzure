using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ratingtoolService.Models
{
    public class BusinessPartner
    {
        // Needed to map to DTO which derives from EntityData that provides
        // Id field with GUID.
        public string Id { get; set; }

        [Key]
        public int BusinessPartnerId { get; set; }

        [StringLength(50)]
        public string ShortName { get; set;  }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}