using RepairRequestsDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.BindingModels
{
    public class RepairRequestServiceBindingModel : IRepairRequestServiceModel
    {
        public int Id { get; set; }
        public int RepairRequestId { get; set; }
        public int ServiceId { get; set; }
    }
}
