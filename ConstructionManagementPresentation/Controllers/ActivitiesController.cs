using ConstructionManagementPresentation.Models;
using ConstructionManagementPresentation.Services;
using ConstructionManagementPresentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConstructionManagementPresentation.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ActivityService _activityService;
        private readonly WorkerService _workerService;


        public ActivitiesController(ActivityService activityService,WorkerService workerService)
        {
            _activityService = activityService;
        }

        public async Task<IActionResult> Index()
        {
            var activities = await _activityService.GetAllActivitiesAsync();
     
            return View(activities);
        }

        public async Task<IActionResult> Details(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        public async Task<IActionResult> Create()
        {

            var workers = await _workerService.GetAllWorkersAsync(); 
            var viewModel = new ActivityViewModel
            {
                Activity = new Activity(),
                Workers = workers.Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.Name
                }).ToList()
            };
            return View(viewModel);
        } 

        [HttpPost]
        public async Task<IActionResult> Create(ActivityViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _activityService.CreateActivityAsync(vm.Activity);
                return RedirectToAction(nameof(Index));
            }

      
            vm.Workers = (await _workerService.GetAllWorkersAsync()).Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.Name
            }).ToList();

            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Activity activity)
        {
            Activity act = new Activity()
            {
                Id = id,
                ActivityType = activity.ActivityType,
                Date = activity.Date,
                Description = activity.Description,
                WorkerId = activity.Id

            };
            if (id != act.Id )
            {
                return BadRequest();
            }

            var updated = await _activityService.UpdateActivityAsync(id, act);
            if (updated==null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _activityService.DeleteActivityAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
