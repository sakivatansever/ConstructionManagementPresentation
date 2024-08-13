using ConstructionManagementPresentation.Models;
using ConstructionManagementPresentation.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementPresentation.Controllers
{
    public class WorkersController : Controller
    {
        private readonly WorkerService _workerServcice;

        public WorkersController(WorkerService workerService)
        {
            _workerServcice = workerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var workers = await _workerServcice.GetAllWorkersAsync();
            return View(workers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var worker = await _workerServcice.GetWorkerByIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                await _workerServcice.CreateWorkerAsync(worker);
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var worker = await _workerServcice.GetWorkerByIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(int id, Worker worker)
        {
            if (id != worker.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var updated = await _workerServcice.UpdateWorkerAsync(id, worker);
            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var worker = await _workerServcice.GetWorkerByIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _workerServcice.DeleteWorkerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
