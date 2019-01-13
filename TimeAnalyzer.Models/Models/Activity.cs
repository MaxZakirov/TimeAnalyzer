namespace TimeAnalyzer.Domain.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public ActivityType Type { get; set; }
    }
}
