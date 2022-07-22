using Microsoft.EntityFrameworkCore;

namespace LogisticsAssistant.Data
{
    public class LogisticsAssistantContext : DbContext
    {
        public LogisticsAssistantContext (DbContextOptions<LogisticsAssistantContext> options)
            : base(options)
        {
        }

        public DbSet<LogisticsAssistant.Models.Lorries> Lorries { get; set; } = default!;

        public DbSet<LogisticsAssistant.Models.ScheduledTrips>? ScheduledTrips { get; set; }
    }
}
