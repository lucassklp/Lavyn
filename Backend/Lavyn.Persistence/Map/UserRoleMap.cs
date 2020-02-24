using Lavyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lavyn.Persistence.Map
{
    class UserRoleMap : IEntityTypeConfiguration<UserHasRole>
    {
        public void Configure(EntityTypeBuilder<UserHasRole> builder)
        {
            builder.ToTable("user_has_roles");

            builder.Property(x => x.UserId);
            builder.Property(x => x.RoleId);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
