using Domain.Users.Employees.Availabilities;
using Microsoft.EntityFrameworkCore;
using Oogarts.Domain.Appointments;
using Oogarts.Domain.Articles;
using Oogarts.Domain.Articles.Fragments;
using Oogarts.Domain.EyeConditions;
using Oogarts.Domain.Users.Doctors;
using Oogarts.Domain.Users.Patients;
using Oogarts.Persistence.Triggers;
using System.Reflection;

namespace Oogarts.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
	public DbSet<Availability> Availabilities => Set<Availability>();
    public DbSet<Timeslot> Timeslots => Set<Timeslot>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<EyeCondition> EyeConditions => Set<EyeCondition>();   
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Fragment> Fragments => Set<Fragment>();
    public DbSet<Symptom> Symptoms => Set<Symptom>();   

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}