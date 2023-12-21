using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Domain.Appointments;
using Domain.Users.Doctors;
using Persistence;
using Shared.Appointments;
using Shared.Appointments.Timeslots;
using Shared.Users.Patients;

namespace Services.Appointments;
public class AppointmentService : IAppointmentService
{

	private readonly ApplicationDbContext dbContext;

	public AppointmentService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<AppointmentResult.Index> GetIndexAsync(AppointmentRequest.Index request)
	{
		var parsedDate = request.Date != null ? DateOnly.Parse(request.Date) : (DateOnly?)null;
		var query = dbContext.Appointments.AsQueryable();

		var items = await query
			.Where(x => parsedDate == null || parsedDate == DateOnly.FromDateTime(x.Timeslot.Time))  //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AppointmentDto.Index
			{
				Id = x.Id,
				Timeslot = new TimeslotDto.Index
				{
					Id = x.Timeslot.Id,
					Date = DateOnly.FromDateTime(x.Timeslot.Time),
					Time = TimeOnly.FromDateTime(x.Timeslot.Time),
					Duration = x.Timeslot.Duration,
				},
			})
			.ToListAsync();

		var result = new AppointmentResult.Index
		{
			Appointments = items,
			TotalAmount = items.Count,
		};

		return result;
	}

	public async Task<AppointmentResult.Index> GetAppointmentsFromDoctor(AppointmentRequest.Index request, long doctorId)
	{
		var parsedDate = request.Date != null ? DateOnly.Parse(request.Date) : (DateOnly?)null;
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();

		var items = await query
			.Where(x => x.Id == doctorId)
			.SelectMany(x => x.Appointments)
			.Where(x => parsedDate == null || parsedDate == DateOnly.FromDateTime(x.Timeslot.Time))  //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AppointmentDto.Index
			{
				Id = x.Id,
				Timeslot = new TimeslotDto.Index
				{
					Id = x.Timeslot.Id,
					Date = DateOnly.FromDateTime(x.Timeslot.Time),
					Time = TimeOnly.FromDateTime(x.Timeslot.Time),
					Duration = x.Timeslot.Duration,
				},
			})
			.ToListAsync();

		var result = new AppointmentResult.Index
		{
			Appointments = items,
			TotalAmount = items.Count,
		};

		return result;
	}
	
	public async Task<AppointmentDto.Detail> GetDetailAsync(long appointmentId)
	{
		AppointmentDto.Detail? appointment = await dbContext.Appointments
			.Select(x => new AppointmentDto.Detail
			{
				Id = x.Id,
				Patient = new PatientDto.Detail
				{
					Id = x.Patient.Id,
					FirstName = x.Patient.FirstName,
					LastName = x.Patient.LastName,
					BirthDate = x.Patient.BirthDate,
					Email = x.Patient.Email,
					PhoneNumber = x.Patient.PhoneNumber,
				},
				Timeslot = new TimeslotDto.Index
				{
					Id = x.Timeslot.Id,
					Date = DateOnly.FromDateTime(x.Timeslot.Time),
					Time = TimeOnly.FromDateTime(x.Timeslot.Time),
					Duration = x.Timeslot.Duration,
				},
				Reason = x.Reason,
				Note = x.Note,
			}).SingleOrDefaultAsync(x => x.Id == appointmentId);

		if (appointment == null)
		{
			throw new EntityNotFoundException(nameof(Appointment), appointmentId);
		}

		return appointment;
	}

	//public async Task<AppointmentResult.Index> GetIndexAsync(AppointmentRequest.Index request)
	//{
	//	var searchTerm = request.Searchterm != null ? request.Searchterm.ToLowerInvariant() : null;

	//	var query = dbContext.Appointments.AsQueryable();

	//	//if (!string.IsNullOrWhiteSpace(searchTerm))
	//	//{
	//	//	query = query.Where(x => x.Name.ToLower().Contains(searchTerm));
	//	//}

	//	int totalAmount = await query.CountAsync();

	//	var items = await query
	//		.Skip((request.Page - 1) * request.PageSize)
	//		.Take(request.PageSize)
	//		.OrderBy(x => x.Id)
	//		.Select(x => new AppointmentDto.Index
	//		{
	//			Id = x.Id,
	//			Date = x.Date,
	//			Duration = x.Duration,
	//		})
	//		.ToListAsync();

	//	var result = new AppointmentResult.Index
	//	{
	//		Appointments = items,
	//		TotalAmount = totalAmount
	//	};

	//	return result;
	//}

	//public async Task<AppointmentDto.Detail> GetDetailAsync(long appointmentId)
	//{
	//	AppointmentDto.Detail? appointment = await dbContext.Appointments
	//		.Select(x => new AppointmentDto.Detail
	//		{
	//			Id = x.Id,
	//			Date = x.Date,
	//			Duration = x.Duration,
	//			Patient = new PatientDto.Detail
	//			{
	//				Id = x.Patient.Id,
	//				FirstName = x.Patient.FirstName,
	//				LastName = x.Patient.LastName,
	//				BirthDate = x.Patient.BirthDate,
	//				Email = x.Patient.Email,
	//				PhoneNumber = x.Patient.PhoneNumber
	//			},
	//			Reason = x.Reason,
	//		}).SingleOrDefaultAsync(x => x.Id == appointmentId);

	//	if (appointment == null)
	//	{
	//		throw new EntityNotFoundException(nameof(Appointment), appointmentId);
	//	}

	//	return appointment;
	//}

}