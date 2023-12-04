//using Oogarts.Client.Extensions;
//using Oogarts.Shared.Users.Doctors.TeamMembers;
//using System;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;

//namespace Oogarts.Client.Team;

//public class TeamMemberService : ITeamMemberService
//{
//	private readonly HttpClient client;
//	private const string endpoint = "api/Team";
//	public TeamMemberService(HttpClient client)
//	{
//		this.client = client;
//	}
	
//	public async Task<TeamMemberResult.Index> GetIndexAsync(TeamMemberRequest.Index request)
//	{
//		var response = await client.GetFromJsonAsync<TeamMemberResult.Index>($"{endpoint}?{request.AsQueryString()}");
//        return response!;
//	}

//        public async Task<TeamMemberResult.Index> GetDetailAsync(long id)
//    {
//        var response = await client.GetFromJsonAsync<TeamMemberResult.GetDetail>($"{endpoint}/{id}");
//        return response;
//    }

//    public async Task<TeamMemberResult.Create> CreateAsync(TeamMemberDto.Mutate model)
//    {
//        var response = await client.PostAsJsonAsync(endpoint, model);
//        return await response.Content.ReadFromJsonAsync<TeamMemberResult.Create>();
//    }

//    	public async Task EditAsync(long id, TeamMemberDto.Mutate edit)
//    {
//	   var response = await client.PutAsJsonAsync($"{endpoint}/{id}", edit);
// 	   response.EnsureSuccessStatusCode();
//    }

//    public async Task DeleteAsync(long id)
//    {
//	    var response = await client.DeleteAsync($"{endpoint}/{id}");
//	    response.EnsureSuccessStatusCode();
//    }

//	Task<TeamMemberDto.Detail> ITeamMemberService.GetDetailAsync(long teamMemberId)
//	{
//		throw new NotImplementedException();
//	}
//	//public async Task<EyeConditionResult.Create> CreateAsync(EyeConditionDto.Mutate request)
//	//{
//	//	var response = await client.PostAsJsonAsync(endpoint, request);
//	//	return await response.Content.ReadFromJsonAsync<EyeConditionResult.Create>();
//	//}

//	//public async Task DeleteAsync(int eyeConditionId)
//	//{
//	//	await client.DeleteAsync($"{endpoint}/{eyeConditionId}");
//	//}

//	//public async Task<EyeConditionDto.Detail> GetDetailAsync(int eyeConditionId)
//	//{
//	//	var response = await client.GetFromJsonAsync<EyeConditionDto.Detail>($"{endpoint}/{eyeConditionId}");
//	//	return response;
//	//}



//	//public async Task EditAsync(int eyeConditionId, EyeConditionDto.Mutate model)
//	//{
//	//	var response = await client.PutAsJsonAsync($"{endpoint}/{eyeConditionId}", model);
//	//}
//}