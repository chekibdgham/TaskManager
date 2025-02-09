using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
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

        public TaskToDosController(TaskToDoService taskToDoService)
        {
            _taskToDoService = taskToDoService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get all tasks")]
        public IActionResult GetAllTasks(CancellationToken cancellationToken) => Ok(_taskToDoService.GetAll(cancellationToken));

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Get task by ID")]
        public IActionResult GetById(int id, CancellationToken cancellationToken) => 
            Ok(_taskToDoService.GetByIdAsync(id,cancellationToken));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Create a new task")]
        public async Task<IActionResult> Create(TaskToDo taskToDo, CancellationToken cancellationToken)
        {
            await _taskToDoService.Add(taskToDo, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = taskToDo.Id }, taskToDo);
        }

        [HttpPut("UpdateTask")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update an existing task")]
        public async Task<IActionResult> Update(TaskToDo taskToDo, CancellationToken cancellationToken)
        {
            await _taskToDoService.Update(taskToDo, cancellationToken);
            return NoContent();
        }

        [HttpPut("UpdateStatusTask/{id}")]
        [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Update task status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, TStatus newTaskStatus, CancellationToken cancellationToken)
        {
            await _taskToDoService.UpdateTaskStatus(id, newTaskStatus, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete a task")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _taskToDoService.Delete(id, cancellationToken);
            return NoContent();
        }
    }
}