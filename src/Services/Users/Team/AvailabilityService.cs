using Microsoft.EntityFrameworkCore;
using Oogarts.Domain.Users.Doctors;
using Oogarts.Persistence;
using Oogarts.Shared.Users.Doctors.Availabilities;

namespace Oogarts.Services.Users.Doctors;
public class AvailabilityService : IAvailabilityService
{
	private readonly ApplicationDbContext dbContext;

	public AvailabilityService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<AvailabilityResult.Index> GetIndexAsync(AvailabilityRequest.Index request)
	{
		var parsedDate = request.Date != null ? DateTime.Parse(request.Date) : (DateTime?)null;
		var query = dbContext.Availabilities.AsQueryable();

		var items = await query
			.Where(x => parsedDate == null || parsedDate == x.StartDate)  //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
			})
			.ToListAsync();

		var result = new AvailabilityResult.Index
		{
			Availabilities = items,
			TotalAmount = items.Count,
		};

		return result;
	}

	public async Task<AvailabilityResult.Index> GetAvailibilitiesFromDoctorAsync(AvailabilityRequest.Index request, long doctorId)
	{
		var parsedDate = request.Date != null ? DateTime.Parse(request.Date) : (DateTime?)null;
		var query = dbContext.Availabilities.AsQueryable();

		var items = await query
			.Where(x => parsedDate == null || parsedDate == x.StartDate)  //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
			})
			.ToListAsync();

		var result = new AvailabilityResult.Index
		{
			Availabilities = items,
		};

		return result;
	}

	public async Task<AvailabilityResult.Index> GetAvailibilitiesFromEmployeeAsync(AvailabilityRequest.Index request, long employeeId)
	{
		var parsedDate = request.Date != null ? DateTime.Parse(request.Date) : (DateTime?)null;
		var query = dbContext.Availabilities.AsQueryable();

		var items = await query
			.Where(x => parsedDate == null || parsedDate == x.StartDate)  //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
			})
			.ToListAsync();

		var result = new AvailabilityResult.Index
		{
			Availabilities = items,
			TotalAmount = items.Count,
		};

		return result;
	}
}
