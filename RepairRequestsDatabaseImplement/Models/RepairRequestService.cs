using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.ViewModels;
using RepairRequestsDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDatabaseImplement.Models
{
    public class RepairRequestService : IRepairRequestServiceModel
    {
        public int Id { get; set; }
        public int RepairRequestId { get; set; }
        public int ServiceId { get; set; }

        public virtual RepairRequest RepairRequest { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;

        public static RepairRequestService Create(RepairRequestServiceBindingModel model)
        {
            return new RepairRequestService
            {
                RepairRequestId = model.RepairRequestId,
                ServiceId = model.ServiceId,
            };
        }

        public RepairRequestServiceViewModel GetViewModel => new()
        {
            Id = Id,
            RepairRequestId = RepairRequestId,
            ServiceId = ServiceId,
            ServiceName = Service?.Name ?? string.Empty,
            ServicePrice = Service?.Price ?? 0,
        };
    }
}
