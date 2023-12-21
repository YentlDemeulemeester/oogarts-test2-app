namespace Shared.Users.Doctors.Availabilities;

public abstract class AvailabilityResult
{
    public class Index
    {
        public IEnumerable<AvailabilityDto.Index>? Availabilities { get; set; }
        public int TotalAmount { get; set; }
    }

    public class Create
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long EmployeeId { get; set; } // Added EmployeeId property
    }
}

