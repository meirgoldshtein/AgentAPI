using Microsoft.EntityFrameworkCore;
using AgentAPI.Models;
namespace AgentAPI.Context
{
    public class AgentDbContext : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Target> Targets { get; set; }
        public DbSet<Mission> Missions { get; set; }


        // זריעת נתונים ראשוניים לשם בדיקה
        private void Seed()
        {
            if (!Agents.Any() && !Targets.Any() && !Missions.Any())
            {
                // זריעת סוכן ומשימה שכבר מקושרים
                Target target1 = new Target { Name = "Sinuwar", Description = "Hamass Commander",X = 5, Y = 3,  Status = "b" };
                Targets.Add(target1);
                SaveChanges();
                Agent agent1 = new Agent { Name = "AmitNakesh", X = 50, Y = 55, Status = "b" };
                Agents.Add(agent1);
                SaveChanges();


                // זריעת משימה שכבר שודכה
                Mission mission = new Mission { agent = agent1, target = target1, Status = "a" };
                Missions.Add(mission);
                SaveChanges();

                // זריעת סוכן ומשימה מרוחקים
                Target target2 = new Target { Name = "Abu Ali", Description = "Huti Commander", X = 888, Y = 863, Status = "a" };
                Targets.Add(target2);
                SaveChanges();
                Agent agent2 = new Agent { Name = "AmitPotsets", X = 60, Y = 66,  Status = "a" };
                Agents.Add(agent2);
                SaveChanges();
              
                // זריעת מטרה שחוסלה
                Target target3 = new Target { Name = "Haminay", Description = "Iran Commander", X = 435, Y = 613, Status = "c" };
                Targets.Add(target3);
                SaveChanges();
                

            }
        }
        public AgentDbContext(DbContextOptions<AgentDbContext> options) : base(options)
        {
            // לוודא שהבסיס נתונים והטבלאות קיימות, אם לא תייצר את כולם 
            Console.WriteLine("Database Exists: " + Database.EnsureCreated());
            Seed();
        }


    }
}
