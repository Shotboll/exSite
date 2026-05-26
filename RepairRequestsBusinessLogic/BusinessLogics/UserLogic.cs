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
    public class UserLogic : IUserLogic
    {
        private readonly IUserStorage _userStorage;

        public UserLogic(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public List<UserViewModel> ReadList(UserSearchModel? model)
        {
            if(model == null)
            {
                return _userStorage.GetFullList();
            }

            return _userStorage.GetFilteredList(model);
        }

        public UserViewModel ReadElement(UserSearchModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _userStorage.GetElement(model)!;
        }

        public bool Create(UserBindingModel model)
        {
            CheckModel(model);

            var existingUser = _userStorage.GetElement(new UserSearchModel
            {
                Login = model.Login,
            });

            if(existingUser != null)
            {
                throw new InvalidOperationException("Пользователь с таким логином уже существует");
            }

            var result = _userStorage.Insert(model);
            return result != null;
        }

        public bool Update(UserBindingModel model)
        {
            CheckModel(model);

            if(model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор пользователя");
            }

            var existingUser = _userStorage.GetElement(new UserSearchModel
            {
                Login = model.Login,
            });

            if(existingUser != null && existingUser.Id != model.Id)
            {
                throw new InvalidOperationException("Пользователь с таким логином уже существует");
            }

            var result = _userStorage.Update(model);
            return result != null;
        }

        public bool Delete(UserBindingModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if(model.Id <= 0)
            {
                throw new InvalidOperationException("Некорректный идентификатор");
            }

            var result = _userStorage.Delete(model);
            return result != null;
        }

        private static void CheckModel(UserBindingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Login))
            {
                throw new InvalidOperationException("Логин пользователя не указан");
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new InvalidOperationException("Имя пользователя не указано");
            }

            if (string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                throw new InvalidOperationException("Пароль пользователя не указан");
            }
        }
    }
}
