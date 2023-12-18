using Domain.Users.Employees.Availabilities;
using Oogarts.Domain.Appointments;
using Oogarts.Domain.Articles.Fragments;
using Oogarts.Domain.EyeConditions;
using Oogarts.Domain.Users.Doctors;
using Oogarts.Domain.Users.Employees;
using Oogarts.Domain.Users.Patients;
using Oogarts.Persistence;
using Domain.Users.Employees;

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
		List<Group> groups = new()
		{
			new Group("Oogartsen", 1),
			new Group("Medewerkers", 2),
		};

		List<Employee> members = new()
		{
			new Doctor("Ozlem", "Kose","https://g05devops.blob.core.windows.net/g05devopsblob/b5ee6440-6dca-45fb-9898-2d741ac128b2.jpeg", new DateTime(1985, 7, 15, 8, 30, 0), "0495741468", "ozlemkose@gmail.com", groups[0]),
			new Doctor("Eline", "De Pauw","https://g05devops.blob.core.windows.net/g05devopsblob/4e677571-7bb3-4958-883f-d6b2d55c719c.jpeg", new DateTime(1995, 2, 10, 12, 45, 0), "0823456789", "elinedepauw@gmail.com", groups[0]),
			new Doctor("Diete", "Paternoster","https://g05devops.blob.core.windows.net/g05devopsblob/95d556c3-ab69-4860-9a29-f109e4c1336e.png ", new DateTime(1980, 6, 3, 10, 15, 0), "0934567890", "davidjones@gmail.com", groups[0]),
		};

		dbContext.Groups.AddRange(groups);

		members[0].Bio = new Bio("Dokter Ozlem Kose behaalde haar diploma in de geneeskunde in 2013 aan de Vrije Universiteit Brussel. Na haar opleiding geneeskunde specialiseerde ze zich gedurende vier jaar in de oftalmologie aan de Université Libre de Bruxelles. Dr. Kose behaalde een bijkomende interuniversitaire diploma \"Inflammations et Infections oculaires\" aan de Universiteit Paris Diderot in Frankrijk. Verder is ze Fellow van de European Board of Ophthalmology. Na haar specialisatie trok ze in 2017 een maand naar India voor een fellowship in de cataract en refractieve chirurgie onder begeleiding van Prof. Dr. Agarwal. Vervolgens genoot ze gedurende 6 maanden van een bijkomende chirurgische vorming onder begeleiding van Dr. Gatinel in het Fondation Rothschild te Parijs. Verder sub specialiseerde ze zich in de orbito palpebrale chirurgie (oogleden, traanwegen en orbita) op de afdelingen oftalmologie van het CHU Brugmann ziekenhuis en het Sint-Pietersziekenhuis te Brussel. Ten slotte volgde ze gedurende één haar een fellowship in het Oogziekenhuis van Rotterdam om zich verder te bekwamen in de oculoplastische chirurgie. Actueel is ze sinds 2019 verbonden aan het AZ Sint Lucas Ziekenhuis in Gent waar ze haar vooral toespitst op chirurgie van de oogleden, traanwegen, cataract, maar ook op algemene oogheelkundige aandoeningen.");

		dbContext.Employees.AddRange(members);
		dbContext.SaveChanges();
	}

	private void SeedAvailabilities()
	{
		var member = dbContext.Employees.ToList()[0];

        var availability1 = new Availability(new DateTime(2023, 12, 14, 7, 0, 0), new DateTime(2023, 12, 14, 13, 0, 0));
        var availability2 = new Availability(new DateTime(2023, 12, 15, 8, 0, 0), new DateTime(2023, 12, 15, 13, 0, 0));
        var availability3 = new Availability(new DateTime(2023, 12, 16, 8, 0, 0), new DateTime(2023, 12, 16, 15, 30, 0));
        var availability4 = new Availability(new DateTime(2023, 12, 17, 10, 0, 0), new DateTime(2023, 12, 17, 17, 0, 0));
        var availability5 = new Availability(new DateTime(2023, 12, 18, 13, 0, 0), new DateTime(2023, 12, 18, 17, 0, 0));
        var availability6 = new Availability(new DateTime(2023, 12, 19, 8, 0, 0), new DateTime(2023, 12, 19, 13, 0, 0));



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
			new Specialization("Oogchirurg"),
			new Specialization("Optometrist"),
			new Specialization("Orthoptist"),
		};

		Doctor d = (Doctor)dbContext.Employees.ToList()[0];
		d.Specialization(specializations[0]);
		Doctor d2 = (Doctor)dbContext.Employees.ToList()[1];
		d2.Specialization(specializations[1]);
		Doctor d3 = (Doctor)dbContext.Employees.ToList()[2];
		d2.Specialization(specializations[2]);

		dbContext.Specializations.AddRange(specializations);
		dbContext.Employees.Update(d);
		dbContext.Employees.Update(d2);
		dbContext.Employees.Update(d3);
		dbContext.SaveChanges();
	}

	private void SeedEyeConditions()
	{
		//var conditions = new EyeConditionFaker().AsTransient().UseSeed(1337).Generate(100);
		List<EyeCondition> conditions = new()
		{
			new EyeCondition("Glaucoom", "Glaucoom is een oogaandoening met verhoogde oogdruk die het gezichtsvermogen kan beschadigen en leiden tot blindheid zonder behandeling.", "<div>Glaucoom is een veelvoorkomende oogaandoening die vaak onopgemerkt blijft tot het vergevorderde stadia bereikt. Het is een sluipende bedreiging voor uw gezichtsvermogen omdat het meestal geen symptomen veroorzaakt in de vroege fase. Het is daarom van vitaal belang om te begrijpen wat glaucoom is, hoe het wordt gediagnosticeerd en behandeld, en wat u kunt doen om uw ooggezondheid te beschermen.</div><div><br></div><div><span style=\"font-weight: bold; font-size: large;\">Wat is Glaucoom?</span></div><div>Glaucoom is een groep oogaandoeningen die schade aan de oogzenuw veroorzaken, wat leidt tot verlies van gezichtsvermogen. Meestal is deze schade gerelateerd aan verhoogde druk in het oog, maar niet altijd. Het kan in beide ogen voorkomen, en als het onbehandeld blijft, kan het leiden tot permanente blindheid.</div><div><br></div><div><span style=\"font-weight: bold; font-size: large;\">Soorten Glaucoom</span></div><div>Er zijn verschillende vormen van glaucoom, waarvan de twee meest voorkomende zijn:</div><div><br></div><div><ul><li>Primair Openhoekglaucoom: Dit is de meest voorkomende vorm en ontwikkelt zich langzaam zonder symptomen. Het gezichtsvermogen wordt geleidelijk aangetast.</li><li>Acuut Geslotenhoekglaucoom: Dit is zeldzamer maar ernstiger. Het kan plotseling optreden en symptomen veroorzaken zoals oogpijn, wazig zicht en misselijkheid. Het vereist onmiddellijke medische aandacht.</li></ul></div>","https://oogziekenhuis.me/omc-amsterdam.nl/Glaucoom_files/shapeimage_10.png", ""),
			new EyeCondition("Cataract", "Cataract is troebelheid van de ooglens, wat wazig zicht veroorzaakt, maar het kan met succes worden behandeld met cataractchirurgie.", "bod","https://ogen-blik.expert/wp-content/uploads/2018/12/banner_ogen_blik_cataract_symptomen.jpg", "https://drive.google.com/uc?export=download&id=1MvoydmefB_ppopx5lfO0OODS650sw_P8"),
			new EyeCondition("Maculadegeneratie", "Maculadegeneratie is schade aan het centrale netvliesgebied (macula) en kan leiden tot verlies van centraal zicht, vooral bij natte maculadegeneratie. Vroege behandeling is belangrijk.", "bod","https://www.oogartsliesenborghs.be/upload/20160621_184183394257694cb4a619d.png", ""),
			new EyeCondition("Diabetische Retinopathie", "Diabetische retinopathie is een oogziekte die optreedt bij mensen met diabetes en schade aan de bloedvaten van het netvlies veroorzaakt, wat kan leiden tot gezichtsproblemen en zelfs blindheid als het niet wordt behandeld.", "bod","https://www.braille.be/uploads/assets/693/1366896198-diabetische-retinopathie-nl-print-02-500x400.jpg", ""),
			new EyeCondition("Bijziendheid", "Bijziendheid, ook wel bekend als myopie, is een oogafwijking waarbij je dichtbij goed kunt zien, maar objecten in de verte vaag lijken.", "bod","https://www.optometrie.nl/serverspecific/default/images/Image/fotosheaders/myopie-bewerkt-.jpg", ""),
			new EyeCondition("Verziendheid", "Verziendheid, ook wel hypermetropie genoemd, is een oogafwijking waarbij objecten dichtbij wazig lijken, terwijl je objecten in de verte beter kunt zien.", "bod","https://www.optometrie.nl/serverspecific/default/images/Image/fotosheaders/headerverziend-bewerkt-.png", ""),
			new EyeCondition("Astigmatisme", "Astigmatisme is een veelvoorkomende brekingsfout van het oog, die de manier waarop licht het oog binnenkomt beïnvloedt. Anders dan bij een normaal oog met een perfect bolvormig hoornvlies, vertoont het hoornvlies van iemand met astigmatisme onregelmatigheden in vorm. Dit resulteert in onscherpe en vervormde beelden, waardoor een specifieke correctie nodig is voor helder zicht.", "<div><span style=\"font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);\">Astigmatisme is een veelvoorkomende brekingsfout van het oog, waarbij het hoornvlies onregelmatigheden vertoont in plaats van de normale bolvorm. Deze onregelmatigheden, of het nu een ovaalvormig hoornvlies is of vervormingen in de ooglens, resulteren in onscherpe en vervormde beelden.</span><br></div><div><br></div><div>De oorzaken van astigmatisme zijn divers. Erfelijkheid speelt een belangrijke rol, en het hoornvlies kan zich anders vormen dan normaal. Veranderingen in de vorm van de ooglens kunnen ook bijdragen aan astigmatisme, wat kan voorkomen bij zowel kinderen als volwassenen.</div><div><br></div><div>Symptomen van astigmatisme manifesteren zich als wazig zicht op alle afstanden, verhoogde oogvermoeidheid bij lees- of computeractiviteiten, en zelfs hoofdpijn en oogirritatie door constante inspanning van de ogen.</div><div><br></div><div>Diagnose van astigmatisme vereist een grondig oogonderzoek, waarbij metingen van brekingsfouten worden uitgevoerd. Een cilindrische waarde wordt hierbij gebruikt om de mate van astigmatisme te kwantificeren. Geautomatiseerde instrumenten zoals een autorefractor meten de breking van het oog nauwkeurig.</div><div><br></div><div>Verschillende behandelingsmogelijkheden zijn beschikbaar. Brillen met cilindrische lenzen corrigeren astigmatisme door het licht op de juiste manier te buigen. Torische contactlenzen zijn ontworpen om ongelijke breking van licht te corrigeren. Voor permanente correctie kunnen refractieve chirurgieopties zoals Lasik en PRK worden overwogen, die de vorm van het hoornvlies veranderen.</div><div><br></div><div>Het belang van een gezonde levensstijl, inclusief regelmatige oogcontroles en goed gebruik van verlichting tijdens lezen en werken, kan niet genoeg worden benadrukt. Het bewustzijn van symptomen is cruciaal om veranderingen in het gezichtsvermogen tijdig op te sporen.</div><div><span style=\"font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);\"><br></span></div><div><ul><li><span style=\"font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);\">Regelmatige oogcontroles bij een professionele oogarts zijn essentieel voor het monitoren van astigmatisme en het tijdig detecteren van eventuele veranderingen.</span></li></ul></div><div><br></div><div>In conclusie biedt een grondig begrip van astigmatisme en de beschikbare behandelingsmethoden hoop voor een helderdere toekomst met scherper zicht. Moderne optische technologieën evolueren voortdurend, waardoor patiënten effectieve oplossingen kunnen vinden voor deze veelvoorkomende oogafwijking. Een consultatie met een ervaren oogarts is essentieel voor een nauwkeurige diagnose en een passende behandelingsoptie.</div>","https://wmimages.uzleuven.be/styles/bb8d67e3da164c4556ec65b16a74c7b3c5a6622b/2020-04/astigmatisme.jpg?style=W3sianBlZyI6eyJxdWFsaXR5Ijo3NX19LHsicmVzaXplIjp7ImZpdCI6Imluc2lkZSIsIndpZHRoIjoxNDIwLCJoZWlnaHQiOjEwODAsIndpdGhvdXRFbmxhcmdlbWVudCI6dHJ1ZX19XQ==&sign=96883d2ec85be89236248aa8190ec56c4edd21247c6ead3f86fbb786bd3dcce4", ""),
			new EyeCondition("Keratoconus", "Keratoconus is een oogziekte waarbij het hoornvlies geleidelijk dunner en kegelvormig wordt, wat leidt tot wazig zicht en vervormde beelden.", "bod","https://rockymountaineyecenter.com/wp-content/uploads/keratoconus.jpg", ""),
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