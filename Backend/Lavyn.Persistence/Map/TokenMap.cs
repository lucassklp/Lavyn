using Lavyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lavyn.Persistence.Map
{
    public class TokenMap : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable("tokens");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Type);
            builder.Property(x => x.UserId);
            builder.Property(x => x.Value).HasMaxLength(150);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tokens)
                .HasForeignKey(x => x.UserId);

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Type);
        }
    }
}