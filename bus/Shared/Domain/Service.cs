using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Domain
{
    public class Service : BaseDomainModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string Type { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
