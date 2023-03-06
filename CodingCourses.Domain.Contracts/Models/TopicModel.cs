namespace CodingCourses.Domain.Contracts.Models
{
    public class TopicModel
    {
        public TopicModel()
        { }

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

        public int? Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int OrderNumber { get; set; }

    }
}
