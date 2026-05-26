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
    public class ServiceStorage : IServiceStorage
    {
        private readonly RepairRequestsDatabase _context;

        public ServiceStorage(RepairRequestsDatabase context)
        {
            _context = context;
        }

        public List<ServiceViewModel> GetFullList()
        {
            return _context.Services
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<ServiceViewModel> GetFilteredList(ServiceSearchModel model)
        {
            IQueryable<Service> query = ApplyFilter(model)
                .OrderBy(x => x.Name);

            if (model.Page.HasValue && model.PageSize.HasValue)
            {
                query = query
                    .Skip((model.Page.Value - 1) * model.PageSize.Value)
                    .Take(model.PageSize.Value);
            }

            return query
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public ServiceViewModel? GetElement(ServiceSearchModel model)
        {
            if (model.Id.HasValue)
            {
                return _context.Services
                    .FirstOrDefault(x => x.Id == model.Id)
                    ?.GetViewModel;
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                return _context.Services
                    .FirstOrDefault(x => x.Name == model.Name)?
                    .GetViewModel;
            }

            return null;
        }

        public int GetCount(ServiceSearchModel? model)
        {
            if (model == null)
            {
                return _context.Services.Count();
            }

            return ApplyFilter(model).Count();
        }

        public ServiceViewModel? Insert(ServiceBindingModel model)
        {
            var newService = Service.Create(model);
            _context.Services.Add(newService);
            _context.SaveChanges();
            return newService.GetViewModel;
        }

        public ServiceViewModel? Update(ServiceBindingModel model)
        {
            var Service = _context.Services.FirstOrDefault(x => x.Id == model.Id);

            if (Service == null)
            {
                return null;
            }

            Service.Update(model);
            _context.SaveChanges();
            return Service.GetViewModel;
        }

        public ServiceViewModel? Delete(ServiceBindingModel model)
        {
            var Service = _context.Services.FirstOrDefault(x => x.Id == model.Id);

            if (Service == null)
            {
                return null;
            }

            _context.Services.Remove(Service);
            _context.SaveChanges();
            return Service.GetViewModel;
        }

        private IQueryable<Service> ApplyFilter(ServiceSearchModel model)
        {
            var query = _context.Services.AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(model.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                var searchText = model.SearchText.ToLower();

                query = query.Where(x =>
                    x.Name.ToLower().Contains(searchText) ||
                    (x.Description != null && x.Description.ToLower().Contains(searchText)));
            }

            if (model.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= model.MinPrice.Value);
            }

            if (model.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= model.MaxPrice.Value);
            }

            return query;
        }
    }
}
