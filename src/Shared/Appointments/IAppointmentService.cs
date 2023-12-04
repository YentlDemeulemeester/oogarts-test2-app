namespace Oogarts.Shared.Appointments;
public interface IAppointmentService
{
	Task<AppointmentResult.Index> GetIndexAsync(AppointmentRequest.Index request);
	Task<AppointmentResult.Index> GetAppointmentsFromDoctor(AppointmentRequest.Index request, long doctorId);
	Task<AppointmentDto.Detail> GetDetailAsync(long appointmentId);
    //Task<AppointmentResult.Create> CreateAsync(AppointmentDto.Mutate model);
    //Task EditAsync(long appointmentId, AppointmentDto.Mutate model);
    //Task DeleteAsync(long appointmentId);
}