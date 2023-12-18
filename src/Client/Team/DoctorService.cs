using Client.Classes;
using Oogarts.Client.Extensions;
using Oogarts.Client.Files;
using Oogarts.Shared.Users.Team.Doctors;
using System.Net.Http.Json;

namespace Oogarts.Client.Team;

public class DoctorService : IDoctorService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authorizedClient;
    private const string endpoint = "api/Doctor";

	public DoctorService(HttpClient authorizedClient, PublicClient publicClient)
	{
		this.authorizedClient = authorizedClient;
		this.publicClient = publicClient;
	}
	
	public async Task<DoctorResult.Index> GetIndexAsync(DoctorRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<DoctorResult.Index>($"{endpoint}?{request.AsQueryString()}");
		return response;
	}
	public async Task<DoctorDto.Detail> GetDetailAsync(long doctorId)
	{
		var response = await publicClient.Client.GetFromJsonAsync<DoctorDto.Detail>($"{endpoint}/{doctorId}", JsonExtensions.GetJsonSerializerOptions());
		return response;
	}
}
