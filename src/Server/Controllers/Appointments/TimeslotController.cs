using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oogarts.Shared.Appointments.Timeslots;
using Swashbuckle.AspNetCore.Annotations;

namespace Oogarts.Server.Controllers.Appointments;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TimeslotController : ControllerBase
{
	private readonly ITimeslotService timeslotService;
	
	public TimeslotController(ITimeslotService timeslotService)
	{
		this.timeslotService = timeslotService;
	}

	[SwaggerOperation("Returns a list of timeslots.")]
	[HttpGet, AllowAnonymous]
	public async Task<TimeslotResult.Index> GetIndex([FromQuery] TimeslotRequest.Index request)
	{
		return await timeslotService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of timeslots for a specific doctor.")]
	[HttpGet("{doctorId}"), AllowAnonymous]
	public async Task<TimeslotResult.Index> GetTimeslotsFromDoctor([FromQuery] TimeslotRequest.Index request, long doctorId)
	{
		return await timeslotService.GetTimeslotsFromDoctorAsync(request, doctorId);
	}
}
