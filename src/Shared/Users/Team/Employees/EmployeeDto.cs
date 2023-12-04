using Oogarts.Shared.Users.Doctors.Availabilities;

namespace Oogarts.Shared.Users.Doctors.Employees;

public abstract class EmployeeDto
{
	public class Index
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
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
	}
}
