using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDataModels.Models
{
    public interface IRepairRequestServiceModel
    {
        int Id { get; }
        int RepairRequestId { get; }
        int ServiceId { get; }
    }
}
