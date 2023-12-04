using Domain.Users.Employees.Availabilities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oogarts.Persistence.Configurations;

namespace Persistence.Configurations.Users.Doctors.Availabilities;
internal class AvailabilityConfiguration : EntityConfiguration<Availability>
{
    public override void Configure(EntityTypeBuilder<Availability> builder)
    {
        base.Configure(builder);

        builder.Property(ec => ec.Day).IsRequired();
        builder.Property(ec => ec.Start).IsRequired();
        builder.Property(ec => ec.End).IsRequired();
    }
}