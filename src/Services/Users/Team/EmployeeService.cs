using Bogus;
using Domain.Users.Employees;
using Domain.Users.Employees.Availabilities;
using Microsoft.EntityFrameworkCore;
using Domain.Users.Doctors;
using Domain.Users.Employees;
using Domain.Users.Patients;
using Persistence;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Biographies;
using Shared.Users.Teams.Groups;

namespace Services.Users.Team;

public class EmployeeService : IEmployeeService
{
	private readonly ApplicationDbContext dbContext;

	public EmployeeService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<EmployeeResult.Index> GetIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Image = x.Image,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}
	
	public async Task<EmployeeResult.Index> GetDoctorsIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}

	public async Task<EmployeeResult.Index> GetAssistantsIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.OfType<Assistant>().AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}

	public async Task<EmployeeResult.Index> GetSecretaryIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.OfType<Secretary>().AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}
    public async Task<EmployeeDto.Detail> GetDetailAsync(long employeeId)
    {
        // Retrieve the employee and their current week availabilities
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(6);

        // Include the Group property when querying the employee
        var employee = await dbContext.Employees
            .Include(x => x.Availabilities)
            .Include(x => x.Group) // Include the Group here
            .SingleOrDefaultAsync(x => x.Id == employeeId);

        if (employee is null)
        {
            throw new EntityNotFoundException(nameof(employee), employeeId);
        }

        // If there are no availabilities for the current week, create them
        if (!employee.Availabilities.Any(a => a.StartDate >= startOfWeek && a.StartDate <= endOfWeek))
        {
            // Create availabilities for each day of the current week
            // (Make sure the Availability class has a constructor that accepts these parameters)
            for (int i = 0; i < 7; i++)
            {
                var date = startOfWeek.AddDays(i);
                var availability = new Availability(new DateTime(date.Year, date.Month, date.Day, 9, 0, 0),
                                                    new DateTime(date.Year, date.Month, date.Day, 17, 0, 0),
                                                    employeeId);
                dbContext.Availabilities.Add(availability);
            }
            await dbContext.SaveChangesAsync();

            // Re-query or re-include the Availabilities after adding them to ensure they are in the context
            await dbContext.Entry(employee).Collection(e => e.Availabilities).LoadAsync();
        }

        // Prepare the detail with availabilities
        var employeeDetail = new EmployeeDto.Detail
        {
            Id = employee.Id,
            Firstname = employee.FirstName,
            Lastname = employee.LastName,
            Birthdate = employee.Birthdate,
            Image = employee.Image,
            Email = employee.Email,
            Phonenumber = employee.PhoneNumber,
            Availabilities = employee.Availabilities
                .Where(a => a.StartDate >= startOfWeek && a.StartDate <= endOfWeek)
                .Select(a => new AvailabilityDto.Index
                {
                    Id = a.Id,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                }).ToList(),
            Group = employee.Group != null ? new GroupDto.Index
            {
                Id = employee.Group.Id,
                Name = employee.Group.Name,
                Sequence = employee.Group.Sequence,
            } : null,
        };

        return employeeDetail;
    }



    public async Task EditAsync(long employeeId, EmployeeDto.Mutate model)
	{
		Employee? employee = await dbContext.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);
		
		if(employee is null)
		{
			throw new EntityNotFoundException(nameof(Employee), employeeId);
		}

		Group? group;
		if (model.Group is not null)
		{
			group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == model.Group.Id);
		} else
		{
			throw new ApplicationException("There is no group associated with the changed employee");
		}

		if (group is null)
		{
			throw new EntityNotFoundException(nameof(Group), model.Group.Id);
		}

		employee.FirstName = model.Firstname!;
		employee.LastName = model.Lastname!;
		employee.Birthdate = (DateTime)model.Birthdate!;
		employee.Email = model.Email!;
		employee.PhoneNumber = model.Phonenumber!;
		employee.Group = group;

		await dbContext.SaveChangesAsync();
	}

	public Task<long> CreateAsync(EmployeeDto.Mutate model)
	{
		throw new NotImplementedException();
	}

	public async Task ChangeGroupAsync(long employeeId, long groupId)
	{
		Group? group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == groupId);
		if (group is null)
		{
			throw new EntityNotFoundException(nameof(Group), groupId);
		}

		Employee? employee = await dbContext.Employees.Include(x => x.Group).SingleOrDefaultAsync(x => x.Id == employeeId);
		if (employee is null)
		{
			throw new EntityNotFoundException(nameof(Employee), employeeId);
		}

		if (employee.Group.Id != groupId)
		{
			employee.Group = group;
			await dbContext.SaveChangesAsync();
		}
	}
}
