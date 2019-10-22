using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class TimeEntryContext : DbContext
    {
        public DbSet<TimeEntryRecord> TimeEntryRecords { get; set; }
        public TimeEntryContext(DbContextOptions options) : base(options)
        {
        }
        
    }
}

