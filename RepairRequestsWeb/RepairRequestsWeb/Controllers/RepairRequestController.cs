using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepairRequestsContracts.BindingModels;
using RepairRequestsContracts.BusinessLogicsContracts;
using RepairRequestsContracts.SearchModels;
using RepairRequestsDataModels.Enums;
using System.Security.Claims;

namespace RepairRequestsWeb.Controllers
{
    [Authorize]
    public class RepairRequestController : Controller
    {

        private readonly IRepairRequestLogic _repairRequestLogic;
        private readonly IUserLogic _userLogic;
        private readonly IDeviceTypeLogic _deviceTypeLogic;
        private readonly IServiceLogic _serviceLogic;

        public RepairRequestController(IRepairRequestLogic repairRequestLogic, IUserLogic userLogic, IDeviceTypeLogic deviceTypeLogic, IServiceLogic serviceLogic)
        {
            _repairRequestLogic = repairRequestLogic;
            _userLogic = userLogic;
            _deviceTypeLogic = deviceTypeLogic;
            _serviceLogic = serviceLogic;
        }

        [HttpGet]
        public IActionResult Index(string? searchText, RequestStatus? status, int? deviceTypeId, int page = 1)
        {
            const int pageSize = 5;

            if (page < 1) page = 1;

            var searchModel = new RepairRequestSearchModel
            {
                SearchText = searchText,
                Status = status.HasValue ? (RequestStatus)status.Value : null,
                PageSize = pageSize,
                DeviceTypeId = deviceTypeId,
                Page = page
            };

            if (!IsAdmin())
            {
                searchModel.UserId = GetCurrentUserId();
            }

            var repairReqiests = _repairRequestLogic.ReadList(searchModel);
            var totalCount = _repairRequestLogic.GetCount(searchModel);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            ViewBag.SearchText = searchText;
            ViewBag.Status = status;
            ViewBag.DeviceTypeId = deviceTypeId;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;
            ViewBag.IsAdmin = IsAdmin();

            FillFilterViewBags();

            return View(repairReqiests);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var repairRequest = _repairRequestLogic.ReadElement(new RepairRequestSearchModel
            {
                Id = id,
            });

            if(repairRequest == null) return NotFound();

            if (!CanAccessRequest(repairRequest.UserId))
            {
                return Forbid();
            }

            return View(repairRequest);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var currentUserId = GetCurrentUserId();

            FillFormViewBags(currentUserId);

            return View(new RepairRequestBindingModel
            {
                CreatedDate = DateTime.UtcNow,
                Status = RequestStatus.Новая,
                UserId = currentUserId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RepairRequestBindingModel model)
        {
            if (!IsAdmin())
            {
                model.UserId = GetCurrentUserId();
                model.Status = RequestStatus.Новая;
            }
            else if (model.UserId <= 0)
            {
                ModelState.AddModelError(nameof(model.UserId), "Выберите пользователя");
            }

            if (!ModelState.IsValid)
            {
                FillFormViewBags(model.UserId);
                return View(model);
            }

            try
            {
                _repairRequestLogic.Create(model);
                TempData["Message"] = "Заявка успешно добавлена";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                FillFormViewBags(model.UserId);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var repairRequest = _repairRequestLogic.ReadElement(new RepairRequestSearchModel
            {
                Id = id,
            });

            if(repairRequest == null) return NotFound();

            if (!CanAccessRequest(repairRequest.UserId))
            {
                return Forbid();
            }

            var model = new RepairRequestBindingModel
            {
                Id = repairRequest.Id,
                Title = repairRequest.Title,
                Description = repairRequest.Description,
                Status = repairRequest.Status,
                CreatedDate = repairRequest.CreatedDate,
                UserId = repairRequest.UserId,
                DeviceTypeId = repairRequest.DeviceTypeId,
                SelectedServiceIds = repairRequest.Services.Select(x => x.ServiceId).ToList(),
            };

            FillFormViewBags();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RepairRequestBindingModel model)
        {
            var oldRequest = _repairRequestLogic.ReadElement(new RepairRequestSearchModel
            {
                Id = model.Id
            });

            if (oldRequest == null)
            {
                return NotFound();
            }

            if (!CanAccessRequest(oldRequest.UserId))
            {
                return Forbid();
            }

            if (!IsAdmin())
            {
                model.UserId = GetCurrentUserId();
                model.Status = oldRequest.Status;
            }

            if (!ModelState.IsValid)
            {
                FillFormViewBags(model.UserId);
                return View(model);
            }

            try
            {
                _repairRequestLogic.Update(model);
                TempData["Message"] = "Заявка успешно изменена";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                FillFormViewBags(model.UserId);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var repairRequest = _repairRequestLogic.ReadElement(new RepairRequestSearchModel
            {
                Id = id
            });

            if (repairRequest == null) return NotFound();

            if (!CanAccessRequest(repairRequest.UserId))
            {
                return Forbid();
            }

            return View(repairRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var repairRequest = _repairRequestLogic.ReadElement(new RepairRequestSearchModel
            {
                Id = id
            });

            if (repairRequest == null)
            {
                return NotFound();
            }

            if (!CanAccessRequest(repairRequest.UserId))
            {
                return Forbid();
            }

            try
            {
                _repairRequestLogic.Delete(new RepairRequestBindingModel
                {
                    Id = id
                });

                TempData["Message"] = "Заявка успешно удалена";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Delete", repairRequest);
            }
        }

        private void FillFormViewBags(int? selectedUserId = null)
        {
            ViewBag.IsAdmin = IsAdmin();

            if (IsAdmin())
            {
                ViewBag.Users = new SelectList(_userLogic.ReadList(null), "Id", "Name", selectedUserId);
            }

            ViewBag.DeviceTypes = new SelectList(_deviceTypeLogic.ReadList(null), "Id", "Name");
            ViewBag.Services = _serviceLogic.ReadList(null);
            ViewBag.Statuses = new SelectList(Enum.GetValues<RequestStatus>());
        }

        private void FillFilterViewBags()
        {
            ViewBag.DeviceTypes = new SelectList(_deviceTypeLogic.ReadList(null), "Id", "Name");
            ViewBag.Statuses = new SelectList(Enum.GetValues<RequestStatus>());
        }

        private int GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return 0;
            }

            return int.Parse(userId);
        }

        private bool IsAdmin()
        {
            return User.IsInRole(UserRole.Администратор.ToString());
        }

        private bool CanAccessRequest(int requestUserId)
        {
            return IsAdmin() || requestUserId == GetCurrentUserId();
        }
    }
}
