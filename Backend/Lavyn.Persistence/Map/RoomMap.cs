using Lavyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lavyn.Persistence.Map
{
    public class RoomMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("rooms");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Type);
            
            builder.HasMany(x => x.Messages)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Type);
        }
    }
}