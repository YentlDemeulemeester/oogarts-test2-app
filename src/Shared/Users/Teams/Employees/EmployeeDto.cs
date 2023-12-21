using FluentValidation;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Teams.Biographies;
using Shared.Users.Teams.Groups;

namespace Shared.Users.Doctors.Employees;

public abstract class EmployeeDto
{
	public class Index
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
		public GroupDto.Index? Group { get; set; }
	}

	public class Detail
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
        public string? Image { get; set; }
        public DateTime Birthdate { get; set; }
		public string? Email { get; set; }
		public string? Phonenumber { get; set; }
		public GroupDto.Index? Group { get; set; }
		public IEnumerable<AvailabilityDto.Index>? Availabilities { get; set; }
	}

	public class Mutate
	{
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
        public string? Image { get; set; }
        public DateTime? Birthdate { get; set; }
		public string? Email { get; set; }
		public string? Phonenumber { get; set; }
		public GroupDto.Index? Group { get; set; }

		public class Validator : AbstractValidator<Mutate>
		{
			public Validator()
			{
				RuleFor(x => x.Firstname).NotEmpty().MaximumLength(75);
				RuleFor(x => x.Lastname).NotEmpty().MaximumLength(75);
				RuleFor(x => x.Birthdate).NotNull();
				RuleFor(x => x.Email).NotEmpty().MaximumLength(100);
				RuleFor(x => x.Phonenumber).MaximumLength(15);
				RuleFor(x => x.Group).NotNull();
			}
		}
	}
}
