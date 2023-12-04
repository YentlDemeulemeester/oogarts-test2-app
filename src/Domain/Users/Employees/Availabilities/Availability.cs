namespace Domain.Users.Employees.Availabilities;

public class Availability : Entity
{
    private DateOnly day = default!;
    public DateOnly Day
    {
        get => day;
        set => day = Guard.Against.Null(value, nameof(day));
    }

    private TimeOnly start = default!;
    public TimeOnly Start
    {
        get => start;
        set => start = Guard.Against.Null(value,nameof(start));
    }

    private TimeOnly end = default!;
    public TimeOnly End
    {
        get => end;
        set => end = Guard.Against.Null(value, nameof(end));
    }

    //Database constructor
    private Availability() { }

    public Availability(DateOnly day, TimeOnly start, TimeOnly end)
    {
        Day = day;
        Start = start;
        End = end;
    }
}
