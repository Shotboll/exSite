using RepairRequestsDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDataModels.Models
{
    public interface IRepairRequestModel
    {
        int Id { get; }
        string Title { get; }
        string Description { get; }
        DateTime CreatedDate { get; }
        RequestStatus Status { get; }
        int UserId { get; }
        int DeviceTypeId { get; }
    }
}
