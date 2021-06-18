using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using mhms3.Models;

namespace mhms3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
  
        public DbSet<mhms3.Models.Client> Client { get; set; }
        public DbSet<mhms3.Models.Appointment> Appointment { get; set; }
        public DbSet<mhms3.Models.Session> Session { get; set; }
        
    }
}
