using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Appointment;

public partial class Index
{
    [Parameter] public DateTime DateValue { get; set; } = DateTime.Now;
    [Parameter] public int ActiveCount { get; set; } = 1;

    [Parameter] public string? SelectValue { get; set; }
    [Parameter] public int SelectCount { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    IEnumerable<string>? filteredDoctors;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        filteredDoctors = new List<string>
        {
            "Dr. Smith",
            "Dr. Johnson",
            "Dr. Jonas",
            "Dr. Larsen"
        };

    }
    void DateRenderSpecial(DateRenderEventArgs args)
    {
        if (args.Date < DateTime.Today)
        {
            args.Disabled = true;
            args.Attributes.Add("style", "color: #d3d3d3;");
        }
    }
    
    void OnChange(DateTime? value)
    {
        Console.WriteLine($"Value changed to {value}");
    }

    void LoadData(LoadDataArgs args)
    {
        var query = filteredDoctors.AsQueryable(); // Use the static list of doctors

        if (!string.IsNullOrEmpty(args.Filter))
        {
            query = query.Where(d => d.ToLower().Contains(args.Filter.ToLower()));
        }

        SelectCount = query.Count();

        filteredDoctors = query.Skip(args.Skip.HasValue ? args.Skip.Value : 0).Take(args.Top.HasValue ? args.Top.Value : 10).ToList();

        InvokeAsync(StateHasChanged);
    }

    private void ClearSearch()
    {
        SelectValue = null; // Set the search value to null or an empty string as needed
        LoadData(new LoadDataArgs()); // Reload the data to reset the filtering
    }

    public void Reset()
    {
        NavigationManager.NavigateTo($"Afspraak", true);
    }
    public void Next()
    {
        ActiveCount++;
    }


    static List<DateTime> GenerateDateTimeList(int numberOfDates)
    {
        List<DateTime> dateTimes = new List<DateTime>();
        Random random = new Random();

        for (int i = 0; i < numberOfDates; i++)
        {
            // Use DateTime.Today for each date
            DateTime currentDate = DateTime.Today;

            // Generate random hours and minutes with 15-minute intervals
            int randomHour = random.Next(0, 24);
            int randomMinute = random.Next(0, 4) * 15;

            dateTimes.Add(new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, randomHour, randomMinute, 0));
        }

        // Order the list of DateTimes in ascending order
        dateTimes = dateTimes.OrderBy(dt => dt).ToList();

        return dateTimes;
    }

    List<DateTime> dateTimes = GenerateDateTimeList(20);



}
