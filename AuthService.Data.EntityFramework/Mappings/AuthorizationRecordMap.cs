using AuthService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Data.EntityFramework.Mappings
{
    public sealed class AuthorizationRecordMap
    {
        public static void Map(EntityTypeBuilder<AuthorizationRecord> builder)
        {
            builder.ToTable("AuthorizationRecord");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
        }
    }
}
