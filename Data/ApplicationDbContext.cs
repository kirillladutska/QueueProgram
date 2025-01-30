using Microsoft.EntityFrameworkCore;
using QueueProgram.Visit;

namespace QueueProgram.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VisitList> Visits { get; set; }
    }
}

