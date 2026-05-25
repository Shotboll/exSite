using RepairRequestsDataModels.Enums;
using RepairRequestsDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.ViewModels
{
    public class RepairRequestViewModel : IRepairRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public RequestStatus Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int DeviceTypeId { get; set; }
        public string DeviceTypeName { get; set; } = string.Empty;

        public List<RepairRequestServiceViewModel> Services { get; set; } = new();
        public decimal TotalPrice => Services.Sum(x => x.ServicePrice);
    }
}
