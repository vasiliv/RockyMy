using Microsoft.EntityFrameworkCore;
using RockyMy.Models;
using System;

namespace RockyMy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
