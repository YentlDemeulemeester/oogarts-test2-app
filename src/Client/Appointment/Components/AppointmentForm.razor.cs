using Microsoft.AspNetCore.Components;
using Shared.Appointments;

namespace Client.Appointment.Components {
	public partial class AppointmentForm {
		/*private bool formSubmitted = false;*/
		private AppointmentDto.Create Appointment = new();

		private Boolean showDate = false;
		private Boolean showTime = false;

		private float startHour = 0;
		private float endHour = 0;

		private void CreateAppointment() {
            /*Console.WriteLine($"Appointment created for {Appointment.} {Patient.LastName}, Email: {Patient.Email}, BirthDate: {Patient.BirthDate}");*/
            Console.WriteLine("this works");
			/*formSubmitted = true;*/
		}

		private void HandleDoctorSelection(ChangeEventArgs args)
		{

            string selectedDoctor = args.Value.ToString();

			if (selectedDoctor == "drSmith") {
				
				startHour = 8; endHour = 16;



			} else if (selectedDoctor == "drJohnson") {

                startHour = 10; endHour = 18;

            }

			if (selectedDoctor != "")
			{
               showDate = true;
            }
            
        }

        private void HandleDateSelection()
        {
            showTime = true;
            // Handle the selected time (you can use it in Appointment object or perform other actions)
            /*Console.WriteLine($"Selected time: {selectedHour}");*/
        }

        private void HandleTimeSelection(float selectedHour)
        {
            // Handle the selected time (you can use it in Appointment object or perform other actions)
            Console.WriteLine($"Selected time: {selectedHour}");
        }

    }
}