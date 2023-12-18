using Microsoft.AspNetCore.Components;
using Oogarts.Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Biographies;

namespace Client.Team;

public partial class DetailEmployee
{
    [Parameter] public long Id { get; set; }
    [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
    [Inject] public IBioService BioService { get; set; } = default!;
	[Inject] public NavigationManager NavigationManager { get; set; } = default!;

    
    private bool admin = true;
    private EmployeeDto.Detail? employee;
    private BioDto.Index? bio;
    private bool IsExpanded = false;

    private string ContentClass => IsExpanded ? "accordion-content expanded" : "accordion-content";

    private string ChevronImage => IsExpanded ? "./images/chevron-up.svg" : "./images/chevron-down.svg";

    private void ToggleAccordion()
    {
        IsExpanded = !IsExpanded;
    }


    protected override async Task OnParametersSetAsync()
	{
        await GetEmployeeAsync();
        await GetBioAsync();
	}

    private async Task GetEmployeeAsync()
    {
        employee = await EmployeeService.GetDetailAsync(Id);
        Console.WriteLine(employee.Availabilities is null);
    }

    private async Task GetBioAsync()
    {
        try
        {
            bio = await BioService.GetDetailAsync(Id);
        } catch (Exception ex)
        {
            bio = null;
        }
        
    }

    private void NavigateToEditEmployee()
    {
        if (admin)
        {
            NavigationManager.NavigateTo($"Team/Medewerker/Edit/{Id}");
        }
    }

    // private TeamMember member;

    private readonly List<string> images = new List<string> {
        "/images/doctor1.jpg", "/images/doctor2.jpg", "/images/doctor3.jpg", "/images/doctor4.jpg", "/images/doctor5.jpg", "/images/doctor6.png", "/images/doctor7.png" };
    private readonly List<string> function = new List<string> {
        "Oogarts (Algemeen)", "Cornea-specialist", "Cornea-specialist", "Orthoptist", "Orthoptist", "Orthoptist", "Cornea-specialist" };
    private readonly List<string> name = new List<string> {
        "Dr. Emily Patel", "Dr. Sarah Mitchell", "Dr. Laura Ramirez", "Dr. James Anderson", "Dr. Ramin Doker", "Dr. David Wilson", "Michael Chang" };


    private void GoBack()
    {
        NavigationManager.NavigateTo($"Team");
    }
}
