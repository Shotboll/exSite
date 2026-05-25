using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDataModels.Models
{
    public interface IServiceModel
    {
        int Id { get; }
        string Name { get; }
        string? Description { get; }
        decimal Price { get; }
    }
}
