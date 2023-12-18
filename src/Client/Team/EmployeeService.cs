using Client.Classes;
using Oogarts.Client.Extensions;
using Oogarts.Client.Files;
using Oogarts.Shared.Users.Doctors.Employees;
using System.Net.Http.Json;

namespace Oogarts.Client.Team;

public class EmployeeService : IEmployeeService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authorizedClient;
    private const string endpoint = "api/Employee";

	public EmployeeService(HttpClient authorizedClient, PublicClient publicClient)
	{
		this.authorizedClient = authorizedClient;
		this.publicClient = publicClient;
	}
	
	public async Task<EmployeeResult.Index> GetIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}?{request.AsQueryString()}");
		return response;
	}
	
	public async Task<EmployeeResult.Index> GetDoctorsIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}/Doctors?{request.AsQueryString()}");
		return response;
	}
	public async Task<EmployeeResult.Index> GetAssistantsIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}/Assistants?{request.AsQueryString()}");
		return response;
	}
	
	public async Task<EmployeeResult.Index> GetSecretaryIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}/Secretary?{request.AsQueryString()}");
		return response;
	}

	public async Task<EmployeeDto.Detail> GetDetailAsync(long employeeId)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeDto.Detail>($"{endpoint}/{employeeId}");
		return response;
	}

	public Task EditAsync(long employeeId, EmployeeDto.Mutate model)
	{
		throw new NotImplementedException();
	}

	public Task<long> CreateAsync(EmployeeDto.Mutate model)
	{
		throw new NotImplementedException();
	}

	public async Task ChangeGroupAsync(long employeeId, long groupId)
	{
		var response = await authorizedClient.PutAsync($"{endpoint}/{employeeId}/{groupId}", null);
	}
}