using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.API.Data;
using ToDo.API.Models;

namespace ToDo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext toDoDbContext;

        public ToDoController(ToDoDbContext toDoDbContext)
        {
            this.toDoDbContext = toDoDbContext;
        }

        //Get all tasks
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await toDoDbContext.ToDoTasks.ToListAsync();
            return Ok(tasks);
        }

        //Get a single task
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetSingleTask")]
        public async Task<IActionResult> GetSingleTask([FromRoute] Guid id)
        {
            var task = toDoDbContext.ToDoTasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task != null)
            {
                return Ok(task);
            }

            return NotFound("Invalid Task");
        }

        //Add a task
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] ToDoModel toDoModel)
        {
            toDoModel.Id = Guid.NewGuid();

            await toDoDbContext.ToDoTasks.AddAsync(toDoModel);
            await toDoDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingleTask), new { id = toDoModel.Id }, toDoModel);
        }

        //Updating a task
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] ToDoModel toDoModel)
        {
            var existingTask = await toDoDbContext.ToDoTasks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTask != null)
            {
                existingTask.TaskName = toDoModel.TaskName;
                existingTask.Priority = toDoModel.Priority;

                await toDoDbContext.SaveChangesAsync();
                return Ok(existingTask);
            }

            return NotFound("Task not found");
        }

        //Delete a task
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            var existingTask = await toDoDbContext.ToDoTasks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTask != null)
            {
                toDoDbContext.Remove(existingTask);

                await toDoDbContext.SaveChangesAsync();
                return Ok(existingTask);
            }

            return NotFound("Task not found");
        }
    }
}