using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("courses/save")]
        public async Task<IActionResult> SaveCourse(int id, string title, string? description,
            string? content, string? category, int durationHours, int difficultyLevel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                if (id == 0)
                {
                    var course = new Course
                    {
                        Title = title,
                        Description = description,
                        Content = content,
                        Category = category,
                        DurationHours = durationHours,
                        DifficultyLevel = difficultyLevel,
                        InstructorId = userId,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _courseService.CreateCourseAsync(course);
                }
                else
                {
                    var course = await _courseService.GetCourseByIdAsync(id);
                    if (course != null)
                    {
                        course.Title = title;
                        course.Description = description;
                        course.Content = content;
                        course.Category = category;
                        course.DurationHours = durationHours;
                        course.DifficultyLevel = difficultyLevel;
                        course.UpdatedDate = DateTime.UtcNow;
                        await _courseService.UpdateCourseAsync(course);
                    }
                }
                return Redirect("/courses");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
