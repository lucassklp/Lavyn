using Lavyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lavyn.Persistence.Map
{
    public class UserHasRoomMap : IEntityTypeConfiguration<UserHasRoom>
    {
        public void Configure(EntityTypeBuilder<UserHasRoom> builder)
        {
            builder.ToTable("user_has_room");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RoomId);
            builder.Property(x => x.UserId);
            builder.Property(x => x.LastSeen);

            builder.HasOne(x => x.Room)
                .WithMany(x => x.UserHasRoom)
                .HasForeignKey(x => x.RoomId);
            
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRooms)
                .HasForeignKey(x => x.UserId);
            
            
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.RoomId);
        }
    }
}