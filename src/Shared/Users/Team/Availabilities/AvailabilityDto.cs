namespace Oogarts.Shared.Users.Doctors.Availabilities;
public abstract class AvailabilityDto
{
	public class Index
	{
		public long Id { get; set; }
		public DateOnly Day {  get; set; }
		public TimeOnly Start { get; set; }
		public TimeOnly End { get; set; }
	}
}
