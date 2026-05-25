using RepairRequestsDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDataModels.Models
{
    public interface IUserModel
    {
        int Id { get; }
        string Login { get; }
        string PasswordHash { get; }
        string Name { get; }

        UserRole Role { get; }
    }
}
