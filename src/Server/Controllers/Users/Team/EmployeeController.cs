using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oogarts.Shared.Users.Doctors.Employees;
using Swashbuckle.AspNetCore.Annotations;

namespace Oogarts.Server.Controllers.Users.Team;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EmployeeController : ControllerBase
{
	private readonly IEmployeeService employeeService;

	public EmployeeController(IEmployeeService employeeService)
	{
		this.employeeService = employeeService;
	}

	[SwaggerOperation("Returns a list of employees.")]
	[HttpGet, AllowAnonymous]
	public async Task<EmployeeResult.Index> GetIndex([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of doctors.")]
	[HttpGet("Doctors"), AllowAnonymous]
	public async Task<EmployeeResult.Index> GetDoctors([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetDoctorsIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of assistants.")]
	[HttpGet("Assistants"), AllowAnonymous]
	public async Task<EmployeeResult.Index> GetAssistants([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetAssistantsIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of secretary members.")]
	[HttpGet("Secretary"), AllowAnonymous]
	public async Task<EmployeeResult.Index> GetSecretary([FromQuery] EmployeeRequest.Index request)
	{
		return await employeeService.GetSecretaryIndexAsync(request);
	}

	[SwaggerOperation("Returns a specific employee.")]
	[HttpGet("{employeeId}"), AllowAnonymous]
	public async Task<EmployeeDto.Detail> GetDetail(long employeeId)
	{
		return await employeeService.GetDetailAsync(employeeId);
	}
}
