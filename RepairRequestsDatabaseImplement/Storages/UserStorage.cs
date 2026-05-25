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
    public class UserStorage : IUserStorage
    {
        private readonly RepairRequestsDatabase _context;

        public UserStorage(RepairRequestsDatabase context)
        {
            _context = context;
        }

        public List<UserViewModel> GetFullList()
        {
            return _context.Users
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<UserViewModel> GetFilteredList(UserSearchModel model)
        {
            var query = _context.Users.AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }
            if (!string.IsNullOrWhiteSpace(model.Login))
            {
                query = query.Where(x => x.Login ==  model.Login);
            }
            if (!string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                query = query.Where(x => x.PasswordHash == model.PasswordHash);
            }
            if (model.Role.HasValue)
            {
                query = query.Where(x => x.Role == model.Role.Value);
            }

            return query
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public UserViewModel? GetElement(UserSearchModel model)
        {
            if (model.Id.HasValue)
            {
                return _context.Users
                    .FirstOrDefault(x => x.Id == model.Id)
                    ?.GetViewModel;
            }

            if(!string.IsNullOrWhiteSpace(model.Login) && !string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                return _context.Users
                    .FirstOrDefault(x => x.Login == model.Login && x.PasswordHash == model.PasswordHash)?
                    .GetViewModel;
            }

            if (!string.IsNullOrWhiteSpace(model.Login))
            {
                return _context.Users
                    .FirstOrDefault(x => x.Login == model.Login)?
                    .GetViewModel;
            }

            return null;
        }

        public UserViewModel? Insert(UserBindingModel model)
        {
            var newUser = User.Create(model);
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser.GetViewModel;
        }

        public UserViewModel? Update(UserBindingModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == model.Id);

            if(user == null)
            {
                return null;
            }

            user.Update(model);
            _context.SaveChanges();
            return user.GetViewModel;
        }

        public UserViewModel? Delete(UserBindingModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == model.Id);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return user.GetViewModel;
        }
    }
}
