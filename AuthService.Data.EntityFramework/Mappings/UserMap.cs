using AuthService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Data.EntityFramework.Mappings
{
    public class UserMap 
    {
        public UserMap()
        {


        }
        public static void Map(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);
            builder.ToTable("Users");

        }
    }
}
