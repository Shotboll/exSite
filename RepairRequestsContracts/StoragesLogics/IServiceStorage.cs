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
    public interface IServiceStorage
    {
        List<ServiceViewModel> GetFullList();
        List<ServiceViewModel> GetFilteredList(ServiceSearchModel model);
        ServiceViewModel? GetElement(ServiceSearchModel model);

        int GetCount(ServiceSearchModel? model);

        ServiceViewModel? Insert(ServiceBindingModel model);
        ServiceViewModel? Update(ServiceBindingModel model);
        ServiceViewModel? Delete(ServiceBindingModel model);
    }
}
