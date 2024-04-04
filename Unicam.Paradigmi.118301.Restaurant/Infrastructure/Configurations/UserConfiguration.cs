using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey("UserId");
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(u => u.Salt)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
