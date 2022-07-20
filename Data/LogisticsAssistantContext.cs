using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogisticsAssistant.Models;

namespace LogisticsAssistant.Data
{
    public class LogisticsAssistantContext : DbContext
    {
        public LogisticsAssistantContext (DbContextOptions<LogisticsAssistantContext> options)
            : base(options)
        {
        }

        public DbSet<LogisticsAssistant.Models.Lorries> Lorries { get; set; } = default!;
    }
}
