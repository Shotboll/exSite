using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.SearchModels
{
    public class RepairRequestServiceSearchModel
    {
        public int? Id { get; set; }
        public int? RepairRequestId { get; set; }
        public int? ServiceId { get; set; }
    }
}
