namespace Shared.Users.Doctors.Employees;

public abstract class EmployeeResult
{
	public class Index
	{
		public IEnumerable<EmployeeDto.Index>? Employees { get; set; }
		public int TotalAmount { get; set; }
	}
}
