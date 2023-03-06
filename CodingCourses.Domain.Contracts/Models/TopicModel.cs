namespace CodingCourses.Domain.Contracts.Models
{
    public class TopicModel
    {
        public TopicModel(
            string value,
            int orderNumber)
        {
            Value = value;
            OrderNumber = orderNumber;
        }

        public TopicModel(
            int id,
            string value,
            int orderNumber)
        {
            Id = id;
            Value = value;
            OrderNumber = orderNumber;
        }

        public int? Id { get; }
        public string Value { get; }
        public int OrderNumber { get; }

    }
}
