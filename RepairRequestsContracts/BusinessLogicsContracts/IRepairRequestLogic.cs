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
    public interface IRepairRequestLogic
    {
        List<RepairRequestViewModel> ReadList(RepairRequestSearchModel? model);
        RepairRequestViewModel ReadElement(RepairRequestSearchModel model);

        int GetCount(RepairRequestSearchModel? model);

        bool Create(RepairRequestBindingModel model);
        bool Update(RepairRequestBindingModel model);
        bool Delete(RepairRequestBindingModel model);
    }
}
