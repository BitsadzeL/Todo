using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapi.Models;

namespace todoapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{


    private readonly TasksDBContext _DBContext;

    public TaskController(TasksDBContext dBContext)
    {
        this._DBContext=dBContext;
    }


[HttpGet("GetAllTasks")]
public async Task<IActionResult> GetAll()
{
    try
    {
        var tasks = await this._DBContext.Tasks.ToListAsync();
        return Ok(tasks);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred while retrieving tasks: {ex.Message}");
    }
}


[HttpPost("AddTask")]
public async Task<IActionResult> AddTask([FromBody] Task task)
{
    try
    {
        if (task == null)
        {
            return BadRequest("Task object is null");
        }

        // Add the new task to the DbSet
        _DBContext.Tasks.Add(task);

        // Save changes to the database
        await _DBContext.SaveChangesAsync();

        // Return the newly added task
        return Ok(task);
    }
    catch (DbUpdateException ex)
    {
        return StatusCode(500, $"An error occurred while adding the task: {ex.InnerException?.Message}");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred while adding the task: {ex.Message}");
    }
}




[HttpDelete("DeleteTask/{id}")]
public async Task<IActionResult> DeleteTask(int id)
{
    try
    {
        // Find the task with the specified id
        var task = await _DBContext.Tasks.FindAsync(id);
        
        if (task == null)
        {
            return NotFound($"Task with ID {id} not found");
        }

        // Remove the task from the DbSet
        _DBContext.Tasks.Remove(task);

        // Save changes to the database
        await _DBContext.SaveChangesAsync();

        // Return the deleted task
        return Ok(task);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred while deleting the task: {ex.Message}");
    }
}



}
