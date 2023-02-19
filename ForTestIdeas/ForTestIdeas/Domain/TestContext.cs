using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ForTestIdeas.Domain.Entities;
using ForTestIdeas.Models;

namespace ForTestIdeas.Domain
{
    public class TestContext : DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<TaskTiket> TaskTikets { get; set; }   

        public TestContext()
        {

        }
        public TestContext(DbContextOptions<TestContext> options) :base(options)
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
      
    }
}
