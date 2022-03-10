using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.EntityFramework.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserCredentials>
    {
        public void Configure(EntityTypeBuilder<UserCredentials> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(u => u.Name).HasMaxLength(15);
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(30);
            builder.Property(u => u.Password).IsRequired();
        }
    }
}
