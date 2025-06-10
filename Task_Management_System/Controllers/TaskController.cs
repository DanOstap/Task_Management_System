using Microsoft.AspNetCore.Mvc;
using Task_Management_System.Models;
using Task_Management_System.Service;

namespace Task_Management_System.Controllers;

[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly ITaskService taskService;

    public TaskController(ITaskService taskService)
    {
        this.taskService = taskService;
    }

    [HttpPost]
    async public Task<ActionResult<Tasks>> CreateTask([FromBody]Tasks model)
    {
        var newTask = await taskService.CreateTask(model);
        return Ok(newTask);
    }

    [HttpPut("/by/{id}")]
    async public Task<IActionResult> UpdateTask([FromRoute]int id, [FromBody] Tasks model)
    {
        var UpdatedTask = await taskService.UpdateTask(id, model);
        if(UpdatedTask != null) return Ok(UpdatedTask);
        return NotFound();
    }

    [HttpDelete("/by/{id}")]
    async public Task<ActionResult<Tasks>> DeleteTask([FromRoute]int id)
    {
        var DeletedTask = await taskService.DeleteTask(id);
        return DeletedTask;
    }

    [HttpGet("/by/{id}")]
    async public Task<ActionResult<Tasks>> GetTaskById([FromRoute] int id)
    {
        var task = await taskService.GetTaskById(id);
        return task;
    }

    [HttpGet]
    async public Task<IActionResult> GetAllTasks()
    {
        var tasks = await taskService.GetAllTasks();
        return Ok(tasks);
    }

    [HttpGet("/by/{userId}")]
    async public Task<IActionResult> GetTasksByUser([FromQuery]int userId)
    {
        var  tasks = await taskService.GetTasksByUser(userId);
        return Ok(tasks);
    }

    [HttpPost("{userId}/{taskId}")]
    async Task<ActionResult<Tasks>> AddUserToTask([FromQuery] int userId, [FromQuery] int taskId)
    {
        var task  = await taskService.AddUserToTask(userId, taskId);
        return task; 
    }

}