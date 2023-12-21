using FluentValidation;

namespace Shared.Users.Doctors.Availabilities;

public abstract class AvailabilityDto
{
    public class Index
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long EmployeeId { get; set; }
    }

    public class Mutate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long EmployeeId { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.StartDate).NotEmpty();
                RuleFor(x => x.EndDate).NotEmpty();
                RuleFor(x => x.EmployeeId).NotEmpty().GreaterThan(0); // Ensure that EmployeeId is provided and valid
            }
        }
    }
}

