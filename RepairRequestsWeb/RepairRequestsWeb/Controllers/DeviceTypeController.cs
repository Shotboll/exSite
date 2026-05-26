using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.BusinessLogicsContracts;
using RepairRequestsContracts.SearchModels;

namespace RepairRequestsWeb.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class DeviceTypeController : Controller
    {
        private readonly IDeviceTypeLogic _deviceTypeLogic;
        
        public DeviceTypeController(IDeviceTypeLogic deviceTypeLogic)
        {
            _deviceTypeLogic = deviceTypeLogic;
        }

        [HttpGet]
        public IActionResult Index(string? name)
        {
            var deviceTypes = _deviceTypeLogic.ReadList(new DeviceTypeSearchModel
            {
                Name = name
            });
            ViewBag.Name = name;

            return View(deviceTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DeviceTypeBindingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeviceTypeBindingModel model)
        {
            if(!ModelState.IsValid) return View(model);

            try
            {
                _deviceTypeLogic.Create(model);
                TempData["Message"] = "Тип техники успешно добавлен";
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
            var deviceType = _deviceTypeLogic.ReadElement(new DeviceTypeSearchModel { Id = id });

            if (deviceType == null) return NotFound();

            var model = new DeviceTypeBindingModel
            {
                Id = deviceType.Id,
                Name = deviceType.Name,
                Description = deviceType.Description,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DeviceTypeBindingModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _deviceTypeLogic.Update(model);
                TempData["Message"] = "Тип техники успешно изменен";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deviceType = _deviceTypeLogic.ReadElement(new DeviceTypeSearchModel
            {
                Id = id
            });

            if(deviceType == null) return NotFound();

            var model = new DeviceTypeBindingModel
            {
                Id = deviceType.Id,
                Name = deviceType.Name,
                Description = deviceType.Description,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeviceTypeBindingModel model)
        {
            try
            {
                _deviceTypeLogic.Delete(model);
                TempData["Message"] = "Тип техники успешно удален";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Невозможно удалить тип техники, потому что он используется в заявках");
                return View(model);
            }
        }
    }
}
