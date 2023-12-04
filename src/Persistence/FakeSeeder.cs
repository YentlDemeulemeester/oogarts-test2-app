using Domain.Users.Employees.Availabilities;
using Oogarts.Domain.Appointments;
using Oogarts.Domain.Articles.Fragments;
using Oogarts.Domain.EyeConditions;
using Oogarts.Domain.Users.Doctors;
using Oogarts.Domain.Users.Employees;
using Oogarts.Domain.Users.Patients;
using Oogarts.Persistence;

public class FakeSeeder
{
	private readonly ApplicationDbContext dbContext;

	public FakeSeeder(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public void Seed()
	{
		// Not a good idea in production.
		dbContext.Database.EnsureDeleted();
		dbContext.Database.EnsureCreated();

		SeedEyeConditions();
		SeedTeamMembers();
		SeedAvailabilities();
		SeedSpecializations();
		SeedAppointments();
		SeedPatients();
		
	}

	private void SeedTeamMembers()
	{
		List<Employee> members = new()
		{
			new Doctor("doctor1", "lastname", new DateOnly(1990,4,4), "0495741468", "doctor1@gmail.com"),
			new Secretary("secretary1", "lastname", new DateOnly(1991, 12, 12), "0495741469", "secretary@gmail.com"),
			new Assistant("assistant1", "lastname", new DateOnly(2002, 4, 4), "0487412574", "assistant@gmail.com")
		};

		dbContext.Employees.AddRange(members);
		dbContext.SaveChanges();
	}

	private void SeedAvailabilities()
	{
		var member = dbContext.Employees.ToList()[0];

		var availability1 = new Availability(new DateOnly(2023, 11, 26), new TimeOnly(7, 0), new TimeOnly(13,0));
		var availability2 = new Availability(new DateOnly(2023, 11, 27), new TimeOnly(8, 0), new TimeOnly(13, 0));
		var availability3 = new Availability(new DateOnly(2023, 11, 28), new TimeOnly(8, 0), new TimeOnly(15, 30));
		var availability4 = new Availability(new DateOnly(2023, 11, 29), new TimeOnly(10, 0), new TimeOnly(17, 0));
		var availability5 = new Availability(new DateOnly(2023, 11, 30), new TimeOnly(13, 0), new TimeOnly(17, 0));
		var availability6 = new Availability(new DateOnly(2023, 12, 1), new TimeOnly(8, 0), new TimeOnly(13, 0));

		member.Availability(availability1);
		member.Availability(availability2);
		member.Availability(availability3);
		member.Availability(availability4);
		member.Availability(availability5);
		member.Availability(availability6);

		dbContext.Employees.Update(member);
		dbContext.SaveChanges();
	}

	private void SeedSpecializations()
	{
		List<Specialization> specializations = new()
		{
			new Specialization("Cataract"),
			new Specialization("Implantlenzen"),
			new Specialization("Netvliesaandoeningen"),
			new Specialization("Ooglidcorrecties"),
		};

		Doctor d = (Doctor)dbContext.Employees.ToList()[0];
		d.Specialization(specializations[0]);
		d.Specialization(specializations[1]);

		dbContext.Specializations.AddRange(specializations);
		dbContext.Employees.Update(d);
		dbContext.SaveChanges();
	}

	private void SeedEyeConditions()
	{
		//var conditions = new EyeConditionFaker().AsTransient().UseSeed(1337).Generate(100);
		List<EyeCondition> conditions = new()
		{
			new EyeCondition("Glaucoom", "Glaucoom is een oogaandoening met verhoogde oogdruk die het gezichtsvermogen kan beschadigen en leiden tot blindheid zonder behandeling.", "bod","https://oogziekenhuis.me/omc-amsterdam.nl/Glaucoom_files/shapeimage_10.png"),
			new EyeCondition("Cataract", "Cataract is troebelheid van de ooglens, wat wazig zicht veroorzaakt, maar het kan met succes worden behandeld met cataractchirurgie.", "bod","https://ogen-blik.expert/wp-content/uploads/2018/12/banner_ogen_blik_cataract_symptomen.jpg"),
			new EyeCondition("Maculadegeneratie", "Maculadegeneratie is schade aan het centrale netvliesgebied (macula) en kan leiden tot verlies van centraal zicht, vooral bij natte maculadegeneratie. Vroege behandeling is belangrijk.", "bod","https://www.oogartsliesenborghs.be/upload/20160621_184183394257694cb4a619d.png"),
			new EyeCondition("Diabetische Retinopathie", "Diabetische retinopathie is een oogziekte die optreedt bij mensen met diabetes en schade aan de bloedvaten van het netvlies veroorzaakt, wat kan leiden tot gezichtsproblemen en zelfs blindheid als het niet wordt behandeld.", "bod","https://www.braille.be/uploads/assets/693/1366896198-diabetische-retinopathie-nl-print-02-500x400.jpg"),
			new EyeCondition("Bijziendheid", "Bijziendheid, ook wel bekend als myopie, is een oogafwijking waarbij je dichtbij goed kunt zien, maar objecten in de verte vaag lijken.", "bod","https://www.optometrie.nl/serverspecific/default/images/Image/fotosheaders/myopie-bewerkt-.jpg"),
			new EyeCondition("Verziendheid", "Verziendheid, ook wel hypermetropie genoemd, is een oogafwijking waarbij objecten dichtbij wazig lijken, terwijl je objecten in de verte beter kunt zien.", "bod","https://www.optometrie.nl/serverspecific/default/images/Image/fotosheaders/headerverziend-bewerkt-.png"),
			new EyeCondition("Astigmatisme", "Astigmatisme is een onregelmatige kromming van het hoornvlies of de ooglens, wat leidt tot vervormd zicht, zowel dichtbij als veraf.", "bod","https://wmimages.uzleuven.be/styles/bb8d67e3da164c4556ec65b16a74c7b3c5a6622b/2020-04/astigmatisme.jpg?style=W3sianBlZyI6eyJxdWFsaXR5Ijo3NX19LHsicmVzaXplIjp7ImZpdCI6Imluc2lkZSIsIndpZHRoIjoxNDIwLCJoZWlnaHQiOjEwODAsIndpdGhvdXRFbmxhcmdlbWVudCI6dHJ1ZX19XQ==&sign=96883d2ec85be89236248aa8190ec56c4edd21247c6ead3f86fbb786bd3dcce4"),
			new EyeCondition("Keratoconus", "Keratoconus is een oogziekte waarbij het hoornvlies geleidelijk dunner en kegelvormig wordt, wat leidt tot wazig zicht en vervormde beelden.", "bod","https://rockymountaineyecenter.com/wp-content/uploads/keratoconus.jpg")
		};

		List<Symptom> symptoms = new()
		{
			new Symptom("Tranende ogen"),
			new Symptom("Droge ogen"),
			new Symptom("Druk in de ogen"),
		};

		conditions[0].Symptom(symptoms[2]);
		conditions[1].Symptom(symptoms[1]);
		conditions[1].Symptom(symptoms[2]);
		conditions[6].Symptom(symptoms[0]);
		dbContext.Symptoms.AddRange(symptoms);
		dbContext.EyeConditions.AddRange(conditions);
		dbContext.SaveChanges();
	}

	private void SeedPatients()
	{
		List<Patient> patients = new()
		{
			new Patient("Joe", "Doe", new DateOnly(2002, 12, 5), "0495357189", "joe.doe@gmail.com"),
			new Patient("Joanna", "Doe", new DateOnly(2002, 4, 23), "0474859614", "joanna.doe@gmail.com"),
		};

		dbContext.Patients.AddRange(patients);
		dbContext.SaveChanges();
	}

	private void SeedAppointments()
	{
		Patient p = new("John", "Doe", new DateOnly(2002, 4, 4), "0497475147", "john.doe@gmail.com");
		Doctor d = dbContext.Employees.OfType<Doctor>().ToList()[0];

		List<Timeslot> timeslots = new()
		{
			new Timeslot(new DateTime(2023, 11, 26, 8, 30, 0))
		};
		d.Timeslot(timeslots);

		List<Appointment> appointments = new()
		{
			new Appointment(p, timeslots[0], "tranende ogen", "allergie aan x"),
			//new Appointment(
		};

		d.Appointment(appointments[0]);
		dbContext.Update(d);

		dbContext.SaveChanges();
	}

	private void SeedSymptoms()
	{
		List<Symptom> symptoms = new()
		{
			new Symptom("Rode ogen"),
			new Symptom("Wazig zicht"),
			new Symptom("Droge ogen"),
			new Symptom("Druk"),
			new Symptom("Tranende ogen")
		};
        dbContext.Symptoms.AddRange(symptoms);
        dbContext.SaveChanges();
    }

	private void SeedBlog()
	{
		List<Fragment> fragments = new()
		{
			new Fragment("Wat is glaucoom?", "Glaucoom is een oogaandoening met verhoogde oogdruk die het gezichtsvermogen kan beschadigen en leiden tot blindheid zonder behandeling."),
			new Fragment("Wat is cataract?", "Cataract is troebelheid van de ooglens, wat wazig zicht veroorzaakt, maar het kan met succes worden behandeld met cataractchirurgie."),
			new Fragment("Wat is maculadegeneratie?", "Maculadegeneratie is schade aan het centrale netvliesgebied (macula) en kan leiden tot verlies van centraal zicht, vooral bij natte maculadegeneratie. Vroege behandeling is belangrijk."),
		};

		//List<Article> articles = new()
		//{			
		//	new Article("Maculadegeneratie", "Maculadegeneratie is schade aan het centrale netvliesgebied (macula) en kan leiden tot verlies van centraal zicht, vooral bij natte maculadegeneratie. Vroege behandeling is belangrijk.", "John Doe", new DateTime(2021, 5, 5), "https://www.oogartsliesenborghs.be/upload/20160621_184183394257694cb4a619d.png", "Wat is maculadegeneratie?", "Maculadegeneratie is schade aan het centrale netvliesgebied (macula) en kan leiden tot verlies van centraal zicht, vooral bij natte maculadegeneratie. Vroege behandeling is belangrijk.", fragments),
		//	new Article("Diabetische Retinopathie", "Diabetische retinopathie is een oogziekte die optreedt bij mensen met diabetes en schade aan de bloedvaten van het netvlies veroorzaakt, wat kan leiden tot gezichtsproblemen en zelfs blindheid als het niet wordt behandeld.", "John Doe", new DateTime(2021, 5, 5), "https://www.braille.be/uploads/assets/693/1366896198-diabetische-retinopathie-nl-print-02-500x400.jpg", "Wat is diabetische retinopathie?", "Diabetische retinopathie is een oogziekte die optreedt bij mensen met diabetes en schade aan de bloedvaten van het netvlies veroorzaakt, wat kan leiden tot gezichtsproblemen en zelfs blindheid als het niet wordt behandeld.", fragments),
		//};

		//List<Article> articles1 = new()
		//{
		//	new Article("Glaucoom", "Glaucoom is een oogaandoening met verhoogde oogdruk die het gezichtsvermogen kan beschadigen en leiden tot blindheid zonder behandeling.", "John Doe", new DateTime(2021, 5, 5), "https://oogziekenhuis.me/omc-amsterdam.nl/Glaucoom_files/shapeimage_10.png", "Wat is glaucoom?", "Glaucoom is een oogaandoening met verhoogde oogdruk die het gezichtsvermogen kan beschadigen en leiden tot blindheid zonder behandeling.", fragments),
		//	new Article("Cataract", "Cataract is troebelheid van de ooglens, wat wazig zicht veroorzaakt, maar het kan met succes worden behandeld met cataractchirurgie.", "John Doe", new DateTime(2021, 5, 5), "https://ogen-blik.expert/wp-content/uploads/2018/12/banner_ogen_blik_cataract_symptomen.jpg", "Wat is cataract?", "Cataract is troebelheid van de ooglens, wat wazig zicht veroorzaakt, maar het kan met succes worden behandeld met cataractchirurgie.", fragments),
		//};

		//List<Blog> blogs = new()
		//{
		//	new Blog("Nieuws", articles),
		//	new Blog("Oogaandoeningen", articles1),
		//};

		//dbContext.Fragments.AddRange(fragments);
		//dbContext.Articles.AddRange(articles);
		//dbContext.Articles.AddRange(articles1);
		//dbContext.Blogs.AddRange(blogs);

		//dbContext.SaveChanges();
	}
}