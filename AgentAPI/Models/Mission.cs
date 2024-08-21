using System.ComponentModel.DataAnnotations;

namespace AgentAPI.Models
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }
        public Agent? agent { get; set; }
        public Target? target { get; set; }
        public double? arrivalTime { get; set; }
        public string? Status { get; set; }



    }
}
