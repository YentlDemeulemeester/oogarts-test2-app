namespace Domain.Users.Employees.Availabilities;

public class Availability : Entity
{
    private DateTime startDate = default!;
    public DateTime StartDate
    {
        get => startDate;
        set => startDate = Guard.Against.Null(value, nameof(startDate));
    }
    private DateTime endDate = default!;
    public DateTime EndDate
    {
        get => endDate;
        set => endDate = Guard.Against.Null(value, nameof(endDate));
    }

    public long EmployeeId { get; private set; }

    private Availability() { }

    public Availability(DateTime startDate, DateTime endDate, long employeeId)
    {
        StartDate = startDate;
        EndDate = endDate;
        EmployeeId = Guard.Against.NegativeOrZero(employeeId, nameof(employeeId));
    }
}
