using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Oogarts.Shared.Users.Patients;
using Radzen;
using System;

namespace Client.Appointment.Components {

	public partial class PatientForm {
		[Parameter] public Action? OnNextButtonClick { get; set; }
        [Parameter] public string? value { get; set; }
        [Parameter] public Variant variant { get; set; } = Variant.Outlined;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        private PatientDto.Mutate Patient = new();
		private PatientDto.Mutate.Validator validator = new PatientDto.Mutate.Validator();
		private async Task CreateAppointmentAsync() {
			var results = validator.Validate(Patient);

			if (!results.IsValid) {
				var errorMessages = string.Join("\n", results.Errors.Select(failure => $"{failure.ErrorMessage}"));

				await JSRuntime.InvokeVoidAsync("alert", errorMessages);
			} else {
				OnNextButtonClick?.Invoke();
				Console.WriteLine($"Afspraak gemaakt voor {Patient.FirstName} {Patient.LastName}, Email: {Patient.Email}, Geboortedatum: {Patient.BirthDate}");
			}
		}
        public void Reset()
        {
            NavigationManager.NavigateTo($"Afspraak", true);
        }
    }

}