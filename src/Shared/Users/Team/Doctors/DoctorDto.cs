using Oogarts.Shared.Appointments.Timeslots;
using Oogarts.Shared.Users.Doctors.Availabilities;
using Oogarts.Shared.Users.Doctors.Specializations;

namespace Oogarts.Shared.Users.Team.Doctors;

public abstract class DoctorDto
{
	public class Index
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public IEnumerable<SpecializationDto.Index>? Specializations { get; set; }
	}

	public class Detail
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public DateOnly? Birthdate { get; set; }
		public string? Email { get; set; }
		public string? Phonenumber { get; set; }
		public IEnumerable<AvailabilityDto.Index>? Availabilities { get; set; }
		public IEnumerable<SpecializationDto.Index>? Specializations { get; set; }
		public IEnumerable<TimeslotDto.Index>? Timeslots { get; set; }
	}
}
