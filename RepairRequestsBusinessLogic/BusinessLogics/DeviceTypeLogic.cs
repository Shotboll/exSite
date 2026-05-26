using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.BusinessLogicsContracts;
using RepairRequestsContracts.SearchModels;
using RepairRequestsContracts.StoragesLogics;
using RepairRequestsContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsBusinessLogic.BusinessLogics
{
    public class DeviceTypeLogic : IDeviceTypeLogic
    {
        private readonly IDeviceTypeStorage _deviceTypeStorage;

        public DeviceTypeLogic(IDeviceTypeStorage deviceTypeStorage)
        {
            _deviceTypeStorage = deviceTypeStorage;
        }

        public List<DeviceTypeViewModel> ReadList(DeviceTypeSearchModel? model)
        {
            if (model == null)
            {
                return _deviceTypeStorage.GetFullList();
            }

            return _deviceTypeStorage.GetFilteredList(model);
        }

        public DeviceTypeViewModel ReadElement(DeviceTypeSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _deviceTypeStorage.GetElement(model)!;
        }

        public bool Create(DeviceTypeBindingModel model)
        {
            CheckModel(model);

            var existingDeviceType = _deviceTypeStorage.GetElement(new DeviceTypeSearchModel
            {
                Name = model.Name,
            });

            if (existingDeviceType != null)
            {
                throw new InvalidOperationException("Тип техники с таким названием уже существует");
            }

            var result = _deviceTypeStorage.Insert(model);
            return result != null;
        }

        public bool Update(DeviceTypeBindingModel model)
        {
            CheckModel(model);

            if (model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор");
            }

            var existingDeviceType = _deviceTypeStorage.GetElement(new DeviceTypeSearchModel
            {
                Name = model.Name,
            });

            if (existingDeviceType != null && existingDeviceType.Id != model.Id)
            {
                throw new InvalidOperationException("Тип техники с таким названием уже существует");
            }

            var result = _deviceTypeStorage.Update(model);
            return result != null;
        }

        public bool Delete(DeviceTypeBindingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор");
            }

            var result = _deviceTypeStorage.Delete(model);
            return result != null;
        }

        private static void CheckModel(DeviceTypeBindingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new InvalidOperationException("Название типа техники не указано");
            }
        }
    }
}
