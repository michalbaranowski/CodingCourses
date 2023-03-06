using System.ComponentModel.DataAnnotations;

namespace CodingCourses.DataAccess.Contracts.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string Value { get; set; } = string.Empty;

        public int OrderNumber { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();
    }
}
