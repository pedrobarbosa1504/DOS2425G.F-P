using Microsoft.AspNetCore.Mvc;
using TMS.Models;
using System.Linq;



namespace TMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private static readonly List<Comment> comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                Text = "This task needs to be done ASAP!",
                TaskId = 1,
                Task = new TaskItem { Id = 1, Title = "Bug reported - fix", Description = "A bug was detected in Service X" }
            },
            new Comment
            {
                Id = 2,
                Text = "The project has a deadline next week.",
                TaskId = 2,
                Task = new TaskItem { Id = 2, Title = "New Functionality - use C#", Description = "It's necessary use .NET Core in these lessons" }
            }
        };

      
        [HttpGet]
        public IActionResult GetComments()
        {
            return Ok(comments);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found.");

            return Ok(comment);
        }

      
        [HttpPost]
        public IActionResult CreateComment([FromBody] Comment comment)
        {
            comment.Id = comments.Max(c => c.Id) + 1;  
            comments.Add(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromBody] Comment updatedComment)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found.");

            comment.Text = updatedComment.Text;
            comment.TaskId = updatedComment.TaskId; 

            return NoContent(); 
        }

  
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found.");

            comments.Remove(comment);
            return NoContent();  
        }
    }
}
