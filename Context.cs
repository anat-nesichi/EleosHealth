using Microsoft.EntityFrameworkCore;

namespace EleosHealth
{
    public class Context : DbContext
    {
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=meetings.db");
    }
}
