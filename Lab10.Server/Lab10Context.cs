using Lab10.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Server
{
    public class Lab10Context : DbContext
    {
        public Lab10Context()
        {
        }

        public Lab10Context(DbContextOptions<Lab10Context> options): base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<LoginTimeout> Timeouts { get; set; } = null!;
    }
}
