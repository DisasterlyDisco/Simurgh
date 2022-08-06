namespace Simurgh.Models
{
    public class TimeSensitiveTask
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }

        public TimeSensitiveTask()
        {

        }
    }
}
