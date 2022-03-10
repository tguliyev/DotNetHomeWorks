using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BaseProject;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFcorePlugin.EntityFrameworkConf.Configurations
{
    internal class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasIndex(g => g.Type).IsUnique();
            builder.Property(g => g.Type).IsRequired(true);
        }
    }
}
