namespace Oogarts.Shared.Users.Doctors.Availabilities;
public abstract class AvailabilityDto
{
	public class Index
	{
		public long Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
