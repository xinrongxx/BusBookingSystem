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
        [DataType(DataType.Date)]
        public string BusSeater { get; set; }
        public string Type { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
    }
}