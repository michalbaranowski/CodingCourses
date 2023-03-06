namespace CodingCourses.Domain.Contracts.Models
{
    public class CourseModel
    {
        public CourseModel()
        { }

        public CourseModel(
            string name,
            string description,
            IList<TopicModel> topics)
        {
            Name = name;
            Description = description;
            Topics = topics;
        }

        public CourseModel(
            int id,
            string name,
            string description,
            IList<TopicModel> topics)
        {
            Id = id;
            Name = name;
            Description = description;
            Topics = topics;
        }

        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public IList<TopicModel> Topics { get; set; } = new List<TopicModel>();
    }
}
