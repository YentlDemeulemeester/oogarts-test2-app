using FluentValidation;
using Oogarts.Shared.Appointments.Timeslots;
using Oogarts.Shared.Users.Patients;

namespace Oogarts.Shared.Appointments;
public abstract class AppointmentDto
{
    public class Index
    {
        public long Id { get; set; }
        public TimeslotDto.Index? Timeslot { get; set; }
    }

    public class Detail
    {
        public long Id { get; set; }
        public PatientDto.Detail? Patient { get; set; }
        public TimeslotDto.Index? Timeslot { get; set; }
        public string? Reason { get; set; }
        public string? Note { get; set; }
	}

    public class Mutate
    {
        public long PatientId { get; set; }
        public long TeamMemberId { get; set; }
        public DateTime Date { get; set; }
        public string? ExtraInfo { get; set; }
    }

    public class MutateValidator : AbstractValidator<Mutate>
    {
        public MutateValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0);
            RuleFor(x => x.TeamMemberId).GreaterThan(0);
            RuleFor(x => x.Date).NotNull();
            RuleFor(x => x.ExtraInfo).MaximumLength(500);
        }
    }

	public class Create
	{
        public int PatientId { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime? Date { get; set; }
        public string? ExtraInfo { get; set; }
	}
}