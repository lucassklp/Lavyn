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
            builder.Property(x => x.Key).HasMaxLength(64);
            builder.Property(x => x.LastMessageDate);
            builder.Property(x => x.Name).HasMaxLength(50);
            
            builder.HasMany(x => x.Messages)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);

            builder.HasMany(x => x.UserHasRoom)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);
            
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.Key);
        }
    }
}