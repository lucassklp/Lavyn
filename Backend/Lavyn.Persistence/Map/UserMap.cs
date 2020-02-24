using Lavyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lavyn.Persistence.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Password).HasMaxLength(500);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.IsOnline);
            builder.Property(x => x.LastLogin);
            
            
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.IsOnline);
        }
    }
}
