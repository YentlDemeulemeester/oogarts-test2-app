using Bogus;
using Domain.Users.Employees;
using Microsoft.EntityFrameworkCore;
using Oogarts.Domain.Users.Doctors;
using Oogarts.Domain.Users.Employees;
using Oogarts.Domain.Users.Patients;
using Oogarts.Persistence;
using Oogarts.Shared.Users.Doctors.Availabilities;
using Oogarts.Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Biographies;
using Shared.Users.Teams.Groups;

namespace Oogarts.Services.Users.Team;

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
		EmployeeDto.Detail? employee = await dbContext.Employees.Select(x => new EmployeeDto.Detail
		{
			Id = x.Id,
			Firstname = x.FirstName,
			Lastname = x.LastName,
			Birthdate = x.Birthdate,
			Image = x.Image,
			Email = x.Email,
			Phonenumber = x.PhoneNumber,
			Availabilities = x.Availabilities.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
			}),
			Group = new GroupDto.Index
			{
				Id = x.Group.Id,
				Name = x.Group.Name,
				Sequence = x.Group.Sequence,
			},
		}).SingleOrDefaultAsync(x => x.Id == employeeId);

		if (employee == null)
		{
			throw new EntityNotFoundException(nameof(employee), employeeId);
		}

		return employee;
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
