namespace Oogarts.Shared.Users.Doctors.Employees;

public interface IEmployeeService
{
	Task<EmployeeResult.Index> GetIndexAsync(EmployeeRequest.Index request);
	Task<EmployeeResult.Index> GetDoctorsIndexAsync(EmployeeRequest.Index request);
	Task<EmployeeResult.Index> GetAssistantsIndexAsync(EmployeeRequest.Index request);
	Task<EmployeeResult.Index> GetSecretaryIndexAsync(EmployeeRequest.Index request);
	Task<EmployeeDto.Detail> GetDetailAsync(long employeeId);
	Task<long> CreateAsync(EmployeeDto.Mutate model);
	Task EditAsync(long employeeId, EmployeeDto.Mutate model);
	Task ChangeGroupAsync(long employeeId, long groupId);
}
