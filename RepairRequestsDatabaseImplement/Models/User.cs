using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.ViewModels;
using RepairRequestsDataModels.Enums;
using RepairRequestsDataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairRequestsDatabaseImplement.Models
{
    public class User : IUserModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public UserRole Role { get; set; }
        
        public virtual List<RepairRequest> RepairRequests { get; set; } = new();

        public static User Create(UserBindingModel model)
        {
            return new User
            {
                Login = model.Login,
                PasswordHash = model.PasswordHash,
                Name = model.Name,
                Role = model.Role,
            };
        }

        public void Update(UserBindingModel model)
        {
            Login = model.Login;
            PasswordHash = model.PasswordHash;
            Name = model.Name;
            Role = model.Role;
        }

        public UserViewModel GetViewModel => new()
        {
            Id = Id,
            Login = Login,
            PasswordHash = PasswordHash,
            Name = Name,
            Role = Role
        };
    }
}
