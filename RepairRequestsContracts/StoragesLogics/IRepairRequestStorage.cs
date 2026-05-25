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
    public interface IRepairRequestStorage
    {
        List<RepairRequestViewModel> GetFullList();
        List<RepairRequestViewModel> GetFilteredList(RepairRequestSearchModel model);
        RepairRequestViewModel? GetElement(RepairRequestSearchModel model);

        int GetCount(RepairRequestSearchModel? model);

        RepairRequestViewModel? Insert(RepairRequestBindingModel model);
        RepairRequestViewModel? Update(RepairRequestBindingModel model);
        RepairRequestViewModel? Delete(RepairRequestBindingModel model);
    }
}
