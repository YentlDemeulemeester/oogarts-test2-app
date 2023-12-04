using Microsoft.AspNetCore.Components;
//using Oogarts.Shared.Users.Doctors.TeamMembers;

namespace Client.Team;

public partial class Index
{
	//[Inject] public ITeamMemberService TeamMemberService { get; set; } = default!;
	[Inject] public NavigationManager NavigationManager { get; set; } = default!;

	//private IEnumerable<TeamMemberDto.Index>? teamMembers;

	[Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
	[Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
	[Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }

	protected override async Task OnParametersSetAsync()
	{
		//TeamMemberRequest.Index request = new()
		//{
		//	//Searchterm = Searchterm,
		//	//Page = Page,
		//	//PageSize = PageSize,
		//};

		//var response = await TeamMemberService.GetIndexAsync(request);
		//teamMembers = response.TeamMembers.ToList();
	}


	//Te veranderen met echte data
	private readonly List<string> Images = new List<string> {
        "../../images/doctor1.jpg", "../../images/doctor2.jpg", "../../images/doctor3.jpg", "../../images/doctor4.jpg", "../../images/doctor5.jpg", "../../images/doctor6.png", "../../images/doctor7.png" };
	private readonly List<string> Function = new List<string> {
		"Oogarts (Algemeen)", "Cornea-specialist", "Cornea-specialist", "Orthoptist", "Orthoptist", "Orthoptist", "Cornea-specialist" };
	private readonly List<string> Name = new List<string> {
		"Dr. Emily Patel", "Dr. Sarah Mitchell", "Dr. Laura Ramirez", "Dr. James Anderson", "Dr. Ramin Doker", "Dr. David Wilson", "Michael Chang" };
}
