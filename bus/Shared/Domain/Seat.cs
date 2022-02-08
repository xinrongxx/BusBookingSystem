using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Domain
{
    public class Seat : BaseDomainModel
    {
        [Required]
        [StringLength(2, ErrorMessage = "Name is too long.")]
        public string Seats { get; set; }

    }
}
