using Microsoft.EntityFrameworkCore.Update.Internal;
using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.ViewModels;
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
    public class Service : IServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Price { get; set; }

        public virtual List<RepairRequestService> RepairRequestServices { get; set; } = new();

        public static Service Create(ServiceBindingModel model)
        {
            return new Service
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
        }

        public void Update(ServiceBindingModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Price = model.Price;
        }

        public ServiceViewModel GetViewModel => new()
        {
            Id = Id,
            Description = Description,
            Name = Name,
            Price = Price
        };
    }
}
