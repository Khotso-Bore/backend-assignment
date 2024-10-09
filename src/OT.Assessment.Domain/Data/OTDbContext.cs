using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Tester.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Domain.Data
{
    public class OTDbContext : DbContext
    {
        public OTDbContext(DbContextOptions<OTDbContext> options) : base(options) { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Spender>().HasNoKey();
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<CasinoWager> CasinoWagers { get; set; }

        public DbSet<Spender> Spenders { get; set; }
    }
}
