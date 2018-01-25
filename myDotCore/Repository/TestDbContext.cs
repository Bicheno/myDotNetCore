using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace Repository
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {

        }

        public DbSet<users> users { get; set; }
        public DbSet<todo> todo { get; set; }
        public DbSet<done> done { get; set; }

    }
}
