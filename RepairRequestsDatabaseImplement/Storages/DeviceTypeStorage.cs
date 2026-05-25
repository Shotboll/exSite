using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.SearchModels;
using RepairRequestsContracts.StoragesLogics;
using RepairRequestsContracts.ViewModels;
using RepairRequestsDatabaseImplement.Database;
using RepairRequestsDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsDatabaseImplement.Storages
{
    public class DeviceTypeStorage : IDeviceTypeStorage
    {
        private readonly RepairRequestsDatabase _context;

        public DeviceTypeStorage(RepairRequestsDatabase context)
        {
            _context = context;
        }

        public List<DeviceTypeViewModel> GetFullList()
        {
            return _context.DeviceTypes
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<DeviceTypeViewModel> GetFilteredList(DeviceTypeSearchModel model)
        {
            var query = _context.DeviceTypes.AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where(x => x.Name == model.Name);
            }

            return query
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public DeviceTypeViewModel? GetElement(DeviceTypeSearchModel model)
        {
            if (model.Id.HasValue)
            {
                return _context.DeviceTypes
                    .FirstOrDefault(x => x.Id == model.Id)
                    ?.GetViewModel;
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                return _context.DeviceTypes
                    .FirstOrDefault(x => x.Name == model.Name)?
                    .GetViewModel;
            }

            return null;
        }

        public DeviceTypeViewModel? Insert(DeviceTypeBindingModel model)
        {
            var newDeviceType = DeviceType.Create(model);
            _context.DeviceTypes.Add(newDeviceType);
            _context.SaveChanges();
            return newDeviceType.GetViewModel;
        }

        public DeviceTypeViewModel? Update(DeviceTypeBindingModel model)
        {
            var DeviceType = _context.DeviceTypes.FirstOrDefault(x => x.Id == model.Id);

            if (DeviceType == null)
            {
                return null;
            }

            DeviceType.Update(model);
            _context.SaveChanges();
            return DeviceType.GetViewModel;
        }

        public DeviceTypeViewModel? Delete(DeviceTypeBindingModel model)
        {
            var DeviceType = _context.DeviceTypes.FirstOrDefault(x => x.Id == model.Id);

            if (DeviceType == null)
            {
                return null;
            }

            _context.DeviceTypes.Remove(DeviceType);
            _context.SaveChanges();
            return DeviceType.GetViewModel;
        }
    }
}
