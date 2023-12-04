using Microsoft.EntityFrameworkCore;
using Oogarts.Domain.Users.Doctors;
using Oogarts.Domain.Users.Employees;
using Oogarts.Domain.Users.Patients;
using Oogarts.Persistence;
using Oogarts.Shared.Users.Doctors.Availabilities;
using Oogarts.Shared.Users.Doctors.Employees;

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
			Email = x.Email,
			Phonenumber = x.PhoneNumber,
			Availabilities = x.Availabilities.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				Day = x.Day,
				Start = x.Start,
				End = x.End,
			})
		}).SingleOrDefaultAsync(x => x.Id == employeeId);

		if (employee == null)
		{
			throw new EntityNotFoundException(nameof(employee), employeeId);
		}

		return employee;
	}
}
