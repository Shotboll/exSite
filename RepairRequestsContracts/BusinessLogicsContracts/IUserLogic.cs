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
    public interface IUserLogic
    {
        List<UserViewModel> ReadList(UserSearchModel? model);
        UserViewModel ReadElement(UserSearchModel model);

        bool Create(UserBindingModel model);
        bool Update(UserBindingModel model);
        bool Delete(UserBindingModel model);
    }
}
