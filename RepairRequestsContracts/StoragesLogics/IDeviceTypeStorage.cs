using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.SearchModels;
using RepairRequestsContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.StoragesLogics
{
    public interface IDeviceTypeStorage
    {
        List<DeviceTypeViewModel> GetFullList();
        List<DeviceTypeViewModel> GetFilteredList(DeviceTypeSearchModel model);
        DeviceTypeViewModel? GetElement(DeviceTypeSearchModel model);
        DeviceTypeViewModel? Insert(DeviceTypeBindingModel model);
        DeviceTypeViewModel? Update(DeviceTypeBindingModel model);
        DeviceTypeViewModel? Delete(DeviceTypeBindingModel model);
    }
}
