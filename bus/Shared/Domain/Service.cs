using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Domain
{
    public class Service : BaseDomainModel
    {
        public string Type { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
