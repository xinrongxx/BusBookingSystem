using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bus.Shared.Domain
{
    public class Booking : BaseDomainModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
        [Required]
        public int Hours { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Location { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Dropoff { get; set; }
        public int? BusId { get; set; }
        public virtual Bus Bus { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
