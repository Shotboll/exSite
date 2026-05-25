using RepairRequestsDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsContracts.BindingModels
{
    public class DeviceTypeBindingModel : IDeviceTypeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название типа техники должно быть заполнено")]
        [StringLength(30, ErrorMessage = "Название должно быть не более 30 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }
    }
}
