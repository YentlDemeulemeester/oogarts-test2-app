using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oogarts.Domain.Users.Doctors;

namespace Oogarts.Persistence.Configurations.Users.Doctors;

internal class DoctorConfiguration : EntityConfiguration<Doctor>
{
	public override void Configure(EntityTypeBuilder<Doctor> builder)
	{
		builder
			.HasMany(x => x.Specializations)
			.WithMany()
			.UsingEntity(j => j.ToTable("DoctorSpecialization"));
	}
}
