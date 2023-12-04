using Microsoft.Extensions.DependencyInjection;
using Oogarts.Services.Appointments;
using Oogarts.Services.EyeConditions;
using Oogarts.Services.Users.Doctors;
using Oogarts.Services.Users.Team;
using Oogarts.Shared.Appointments;
using Oogarts.Shared.Appointments.Timeslots;
using Oogarts.Shared.EyeConditions;
using Oogarts.Shared.Users.Doctors.Availabilities;
using Oogarts.Shared.Users.Doctors.Employees;
using Oogarts.Shared.Users.Doctors.Specializations;
using Oogarts.Shared.Users.Patients;
using Shared.Articles;
using Services.Articles;
using Services.Files;
using Oogarts.Shared.Users.Team.Doctors;
using Services.EyeConditions;
using Services.Users.Patients;

namespace Oogarts.Services;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds all services to the DI container.
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IEyeConditionService, EyeConditionService>();
		services.AddScoped<ISymptomService, SymptomService>();
		services.AddScoped<ITimeslotService, TimeslotService>();
		services.AddScoped<IAppointmentService, AppointmentService>();
		services.AddScoped<IAvailabilityService, AvailabilityService>();
		services.AddScoped<IPatientService, PatientService>();
		services.AddScoped<ISpecializationService, SpecializationService>();
		services.AddScoped<IEmployeeService, EmployeeService>();
		services.AddScoped<IDoctorService, DoctorService>();		
		
		services.AddScoped<IStorageService,BlobStorageService>();
        services.AddScoped<IArticleService, ArticleService>();
		return services;
	}
}