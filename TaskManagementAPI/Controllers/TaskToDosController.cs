using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories.Interfaces;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskToDosController : ControllerBase
    {
        private readonly TaskToDoService _taskToDoService;
        private readonly ITaskToDoRepository _taskToDoRepository;

        public TaskToDosController(TaskToDoService taskToDoService, ITaskToDoRepository taskToDoRepository)
        {
            _taskToDoService = taskToDoService;
            _taskToDoRepository = taskToDoRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get all tasks")]
        public IActionResult GetAllTasks() => Ok(_taskToDoRepository.GetAll());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Get task by ID")]
        public IActionResult GetById(int id) => Ok(_taskToDoService.GetById(id));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Create a new task")]
        public IActionResult Create(TaskToDo taskToDo)
        {
            _taskToDoService.Add(taskToDo);
            return CreatedAtAction(nameof(GetById), new { id = taskToDo.Id }, taskToDo);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update an existing task")]
        public IActionResult Update(TaskToDo taskToDo)
        {
            _taskToDoService.Update(taskToDo);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Update task status")]
        public IActionResult UpdateTaskStatus(int id, TStatus newTaskStatus)
        {
            _taskToDoService.UpdateTaskStatus(id, newTaskStatus);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete a task")]
        public IActionResult Delete(int id)
        {
            _taskToDoService.Delete(id);
            return NoContent();
        }
    }
}