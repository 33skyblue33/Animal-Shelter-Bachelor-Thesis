using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Util;

namespace Domain.Entities
{
    public  class AdoptionRequest
    {
        public long Id { get; set; }
        public AdoptionRequestStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ResolvedDate {  get; set; }
    }
}
