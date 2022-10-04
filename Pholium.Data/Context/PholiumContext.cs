using Microsoft.EntityFrameworkCore;
using Pholium.Data.Extensions;
using Pholium.Data.Mappings;
using Pholium.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pholium.Data.Context
{
    public class PholiumContext : DbContext
    {
        public PholiumContext(DbContextOptions<PholiumContext> option)
            : base(option) { }
        #region DBSets

        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            modelBuilder.ApplyGlobalConfigurations();
            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }
    }
}
