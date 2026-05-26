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
    public class ServiceLogic : IServiceLogic
    {
        private readonly IServiceStorage _serviceStorage;

        public ServiceLogic(IServiceStorage serviceStorage)
        {
            _serviceStorage = serviceStorage;
        }

        public List<ServiceViewModel> ReadList(ServiceSearchModel? model)
        {
            if (model == null)
            {
                return _serviceStorage.GetFullList();
            }

            return _serviceStorage.GetFilteredList(model);
        }

        public ServiceViewModel ReadElement(ServiceSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _serviceStorage.GetElement(model)!;
        }

        public int GetCount(ServiceSearchModel? model)
        {
            return _serviceStorage.GetCount(model);
        }

        public bool Create(ServiceBindingModel model)
        {
            CheckModel(model);

            var existingService = _serviceStorage.GetElement(new ServiceSearchModel
            {
                Name = model.Name,
            });

            if (existingService != null)
            {
                throw new InvalidOperationException("Услуга с таким названием уже существует");
            }

            var result = _serviceStorage.Insert(model);
            return result != null;
        }

        public bool Update(ServiceBindingModel model)
        {
            CheckModel(model);

            if (model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор");
            }

            var existingService = _serviceStorage.GetElement(new ServiceSearchModel
            {
                Name = model.Name,
            });

            if (existingService != null && existingService.Id != model.Id)
            {
                throw new InvalidOperationException("Услуга с таким названием уже существует");
            }

            var result = _serviceStorage.Update(model);
            return result != null;
        }

        public bool Delete(ServiceBindingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор");
            }

            var result = _serviceStorage.Delete(model);
            return result != null;
        }

        private static void CheckModel(ServiceBindingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new InvalidOperationException("Название услуги не указано");
            }

            if (model.Price < 0)
            {
                throw new InvalidOperationException("Стоимость услуги не может быть отрицательной");
            }
        }
    }
}
