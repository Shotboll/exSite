using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.SearchModels;
using RepairRequestsContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.BusinessLogicsContracts
{
    public interface IDeviceTypeLogic
    {
        List<DeviceTypeViewModel> ReadList(DeviceTypeSearchModel? model);
        DeviceTypeViewModel ReadElement(DeviceTypeSearchModel model);

        bool Create(DeviceTypeBindingModel model);
        bool Update(DeviceTypeBindingModel model);
        bool Delete (DeviceTypeBindingModel model);
    }
}
