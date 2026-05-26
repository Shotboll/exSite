using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.BusinessLogicsContracts;
using RepairRequestsContracts.SearchModels;

namespace RepairRequestsWeb.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class ServiceController : Controller
    {

        private readonly IServiceLogic _serviceLogic;

        public ServiceController(IServiceLogic serviceLogic)
        {
            _serviceLogic = serviceLogic;
        }

        [HttpGet]
        public IActionResult Index(string? searchText, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            const int pageSize = 5;

            if(page < 1) page = 1;

            var searchModel = new ServiceSearchModel
            {
                SearchText = searchText,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Page = page,
                PageSize = pageSize
            };

            var services = _serviceLogic.ReadList(searchModel);
            var totalCount = _serviceLogic.GetCount(searchModel);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            ViewBag.SearchText = searchText;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;

            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ServiceBindingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _serviceLogic.Create(model);
                TempData["Message"] = "Услуга успешно добавлена";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var service = _serviceLogic.ReadElement(new ServiceSearchModel
            {
                Id = id
            });

            if (service == null) return NotFound();

            var model = new ServiceBindingModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceBindingModel model)
        {
            if (!ModelState.IsValid)  return View(model);

            try
            {
                _serviceLogic.Update(model);
                TempData["Message"] = "Услуга успешно изменена";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var service = _serviceLogic.ReadElement(new ServiceSearchModel
            {
                Id = id,
            });

            if (service == null) return NotFound();

            var model = new ServiceBindingModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ServiceBindingModel model)
        {
            try
            {
                _serviceLogic.Delete(model);
                TempData["Message"] = "Услуга успешно удалена";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Невозможно удалить услугу, потому что она используется в заявках");
                return View(model);
            }
        }
    }
}
