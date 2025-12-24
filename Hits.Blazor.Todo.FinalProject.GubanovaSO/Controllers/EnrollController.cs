using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class EnrollController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll([FromForm] int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                await _enrollmentService.EnrollUserAsync(userId, courseId);
                return Redirect("/my-learning");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("unenroll/{enrollmentId:int}")]
        public async Task<IActionResult> Unenroll(int enrollmentId)
        {
            try
            {
                await _enrollmentService.DeleteEnrollmentAsync(enrollmentId);
                return Redirect("/my-learning");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
