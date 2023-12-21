﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Appointments.Timeslots;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Appointments;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Administrator")]
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
