using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;
using Shared.Users;
using Shared.Users.Doctors.Employees;
using System.Collections.Immutable;
using System.Security.Claims;

namespace Client.Admin.Components.Team
{
    public partial class TeamGrid
    {
        RadzenDataGrid<EmployeeDto.Index> employeeGrid;
        List<EmployeeDto.Index>? employees;
        IEnumerable<UserDto.Index> users;
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] public TeamService TeamService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            EmployeeResult.Index res = await EmployeeService.GetIndexAsync(new EmployeeRequest.Index());
            users = await TeamService.GetUsers();
            employees = res.Employees.ToList();
        }

        public async Task ChangeRole(string email)
        {
            var userId = users.FirstOrDefault(x => x.Email == email).UserId;
            await TeamService.ChangeRole(userId);
            await employeeGrid.Reload();
        }

        public async Task MakeUser(string email)
        {
            UserDto.Mutate newUser = new UserDto.Mutate()
            {
                Email = email,
                Password = "Password123456"
            };
            await TeamService.CreateUser(newUser);
            await employeeGrid.Reload();
        }

        public bool UserExists(string email)
        {
            return users.Any(u => u.Email == email);
        }

        public string GetRole(string email)
        {
            return users.Where(x => x.Email == email).FirstOrDefault().Role;
        }
    }
}
