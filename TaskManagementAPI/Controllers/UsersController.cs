using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories.Interfaces;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController(IUserRepository userRepository) : ControllerBase
    { 
         

        [HttpGet]
        public IActionResult GetAll() => Ok(userRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(userRepository.GetById(id));

        [HttpPost]
        public IActionResult Create(User user)
        {
            userRepository.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            userRepository.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            userRepository.Delete(id);
            return NoContent();
        }
    }
}
