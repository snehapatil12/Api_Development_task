using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new();

        [HttpGet]
        public IActionResult GetAll() => Ok(Users);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.Id = Users.Count + 1;
            Users.Add(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = Users.FirstOrDefault(u => u.Id == id);
            if (existing == null)
                return NotFound();

            existing.FullName = updatedUser.FullName;
            existing.Email = updatedUser.Email;
            existing.Age = updatedUser.Age;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            Users.Remove(user);

            return NoContent();
        }
    }
}
