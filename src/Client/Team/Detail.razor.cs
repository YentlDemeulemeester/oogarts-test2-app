using Microsoft.AspNetCore.Components;

namespace Client.Team;

public partial class Detail
{
    [Parameter] public int Id { get; set; }

    // private TeamMember member;

    private readonly List<string> images = new List<string> {
        "/images/doctor1.jpg", "/images/doctor2.jpg", "/images/doctor3.jpg", "/images/doctor4.jpg", "/images/doctor5.jpg", "/images/doctor6.png", "/images/doctor7.png" };
    private readonly List<string> function = new List<string> {
        "Oogarts (Algemeen)", "Cornea-specialist", "Cornea-specialist", "Orthoptist", "Orthoptist", "Orthoptist", "Cornea-specialist" };
    private readonly List<string> name = new List<string> {
        "Dr. Emily Patel", "Dr. Sarah Mitchell", "Dr. Laura Ramirez", "Dr. James Anderson", "Dr. Ramin Doker", "Dr. David Wilson", "Michael Chang" };

    private readonly List<string> predefinedDays = new List<string>
    {
        "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag"
    };
    private readonly List<List<WorkHour>> workHours = new List<List<WorkHour>>
    {
        new List<WorkHour>
        {
            new WorkHour { StartHour = "09:00", EndHour = "17:00" },
            new WorkHour { StartHour = "08:30", EndHour = "16:00" },
            new WorkHour { StartHour = "afwezig", EndHour = "afwezig" },
            new WorkHour { StartHour = "09:00", EndHour = "17:00" },
            new WorkHour { StartHour = "08:30", EndHour = "16:00" },
            new WorkHour { StartHour = "afwezig", EndHour = "afwezig" },
            new WorkHour { StartHour = "afwezig", EndHour = "afwezig" }
        }, // Doctor 0

        new List<WorkHour>
        {
            new WorkHour { StartHour = "08:00", EndHour = "16:0" },
            new WorkHour { StartHour = "09:00", EndHour = "17:00" },
            new WorkHour { StartHour = "08:30", EndHour = "16:00" },
            new WorkHour { StartHour = "08:00", EndHour = "16:00" },
            new WorkHour { StartHour = "09:00", EndHour = "17:00" },
            new WorkHour { StartHour = "afwezig", EndHour = "afwezig" },
            new WorkHour { StartHour = "afwezig", EndHour = "afwezig" }
        }, // Doctor 1

        // Define work hours for other doctors
    };

    public class WorkHour
    {
        public string StartHour { get; set; }
        public string EndHour { get; set; }

        public override string ToString()
        {
            if (StartHour == "afwezig" || EndHour == "afwezig")
            {
                return "Afwezig";
            }
            return $"{StartHour} - {EndHour} (Aanwezig)";
        }
    }

    private WorkHour GetWorkHoursForDay(int doctorIndex, string day)
    {
        if (doctorIndex >= 0 && doctorIndex < workHours.Count)
        {
            int dayIndex = predefinedDays.IndexOf(day);
            if (dayIndex >= 0 && dayIndex < workHours[doctorIndex].Count)
            {
                return workHours[doctorIndex][dayIndex];
            }
        }
        return new WorkHour { StartHour = "Werkuren niet beschikbaar", EndHour = "" };
    }

    private void MakeAppointment()
    {
        // Implement the logic to make an appointment with this doctor
        // You can show a modal or navigate to another appointment page
        // and pass the doctor's information to that page if needed.
    }
}
