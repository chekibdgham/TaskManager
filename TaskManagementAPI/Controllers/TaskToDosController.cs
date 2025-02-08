using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskToDosController : ControllerBase
    {
        private readonly TaskToDoService _taskToDoService;

        public TaskToDosController(TaskToDoService taskToDoService)
        {
            _taskToDoService = taskToDoService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_taskToDoService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_taskToDoService.GetById(id));

        [HttpPost]
        public IActionResult Create(TaskToDo taskToDo)
        {
            _taskToDoService.Add(taskToDo);
            return CreatedAtAction(nameof(GetById), new { id = taskToDo.Id }, taskToDo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskToDo taskToDo)
        {
            _taskToDoService.Update(taskToDo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _taskToDoService.Delete(id);
            return NoContent();
        }
    }
}
