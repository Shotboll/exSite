using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.ViewModels;
using RepairRequestsDataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairRequestsDatabaseImplement.Models
{
    public class DeviceType : IDeviceTypeModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; } = string.Empty;

        public virtual List<RepairRequest> RepairRequests { get; set; } = new();

        public static DeviceType Create(DeviceTypeBindingModel model)
        {
            return new DeviceType
            {
                Name = model.Name,
                Description = model.Description
            };
        }

        public void Update(DeviceTypeBindingModel model)
        {
            Name = model.Name;
            Description = model.Description;
        }

        public DeviceTypeViewModel GetViewModel => new()
        {
            Id = Id,
            Name = Name,
            Description = Description
        };
    }
}
