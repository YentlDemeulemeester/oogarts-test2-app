using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Oogarts.Persistence;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Oogarts.Server.Middleware;
using Microsoft.AspNetCore.Authentication;
using Oogarts.Server.Authentication;
using Oogarts.Services;
using Oogarts.Shared.Users.Patients;
using Oogarts.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddTransient <IEmailSender, EmailSender>();

// Fluentvalidation
builder.Services.AddValidatorsFromAssemblyContaining<PatientDto.Mutate.Validator>();//??? For each DTO??
builder.Services.AddFluentValidationAutoValidation();

// Swagger | OAS 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	// Since we subclass our dto's we need a more unique id.
	options.CustomSchemaIds(type => type.DeclaringType is null ? $"{type.Name}" : $"{type.DeclaringType?.Name}.{type.Name}");
	options.EnableAnnotations();
}).AddFluentValidationRulesToSwagger();

// Database
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(
		builder.Configuration.GetConnectionString("MySql"),
		serverVersion)
	// The following three options help with debugging, but should
	// be changed or removed for production.
	.LogTo(Console.WriteLine, LogLevel.Information)
	.EnableSensitiveDataLogging()
	.EnableDetailedErrors()
); ;


// (Fake) Authentication
builder.Services.AddAuthentication("Fake Authentication")
				.AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("Fake Authentication", null);


builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext.User);

builder.Services.AddControllersWithViews()
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
			options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
		});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseWebAssemblyDebugging();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers().RequireAuthorization();
app.MapFallbackToFile("index.html");


using (var scope = app.Services.CreateScope())
{ // Require a DbContext from the service provider and seed the database.
	var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	FakeSeeder seeder = new(dbContext);
	seeder.Seed();
}

app.Run();