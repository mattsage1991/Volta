using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volta.UserAccess.Domain.Users;

namespace Volta.UserAccess.Infrastructure.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne<Email>("email", b =>
            {
                b.Property(x => x.Value).HasColumnName("Email");
            });

            builder.OwnsOne<Domain.Users.Password>("password", b =>
            {
                b.Property(x => x.Hash).HasColumnName("Password");
            });

            builder.OwnsOne<Title>("title", b =>
            {
                b.Property(x => x.Value).HasColumnName("Title");
            });

            builder.OwnsOne<FirstName>("firstName", b =>
            {
                b.Property(x => x.Value).HasColumnName("FirstName");
            });

            builder.OwnsOne<LastName>("lastName", b =>
            {
                b.Property(x => x.Value).HasColumnName("LastName");
            });

            builder.OwnsOne<Address>("address", b =>
            {
                b.Property(x => x.Value).HasColumnName("Address");
            });

            builder.OwnsOne<PhoneNumber>("phoneNumber", b =>
            {
                b.Property(x => x.Value).HasColumnName("PhoneNumber");
            });

            builder.OwnsOne<CreatedDate>("createdDate", b =>
            {
                b.Property(x => x.Value).HasColumnName("CreatedDate");
            });

            builder.Property("isActive").HasColumnName("IsActive");

            builder.Ignore(x => x.DomainEvents);
        }
    }
}