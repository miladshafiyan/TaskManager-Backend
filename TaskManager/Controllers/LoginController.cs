using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _UserService;

        public LoginController(UserService userService) =>
            _UserService = userService;

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _UserService.GetAsync();

        [HttpGet("{UserName}/{Password}")]
        public async Task<ActionResult<User>> Get(string UserName, string Password)
        {
            var user = await _UserService.GetAsync(UserName);
            if (user != null)
            {
                if (user.UserName == UserName && user.Password == Password)
                {
                    return Ok(user);
                }
            }
            
            return StatusCode(404);            
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(User newuser)
        {
            await _UserService.CreateAsync(newuser);

            return CreatedAtAction(nameof(Get), new { id = newuser._id }, newuser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedTask)
        {
            var user = await _UserService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedTask._id = user._id;

            await _UserService.UpdateAsync(id, updatedTask);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _UserService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _UserService.RemoveAsync(id);

            return NoContent();
        }
    }
}
