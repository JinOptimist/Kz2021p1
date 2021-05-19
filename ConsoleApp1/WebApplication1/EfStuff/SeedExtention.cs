using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;

namespace WebApplication1.EfStuff
{
    public static class SeedExtention
    {
        public const string AdminName = "admin";

        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateDefaultCitizen(scope.ServiceProvider);

                CreateDefaultAdress(scope.ServiceProvider);
                CreateDefaultFireAdmin(scope.ServiceProvider);

                CreateDefaultCertificates(scope.ServiceProvider);

                CreateDefaultUniversities(scope.ServiceProvider);

                CreateDefaultSchools(scope.ServiceProvider);

                CreateDefaultStudents(scope.ServiceProvider);

                CreateDefaultPupils(scope.ServiceProvider);                

                CreateDefaultFlights(scope.ServiceProvider);
            }

            return host;
        }

        private static void CreateDefaultFlights(IServiceProvider serviceProvider)
        {
            IFlightsRepository flightsRepository = serviceProvider.GetService<IFlightsRepository>();
            if (!flightsRepository.HasAnyFlights())
            {
                Random random = new Random();
                FlightStatus[] incomingStatuses = new FlightStatus[] { FlightStatus.Expected, FlightStatus.Delayed, FlightStatus.Landed };
                FlightStatus[] departingStatuses = new FlightStatus[] { FlightStatus.Canceled, FlightStatus.OnTime, FlightStatus.Departed, FlightStatus.Canceled };
                string[] places = new string[] { "Moscow", "New York", "Sydney", "Los Angeles", "Berlin", "Tokyo", "Paris", "Istanbul", "Rome", "Krakow", "Singapore" };
                string[] airlines = new string[] { "International Airline", "Southwest Airline", "Delta Airline", "United Airline", "UC Airline", "Rex Airline" };
                for (int i = 0; i < random.Next(10, 50); i++)
                {
                    Flight departingFlight = new Flight()
                    {
                        TailNumber = GenerateTailNumber(random),
                        FlightType = FlightType.DepartingFlight,
                        Airline = airlines[random.Next(airlines.Length)],
                        FlightStatus = departingStatuses[random.Next(departingStatuses.Length)],
                        Place = places[random.Next(places.Length)],
                        Date = DateTime.Now.AddHours(random.Next(12)).AddMinutes(random.Next(30))
                    };
                    flightsRepository.Save(departingFlight);
                }
                for (int i = 0; i < random.Next(10, 50); i++)
                {
                    Flight incomingFlight = new Flight()
                    {
                        TailNumber = GenerateTailNumber(random),
                        FlightType = FlightType.IncomingFlight,
                        Airline = airlines[random.Next(airlines.Length)],
                        FlightStatus = incomingStatuses[random.Next(incomingStatuses.Length)],
                        Place = places[random.Next(places.Length)],
                        Date = DateTime.Now.AddHours(random.Next(12)).AddMinutes(random.Next(30))
                    };
                    flightsRepository.Save(incomingFlight);
                }
            }
        }

        private static string GenerateTailNumber(Random random)
        {
            var firstPart = $"{(char)('A' + random.Next(26))}{(char)('A' + random.Next(26))}";
            var secondPart = $"{(char)('0' + random.Next(9))}{(char)('0' + random.Next(9))}";
            return $"{firstPart} {secondPart}";
        }

        private static void CreateDefaultAdress(IServiceProvider serviceProvider)
        {
        }

        private static void CreateDefaultCitizen(IServiceProvider serviceProvider)
        {
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();

            var admin = citizenRepository.GetByName(AdminName);
            if (admin == null)
            {
                admin = new Citizen()
                {
                    Name = AdminName,
                    Password = "admin",
                    Age = 30
                };
                citizenRepository.Save(admin);
            }
        }

        private static void CreateDefaultUniversities(IServiceProvider serviceProvider)
        {
            var universityRepository = serviceProvider.GetService<IUniversityRepository>();
            var universities = universityRepository.GetAll().Any();
            if (!universities)
            {
                University university1 = new University()
                {
                    Name = "KazNU",
                    Address = "Al-Farabi 71, Almaty 050000, Kazakhstan",
                    Description = "In connection with declaration of independence of the Republic " +
                    "of Kazakhstan a name of the great scientist, the great thinker and scholar, " +
                    "«the Second teacher» East – Abu nasr al - Farabi was assigned to the Kazakh " +
                    "state university named after Kirov оn October, 23rd, 1991.Assignment of the " +
                    "name al - Farabi leading national university is of great importance for " +
                    "education of students and all youth in spirit of fidelity to the Native land " +
                    "on examples of history and culture of our ancestors.Al - Farabi is the native " +
                    "of the Kazakhstan ground He has written the works in Turkic, Arabian and Persian " +
                    "languages.The name al - Farabi has taken his place in the world of history, science" +
                    " and culture.His works, have rendered dramatic influence not only on progress of " +
                    "Turkish and Kazakh philosophy, but built the bridge for rapproachement of cultures " +
                    "of the West and the East."
                };

                University university2 = new University()
                {
                    Name = "AUPET",
                    Address = "Nauryzbai 27, Almaty 02457874, Kazakhstan",
                    Description = "Almaty University of Power Engineering and Telecommunications " +
                    "on January 10, 1997, on the basis of the Almaty Energy Institute(AEI), which " +
                    "existed from 1975 to 1997, was created. AUPET is the first non - state " +
                    "technical university with the status of a non - profit organization." +
                    "Training is conducted in the Kazakh and Russian languages.In 2013, education " +
                    "in English in two specialties: “Radio Engineering, Electronics and " +
                    "Telecommunications” and “Electric Power Engineering” was started so far. " +
                    "In 1989, the Almaty Energy Institute was the first in Kazakhstan and one " +
                    "of the few in the Soviet Union which was certified by a commission of the " +
                    "State Inspectorate of the USSR State Administration. The high level of training " +
                    "at AEI was officially recognized at the Union level."
                };

                universityRepository.Save(university1);
                universityRepository.Save(university2);
            }
        }

        private static void CreateDefaultSchools(IServiceProvider serviceProvider)
        {
            var schoolRepository = serviceProvider.GetService<ISchoolRepository>();
            var schools = schoolRepository.GetAll().Any();
            if (!schools)
            {
                School school1 = new School()
                {
                    Name = "Miras International School Almaty",
                    Description = "Miras International School Almaty, is an IB World school, " +
                    "accredited by the Council of International Schools, provides high quality " +
                    "education based on Integrated Kazakhstani and International Standards " +
                    "in a multilingual environment devoted to empowering every student to become " +
                    "an internationally minded life - long learner who is responsible, productive " +
                    "and actively engaged in making a positive contribution to an ever changing world." +
                    "ECIS member, UNESCO associated, IBSA (International Baccalaureate Schools " +
                    "Association for CIS countries) member.",
                    Address = "Al-Farabi Avenue 190, Almaty, Kazakhstan"
                };
                School school2 = new School()
                {
                    Name = "Kazakhstan International School",
                    Description = "Kazakhstan International School is proud to be an IB World School, " +
                    "offering the prestigious International Baccalaureate programme of education " +
                    "from Early Years to the IB Diploma, for children aged 2 - 19.We are a friendly," +
                    "family school with close ties between parents, students and teachers.",
                    Address = "Al-Farabi Avenue 118/15, Almaty 050000, Kazakhstan"
                };
                School school3 = new School()
                {
                    Name = "Haileybury Almaty",
                    Description = "Haileybury Almaty opened in 2008, in association with Haileybury " +
                    "in the United Kingdom.It offers a broad, liberal education in the best traditions " +
                    "of British independent schooling.Pupils are prepared for Advanced Levels (‘A levels’), " +
                    "which enable them to apply to and enter the best universities both abroad and in Kazakhstan.",
                    Address = "Al-Farabi Avenue 112, Almaty, Kazakhstan"
                };

                schoolRepository.Save(school1);
                schoolRepository.Save(school2);
                schoolRepository.Save(school3);
            }
        }

        private static void CreateDefaultStudents(IServiceProvider serviceProvider)
        {
            var studentRepository = serviceProvider.GetService<IStudentRepository>();
            var certificateRepository = serviceProvider.GetService<ICertificateRepository>();
            var certificate = certificateRepository.GetCertificateByType("Middle");
            var students = studentRepository.GetAll().Any();

            List<string> studentIins = new List<string>() { "980704401339", "971104401334", "960915401335", "990521401336", "001210401337", "010229401338" };
            List<string> studentNames = new List<string>() { "Samat", "Asem", "Arai", "Sunkar", "Madina", "Askar" };
            List<string> studentSurnames = new List<string>() { "Maksatov", "Askatova", "Samatova", "Madiyarov", "Alieva", "Zhanibekov" };
            List<string> studentPatronymics = new List<string>() { "Maksatuly", "Askatkyzy", "Samatkyzy", "Madiyaruly", "Alikyzy", "Kozhauly" };
            List<string> studentBirthdays = new List<string>() { "04.07.98", "04.11.97", "15.09.96", "21.05.99", "10.12.00", "29.02.01" };
            List<string> studentEmails = new List<string>() { "samat@gmail.com", "asem@gmail.com", "arai@gmail.com", "sunkar@gmail.com", "madina@gmail.com", "askar@gmail.com" };
            List<string> studentFaculties = new List<string>() { "BIOLOGY_AND_BIOTECHNOLOGY", "INTERNATIONAL_RELATIONS_DEPARTMENT", "PHYSICS_AND_TECHNOLOGY", "GEOGRAPHY_AND_ENVIRONMENTAL_SCIENCES", "MECHANICAL_MATHEMATICS", "INFORMATION_TECHNOLOGY" };
            List<int> studentCourseYears = new List<int>() { 3, 4, 3, 4, 2, 2 };
            List<double> studentGpas = new List<double>() { 4.0, 3.67, 3.3, 2.67, 3.0, 2.3 };
            List<bool> studentOnGrant = new List<bool>() { true, true, true, false, true, false };
            List<string> studentEnteredYears = new List<string>() { "08-08-2015", "08-08-2015", "08-08-2014", "08-08-2016", "08-08-2017", "08-08-2018" };
            List<long> studentUniverIds = new List<long>() { 1, 2, 2, 1, 2, 1 };

            if (!students)
            {
                for (int i = 0; i < studentIins.Count; i++)
                {
                    Student student = new Student()
                    {
                        Iin = studentIins[i],
                        Name = studentNames[i],
                        Surname = studentSurnames[i],
                        Patronymic = studentPatronymics[i],
                        AvatarUrl = null,
                        Birthday = studentBirthdays[i],
                        Email = studentEmails[i],
                        Faculty = studentFaculties[i],
                        CourseYear = studentCourseYears[i],
                        Gpa = studentGpas[i],
                        IsGrant = studentOnGrant[i],
                        EnteredYear = DateTime.ParseExact(studentEnteredYears[i], new string[] { "MM.dd.yyyy", "MM-dd-yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                        GraduatedYear = null,
                        UniversityId = studentUniverIds[i],
                        Certificates = certificate as ICollection<Certificate>
                    };

                    studentRepository.Save(student);
                }
            }
        }

        private static void CreateDefaultPupils(IServiceProvider serviceProvider)
        {
            var pupilRepository = serviceProvider.GetService<IPupilRepository>();
            var pupils = pupilRepository.GetAll().Any();

            List<string> pupilIins = new List<string>() { "061231401442", "050131401441", "041005401442", "070915401443", "051105401442", "051104401442" };
            List<string> pupilNames = new List<string>() { "Aigerim", "Aliya", "Damir", "Aybek", "Kaisar", "Kairat" };
            List<string> pupilSurnames = new List<string>() { "Askarova", "Zheksen", "Sadykov", "Alishev", "Karimov", "Karimov" };
            List<string> pupilPatronymics = new List<string>() { "Maksatkyzy", "Zhuniskyzy", "Nursatuly", "Bekzhanuly", "Nursatuly", "Nursatuly" };
            List<string> pupilBirthdays = new List<string>() { "31.12.06", "31.01.05", "04.10.05", "15.09.07", "05.11.05", "05.11.04" };
            List<string> pupilEmails = new List<string>() { "aigerim@gmail.com", "aliya@gmail.com", "damir@gmail.com", "aybek@gmail.com", "kaisar@gmail.com", "kairat@gmail.com" };
            List<int> pupilClassYears = new List<int>() { 9, 10, 11, 8, 11, 11 };
            List<int> pupilAverageMarks = new List<int>() { 12, 10, 9, 11, 9, 9 };
            List<long> pupilSchoolIds = new List<long>() { 1, 2, 3, 1, 2, 3 };

            if (!pupils)
            {
                for (int i = 0; i < pupilIins.Count; i++)
                {
                    Pupil pupil = new Pupil()
                    {
                        Iin = pupilIins[i],
                        Name = pupilNames[i],
                        Surname = pupilSurnames[i],
                        Patronymic = pupilPatronymics[i],
                        AvatarUrl = null,
                        Birthday = pupilBirthdays[i],
                        Email = pupilEmails[i],
                        ClassYear = pupilClassYears[i],
                        AverageMark = pupilAverageMarks[i],
                        GraduatedYear = null,
                        ENT = null,
                        SchoolId = pupilSchoolIds[i],
                        Certificate = null
                    };

                    pupilRepository.Save(pupil);
                }
            }
        }

        private static void CreateDefaultFireAdmin(IServiceProvider serviceProvider)
        {
            var firemanRepository = serviceProvider.GetService<IFiremanRepository>();
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();

            var firecitizen = citizenRepository.GetByName("FireAdmin");
            if (firecitizen == null)
            {
                firecitizen = new Citizen()
                {
                    Name = "FireAdmin",
                    Password = "fireadmin",
                    Age = 40
                };
                citizenRepository.Save(firecitizen);
                var fireadmin = new Fireman()
                {
                    Role = "FireAdmin",
                    WorkExperYears = 10,
                    CitizenId = firecitizen.Id,
                    Citizen = firecitizen
                };
                firemanRepository.Save(fireadmin);
            }
        }


        private static void CreateDefaultCertificates(IServiceProvider serviceProvider)
        {
            var certificateRepository = serviceProvider.GetService<ICertificateRepository>();
            var certificates = certificateRepository.GetAll().Any();
            if (!certificates)
            {
                var certificate1 = new Certificate()
                {
                    Type = "Middle",
                    CertificateImgUrl = "middle.jpeg"
                };

                var certificate2 = new Certificate()
                {
                    Type = "High",
                    CertificateImgUrl = "high.jpeg"
                };

                var certificate3 = new Certificate()
                {
                    Type = "Police",
                    CertificateImgUrl = "police.jpeg"
                };

                var certificate4 = new Certificate()
                {
                    Type = "Medicine",
                    CertificateImgUrl = "medicine.jpeg"
                };

                certificateRepository.Save(certificate1);
                certificateRepository.Save(certificate2);
                certificateRepository.Save(certificate3);
                certificateRepository.Save(certificate4);
            }
        }
    }
}
