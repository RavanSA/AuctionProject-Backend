namespace Persistence.Configurations
{
    using Common;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MessageConfiguration : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder
                .ToTable("Messages");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Message)
                .IsRequired();


            builder
                .Property(p => p.SellerId)
                .IsRequired();


            builder
                .Property(p => p.BidderId)
                .IsRequired();

            builder
                .Property(p => p.ItemId)
                .IsRequired();

        }
    }
}