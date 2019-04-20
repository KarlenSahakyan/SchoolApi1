using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace School.Models.DBManager
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<school> Schools { get; set; }

        public DbSet<Person> Persons { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=USER-PC\SQLEXPRESS;Database=SchoolData;Trusted_Connection=True;");
        }
    }
}
