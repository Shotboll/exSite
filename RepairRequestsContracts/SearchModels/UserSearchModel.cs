using RepairRequestsDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.SearchModels
{
    public class UserSearchModel
    {
        public int? Id { get; set; }
        public string? Login { get; set; }
        public string? PasswordHash { get; set; }
        public string? Nmae { get; set; }
        public UserRole? Role { get; set; }
    }
}
