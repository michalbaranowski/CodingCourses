namespace CodingCourses.Domain.Contracts.Models
{
    public class CourseModel
    {
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

        public int? Id { get; }
        public string Name { get; }
        public string Description { get; }

        public IList<TopicModel> Topics { get; } = new List<TopicModel>();
    }
}
