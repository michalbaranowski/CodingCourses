using System.ComponentModel.DataAnnotations;

namespace CodingCourses.DataAccess.Contracts.Entities
{
    public class Course
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}
