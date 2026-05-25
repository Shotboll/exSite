using RepairRequestsDataModels.Enums;
using RepairRequestsDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.BindingModels
{
    public class UserBindingModel : IUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        [StringLength(50, ErrorMessage = "Логин не должен превышать 50 симолов")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Пароль должен содержать от 5 до 50 символов")]
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя пользователя должно быть заполнено")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Имя пользователя должно быть от 5 до 30 символов")]
        public string Name { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Пользователь;
    }
}
