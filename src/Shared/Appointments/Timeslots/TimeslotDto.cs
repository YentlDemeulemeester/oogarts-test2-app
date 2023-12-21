namespace Shared.Appointments.Timeslots;

public abstract class TimeslotDto
{
	public class Index
	{
		public long Id { get; set; }
		public DateOnly Date {  get; set; }
		public TimeOnly Time { get; set; }
		public TimeSpan Duration { get; set; }
	}
}
