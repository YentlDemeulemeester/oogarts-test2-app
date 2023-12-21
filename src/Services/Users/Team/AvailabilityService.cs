using Domain.Users.Employees.Availabilities;
using Microsoft.EntityFrameworkCore;

using Persistence;
using Shared.Users.Doctors.Availabilities;

namespace Services.Users.Doctors;
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
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(6);

        var query = dbContext.Availabilities.Where(x => x.EmployeeId == employeeId && x.StartDate >= startOfWeek && x.StartDate <= endOfWeek);

        if (!await query.AnyAsync())
        {
            // If no availabilities exist for the current week, create them
            for (int i = 0; i < 7; i++)
            {
                var date = startOfWeek.AddDays(i);
                var newAvailability = new AvailabilityDto.Mutate
                {
                    StartDate = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0),
                    EndDate = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0),
                    EmployeeId = employeeId  // Make sure the availability is linked to the employee
                };
                await CreateAsync(newAvailability);
            }
        }

        var items = await query
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

    public async Task<AvailabilityResult.Create> CreateAsync(AvailabilityDto.Mutate model)
    {
        Availability availability = new Availability(model.StartDate, model.EndDate, model.EmployeeId);  // Assuming constructor takes employeeId as well
        dbContext.Add(availability);
        await dbContext.SaveChangesAsync();

        AvailabilityResult.Create result = new()
        {
            Id = availability.Id,
            StartDate = availability.StartDate,
            EndDate = availability.EndDate,
        };

        return result;
    }

}
