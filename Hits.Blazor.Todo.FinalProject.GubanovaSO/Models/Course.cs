using System.ComponentModel.DataAnnotations;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Course
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(5000)]
        public string? Content { get; set; }

        [StringLength(100)]
        public string? Category { get; set; }

        [Range(1, 1000)]
        public int DurationHours { get; set; } = 1;

        [Range(1, 3)]
        public int DifficultyLevel { get; set; } = 1;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public string? InstructorId { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
