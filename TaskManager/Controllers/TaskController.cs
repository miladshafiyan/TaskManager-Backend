using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _TaskService;

        public TaskController(TaskService taskService) =>
            _TaskService = taskService;

        [HttpGet]
        public async Task<List<task>> Get() =>
            await _TaskService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<task>> Get(string UserName, string Password)
        {
            var task = await _TaskService.GetAsync(UserName);
            if (task != null)
            {
               return Ok(task); 
            }

            return StatusCode(404);
        }

        [HttpPost]
        public async Task<IActionResult> Post(task newtask)
        {
            await _TaskService.CreateAsync(newtask);

            return CreatedAtAction(nameof(Get), new { id = newtask._id }, newtask);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, task updatedTask)
        {
            var task = await _TaskService.GetAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            updatedTask._id = task._id;

            await _TaskService.UpdateAsync(id, updatedTask);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var task = await _TaskService.GetAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            await _TaskService.RemoveAsync(id);

            return NoContent();
        }
    }
}
