using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Domain
{
    public class Bus : BaseDomainModel
    {
        [Required]
        public int BusNo { get; set; }
        public int? SeatId { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}