using AuthService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Data.EntityFramework.Context
{
    public class EFContext:DbContext
    {
        public EFContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public DbSet<AuthorizationRecord> AuthorizationRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this.Configuration.GetConnectionString("AuthorizationServiceContext"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Mappings.AuthorizationRecordMap.Map(modelBuilder.Entity<AuthorizationRecord>());
        }
        
    }
}
