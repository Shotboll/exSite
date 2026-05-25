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
    public interface IServiceLogic
    {
        List<ServiceViewModel> ReadList(ServiceSearchModel? model);
        ServiceViewModel ReadElement(ServiceSearchModel model);

        int GetCount(ServiceSearchModel? model);

        bool Create(ServiceBindingModel model);
        bool Update(ServiceBindingModel model);
        bool Delete(ServiceBindingModel model);
    }
}
