using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.ViewModels;
using RepairRequestsDataModels.Enums;
using RepairRequestsDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDatabaseImplement.Models
{
    public class RepairRequest : IRepairRequestModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public RequestStatus Status { get; set; }

        public int UserId { get; set; }
        public int DeviceTypeId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual DeviceType DeviceType { get; set; } = null!;
        public virtual List<RepairRequestService> RepairRequestServices { get; set; } = new();

        public static RepairRequest Create(RepairRequestBindingModel model)
        {
            return new RepairRequest
            {
                Status = model.Status,
                Title = model.Title,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                UserId = model.UserId,
                DeviceTypeId = model.DeviceTypeId,
            };
        }

        public void Update(RepairRequestBindingModel model)
        {
            Title = model.Title;
            Description = model.Description;
            Status = model.Status;
            DeviceTypeId = model.DeviceTypeId;
        }

        public RepairRequestViewModel GetViewModel => new()
        {
            Id = Id,
            Description = Description,
            DeviceTypeId = DeviceTypeId,
            CreatedDate = CreatedDate,
            Status = Status,
            UserId = UserId,
            DeviceTypeName = DeviceType?.Name ?? string.Empty,
            Title = Title,
            UserName = User?.Name ?? string.Empty,
            Services = RepairRequestServices.Select(x => x.GetViewModel).ToList()
        };
    }
}
