using Microsoft.AspNetCore.Components;

namespace Client.Appointment.Components;

public partial class TimeSlot
{
    [Parameter] public DateTime Slot { get; set; }
}
