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
    public class RepairRequestBindingModel : IRepairRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название заявки обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание заявки обязательно заполняется")]
        [StringLength(1000, ErrorMessage = "Текст заявки не должен превышать 1000 символов")]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public RequestStatus Status { get; set; } = RequestStatus.Новая;

        public int UserId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Необходимо выбрать тип техники")]
        public int DeviceTypeId { get; set; }

        public List<int> SelectedServiceIds { get; set; } = new();
    }
}
