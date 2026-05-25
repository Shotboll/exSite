using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDataModels.Models
{
    public interface IDeviceTypeModel
    {
        int Id { get; }
        string Name { get; }
        string? Description { get; }
    }
}
