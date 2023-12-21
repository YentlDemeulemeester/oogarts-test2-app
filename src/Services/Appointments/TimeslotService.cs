using Microsoft.EntityFrameworkCore;
using Domain.Users.Doctors;
using Persistence;
using Shared.Appointments.Timeslots;

namespace Services.Appointments;

public class TimeslotService : ITimeslotService
{
	private readonly ApplicationDbContext dbContext;

	public TimeslotService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<TimeslotResult.Index> GetIndexAsync(TimeslotRequest.Index request)
	{
		var parsedDate = request.Date != null ? DateOnly.Parse(request.Date) : (DateOnly?)null;
		var query = dbContext.Timeslots.AsQueryable();

		var items = await query
			.Where(x => parsedDate == null || parsedDate == DateOnly.FromDateTime(x.Time))	//If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new TimeslotDto.Index
			{
				Id = x.Id,
				Date = DateOnly.FromDateTime(x.Time),
				Time = TimeOnly.FromDateTime(x.Time),
				Duration = x.Duration,
			})
			.ToListAsync();

		var result = new TimeslotResult.Index
		{
			Timeslots = items,
			TotalAmount = items.Count,
		};

		return result;
	}

	public async Task<TimeslotResult.Index> GetTimeslotsFromDoctorAsync(TimeslotRequest.Index request, long doctorId)
	{
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();
		var parsedDate = request.Date != null ? DateOnly.Parse(request.Date) : (DateOnly?)null;
		
		var items = await query
			.Where(x => x.Id == doctorId)
			.SelectMany(x => x.Timeslots)
			.Where(x => parsedDate == null || parsedDate == DateOnly.FromDateTime(x.Time))   //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new TimeslotDto.Index
			{
				Id = x.Id,
				Date = DateOnly.FromDateTime(x.Time),
				Time = TimeOnly.FromDateTime(x.Time),
				Duration = x.Duration,
			})
			.ToListAsync();

		var result = new TimeslotResult.Index
		{
			Timeslots = items,
			TotalAmount = items.Count,
		};

		return result;
	}
}
