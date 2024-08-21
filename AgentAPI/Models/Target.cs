using System.ComponentModel.DataAnnotations;
namespace AgentAPI.Models
{
    public class Target
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? X { get; set; }
        public int? Y { get; set; }
        public string? Photo_url { get; set; } = string.Empty;
        public string? Status { get; set; }
    }
}
