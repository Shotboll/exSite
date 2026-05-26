using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.BusinessLogicsContracts;
using RepairRequestsContracts.SearchModels;
using RepairRequestsContracts.StoragesLogics;
using RepairRequestsContracts.ViewModels;
using RepairRequestsDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsBusinessLogic.BusinessLogics
{
    public class RepairRequestLogic : IRepairRequestLogic
    {
        private readonly IRepairRequestStorage _repairRequestStorage;
        private readonly IUserStorage _userStorage;
        private readonly IDeviceTypeStorage _deviceTypeStorage;
        private readonly IServiceStorage _serviceStorage;

        public RepairRequestLogic(IRepairRequestStorage repairRequestStorage, IUserStorage userStorage, IDeviceTypeStorage deviceTypeStorage, IServiceStorage serviceStorage)
        {
            _repairRequestStorage = repairRequestStorage;
            _userStorage = userStorage;
            _deviceTypeStorage = deviceTypeStorage;
            _serviceStorage = serviceStorage;
        }

        public List<RepairRequestViewModel> ReadList(RepairRequestSearchModel? model)
        {
            if (model == null)
            {
                return _repairRequestStorage.GetFullList();
            }

            return _repairRequestStorage.GetFilteredList(model);
        }

        public RepairRequestViewModel ReadElement(RepairRequestSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _repairRequestStorage.GetElement(model)!;
        }

        public int GetCount(RepairRequestSearchModel? model)
        {
            return _repairRequestStorage.GetCount(model);
        }

        public bool Create(RepairRequestBindingModel model)
        {
            CheckModel(model, true);

            model.Status = RequestStatus.Новая;
            model.CreatedDate = DateTime.UtcNow;

            var result = _repairRequestStorage.Insert(model);
            return result != null;
        }

        public bool Update(RepairRequestBindingModel model)
        {
            CheckModel(model, false);

            if (model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор");
            }

            var existingRepairRequest = _repairRequestStorage.GetElement(new RepairRequestSearchModel
            {
                Id = model.Id,
            });

            if (existingRepairRequest == null)
            {
                throw new InvalidOperationException("Заявка не найдена");
            }

            model.CreatedDate = existingRepairRequest.CreatedDate;

            var result = _repairRequestStorage.Update(model);
            return result != null;
        }

        public bool Delete(RepairRequestBindingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор");
            }

            var existingRequest = _repairRequestStorage.GetElement(new RepairRequestSearchModel
            {
                Id = model.Id
            });

            if (existingRequest == null)
            {
                throw new InvalidOperationException("Заявка не найдена");
            }

            var result = _repairRequestStorage.Delete(model);
            return result != null;
        }

        private void CheckModel(RepairRequestBindingModel model, bool isCreate)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Title))
            {
                throw new InvalidOperationException("Название заявки не указано");
            }

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                throw new InvalidOperationException("Описание заявки не указано");
            }

            if (model.UserId <= 0)
            {
                throw new InvalidOperationException("Пользователь заявки не указан");
            }

            if (model.DeviceTypeId <= 0)
            {
                throw new InvalidOperationException("Тип техники не выбран");
            }

            var user = _userStorage.GetElement(new UserSearchModel
            {
                Id = model.UserId
            });

            if (user == null)
            {
                throw new InvalidOperationException("Пользователь не найден");
            }

            var deviceType = _deviceTypeStorage.GetElement(new DeviceTypeSearchModel
            {
                Id = model.DeviceTypeId
            });

            if (deviceType == null)
            {
                throw new InvalidOperationException("Тип техники не найден");
            }

            if (model.SelectedServiceIds == null || model.SelectedServiceIds.Count == 0)
            {
                throw new InvalidOperationException("Необходимо выбрать хотя бы одну услугу");
            }

            foreach (var serviceId in model.SelectedServiceIds.Distinct())
            {
                var service = _serviceStorage.GetElement(new ServiceSearchModel
                {
                    Id = serviceId
                });

                if (service == null)
                {
                    throw new InvalidOperationException("Одна из выбранных услуг не найдена");
                }
            }

            if (!isCreate && model.Status < RequestStatus.Новая)
            {
                throw new InvalidOperationException("Некорректный статус заявки");
            }
        }
    }
}
