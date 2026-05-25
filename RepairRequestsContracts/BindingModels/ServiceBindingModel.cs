using RepairRequestsDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.BindingModels
{
    public class ServiceBindingModel : IServiceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название услуги обязательно")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Название услуги должно быть от 5 до 50 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание услуги не должно превышать 500 символов")]
        public string? Description { get; set; }

        [Range(0, 1000000, ErrorMessage = "Стоимость услуги должна быть не меньше 0")]
        public decimal Price { get; set; }
    }
}
