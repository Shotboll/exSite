using Microsoft.EntityFrameworkCore;
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
    public class RepairRequestStorage : IRepairRequestStorage
    {
        private readonly RepairRequestsDatabase _context;

        public RepairRequestStorage(RepairRequestsDatabase context)
        {
            _context = context;
        }

        public List<RepairRequestViewModel> GetFullList()
        {
            return _context.RepairRequests
                .Include(x => x.User)
                .Include(x => x.DeviceType)
                .Include(x => x.RepairRequestServices)
                .ThenInclude(x => x.Service)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<RepairRequestViewModel> GetFilteredList(RepairRequestSearchModel model)
        {
            IQueryable<RepairRequest> query = ApplyFilter(model)
                .OrderByDescending(x => x.CreatedDate);

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

        public RepairRequestViewModel? GetElement(RepairRequestSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }

            return _context.RepairRequests
                .Include(x => x.User)
                .Include(x => x.DeviceType)
                .Include(x => x.RepairRequestServices)
                .ThenInclude(x => x.Service)
                .FirstOrDefault(x => x.Id == model.Id.Value)
                ?.GetViewModel;
        }

        public int GetCount(RepairRequestSearchModel? model)
        {
            if (model == null)
            {
                return _context.RepairRequests.Count();
            }

            return ApplyFilter(model).Count();
        }

        public RepairRequestViewModel? Insert(RepairRequestBindingModel model)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var repairRequest = RepairRequest.Create(model);

                _context.RepairRequests.Add(repairRequest);
                _context.SaveChanges();

                SaveServices(repairRequest.Id, model.SelectedServiceIds);

                transaction.Commit();

                return GetElement(new RepairRequestSearchModel
                {
                    Id = repairRequest.Id
                });
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public RepairRequestViewModel? Update(RepairRequestBindingModel model)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var repairRequest = _context.RepairRequests
                    .Include(x => x.RepairRequestServices)
                    .FirstOrDefault(x => x.Id == model.Id);

                if (repairRequest == null)
                {
                    return null;
                }

                repairRequest.Update(model);

                var oldServices = _context.RepairRequestServices
                    .Where(x => x.RepairRequestId == model.Id)
                    .ToList();

                _context.RepairRequestServices.RemoveRange(oldServices);
                _context.SaveChanges();

                SaveServices(repairRequest.Id, model.SelectedServiceIds);

                transaction.Commit();

                return GetElement(new RepairRequestSearchModel
                {
                    Id = repairRequest.Id
                });
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public RepairRequestViewModel? Delete(RepairRequestBindingModel model)
        {
            var repairRequest = _context.RepairRequests
                .Include(x => x.RepairRequestServices)
                .FirstOrDefault(x => x.Id == model.Id);

            if (repairRequest == null)
            {
                return null;
            }

            _context.RepairRequests.Remove(repairRequest);
            _context.SaveChanges();

            return repairRequest.GetViewModel;
        }

        private IQueryable<RepairRequest> ApplyFilter(RepairRequestSearchModel model)
        {
            var query = _context.RepairRequests
                .Include(x => x.User)
                .Include(x => x.DeviceType)
                .Include(x => x.RepairRequestServices)
                .ThenInclude(x => x.Service)
                .AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (model.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == model.UserId.Value);
            }

            if (model.DeviceTypeId.HasValue)
            {
                query = query.Where(x => x.DeviceTypeId == model.DeviceTypeId.Value);
            }

            if (model.Status.HasValue)
            {
                query = query.Where(x => x.Status == model.Status.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                var searchText = model.SearchText.ToLower();

                query = query.Where(x =>
                    x.Title.ToLower().Contains(searchText) ||
                    x.Description.ToLower().Contains(searchText));
            }

            return query;
        }

        private void SaveServices(int repairRequestId, List<int> serviceIds)
        {
            foreach (var serviceId in serviceIds.Distinct())
            {
                _context.RepairRequestServices.Add(new RepairRequestService
                {
                    RepairRequestId = repairRequestId,
                    ServiceId = serviceId
                });
            }

            _context.SaveChanges();
        }
    }
}
