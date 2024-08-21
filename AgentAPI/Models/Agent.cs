using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AgentAPI.Models
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? X { get; set; }

        public int? Y { get; set; }
        
        public string? Status { get; set; }


    }
}
