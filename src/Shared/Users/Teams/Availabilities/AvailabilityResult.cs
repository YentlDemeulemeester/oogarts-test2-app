namespace Oogarts.Shared.Users.Doctors.Availabilities;
public abstract class AvailabilityResult
{
	public class Index
	{
		public IEnumerable<AvailabilityDto.Index>? Availabilities { get; set; }
		public int TotalAmount { get; set; }
	}
}
