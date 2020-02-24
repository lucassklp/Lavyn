using Lavyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lavyn.Persistence.Map
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Date);
            builder.Property(x => x.Content).HasColumnType("MEDIUMTEXT");
            builder.Property(x => x.RoomId);
            builder.Property(x => x.SenderId);

            builder.HasOne(x => x.Room)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.RoomId);

            builder.HasOne(x => x.Sender)
                .WithMany(x => x.SentMessages)
                .HasForeignKey(x => x.SenderId);
            
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.RoomId);
        }
    }
}