using Microsoft.AspNetCore.Mvc;
using TMS.Models;
using System.Linq;  

namespace TMS.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id = 1,
                TicketNumber = "JS-1203",
                Title = "Bug reported - fix",
                Description = "A bug was detected in Service X",
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(2),
                Priority = "High",
                Assignee = new User { Id = 1, UserName = "john_doe", Email = "john@example.com", FullName = "John Doe", Role = "Developer" }
            },
            new TaskItem
            {
                Id = 2,
                TicketNumber = "AB-352",
                Title = "New Functionality - use C#",
                Description = "It's necessary use .NET Core in these lessons",
                IsCompleted = true,
                DueDate = DateTime.Now.AddDays(-1),
                Priority = "Medium",
                Assignee = new User { Id = 2, UserName = "jane_smith", Email = "jane@example.com", FullName = "Jane Smith", Role = "Project Manager" }
            },
            new TaskItem
            {
                Id = 3,
                TicketNumber = "AA-9855",
                Title = "Improvements",
                Description = "Create new stories to implement new features",
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(5),
                Priority = "Low",
                Assignee = new User { Id = 3, UserName = "alice_white", Email = "alice@example.com", FullName = "Alice White", Role = "QA Tester" }
            }
        };

        
        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(tasks);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            return Ok(task);
        }

      
        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;  
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

    
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            task.TicketNumber = updatedTask.TicketNumber;
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
            task.DueDate = updatedTask.DueDate;
            task.Priority = updatedTask.Priority;
            task.Assignee = updatedTask.Assignee;

            return NoContent();  
        }

    
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            tasks.Remove(task);
            return NoContent();
        }
    }
}
