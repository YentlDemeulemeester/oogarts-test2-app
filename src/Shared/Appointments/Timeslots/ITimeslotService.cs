namespace Oogarts.Shared.Appointments.Timeslots;

public interface ITimeslotService
{
	Task<TimeslotResult.Index> GetIndexAsync(TimeslotRequest.Index request);
	Task<TimeslotResult.Index> GetTimeslotsFromDoctorAsync(TimeslotRequest.Index request, long doctorId);
}
