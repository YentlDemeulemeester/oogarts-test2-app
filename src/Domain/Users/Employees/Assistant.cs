using Oogarts.Domain.Users.Doctors;

namespace Oogarts.Domain.Users.Employees;

public class Assistant : Employee
{
	//Database constructor
	private Assistant() { }
	public Assistant(string firstname, string lastname, DateOnly birthdate, string email, string phonenumber) { 
		FirstName = firstname;
		LastName = lastname;
		Birthdate = birthdate;
		Email = email;
		PhoneNumber = phonenumber;
	}
}
