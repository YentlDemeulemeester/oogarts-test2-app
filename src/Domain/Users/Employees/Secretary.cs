using Oogarts.Domain.Users.Doctors;

namespace Oogarts.Domain.Users.Employees;

public class Secretary : Employee
{
	//Database constructor
	private Secretary() { }

	public Secretary(string firstname, string lastname, DateOnly birthdate, string email, string phonenumber)
	{
		FirstName = firstname;
		LastName = lastname;
		Birthdate = birthdate;
		Email = email;
		PhoneNumber = phonenumber;
	}
}
