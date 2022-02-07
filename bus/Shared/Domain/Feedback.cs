using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Domain
{
    public class Feedback : BaseDomainModel
    {
        public string Rating { get; set; }
        public string Comment { get; set; }
    }
}
