using RepairRequestsDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.SearchModels
{
    public class RepairRequestSearchModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? DeviceTypeId { get; set; }
        public RequestStatus? Status { get; set; }
        public string? SearchText { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
