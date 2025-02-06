using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private static readonly List<Project> projects = new List<Project>
    {
        new Project { Id = 1, Name = "Project 1", Description = "First project", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2) }
    };

    [HttpGet]
    public IActionResult GetProjects()
    {
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public IActionResult GetProject(int id)
    {
        var project = projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound($"Project with ID {id} not found.");

        return Ok(project);
    }

    [HttpPost]
    public IActionResult CreateProject([FromBody] Project project)
    {
        project.Id = projects.Max(p => p.Id) + 1;
        projects.Add(project);
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProject(int id, [FromBody] Project updatedProject)
    {
        var project = projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound($"Project with ID {id} not found.");

        project.Name = updatedProject.Name;
        project.Description = updatedProject.Description;
        project.StartDate = updatedProject.StartDate;
        project.EndDate = updatedProject.EndDate;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        var project = projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound($"Project with ID {id} not found.");

        projects.Remove(project);
        return NoContent();
    }
}
