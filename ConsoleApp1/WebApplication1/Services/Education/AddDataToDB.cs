using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using AutoMapper.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApplication1.EfStuff.Repositoryies;

namespace WebApplication1.Services.Education
{
    public class AddDataToDB
    {
         private static StudentRepository _studentRepository;
         private static PupilRepository _pupilRepository;
         private static UniversityRepository _universityRepository;
         private static SchoolRepository _schoolRepository;

  /*      private StudentRepository _studentRepository;
        private PupilRepository _pupilRepository;
        private UniversityRepository _universityRepository;
        private SchoolRepository _schoolRepository;*/

        public AddDataToDB(StudentRepository studentRepository, PupilRepository pupilRepository, UniversityRepository universityRepository, SchoolRepository schoolRepository)
        {
            _studentRepository = studentRepository;
            _pupilRepository = pupilRepository;
            _universityRepository = universityRepository;
            _schoolRepository = schoolRepository;
        }
        public static void AddData(string connectionString)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                if (_studentRepository.GetAll().Count() == 0)
                {
                    string str = "(Faculty, CourseYear, Gpa, OnGrant, EnteredYear, GraduatedYear, UniversityId, IIN, Name, Surname, Patronymic, Birthday, Email)";
                    string commandString1 = "INSert into Students" + str + "Values('Fiz-tech', 3, 4.0, 1, '2015', '2019', 100, '981004401339', 'Samat', 'Maksatov', 'Maksatuly', '12.03.01', 'samat@gmail.com')";
                    string commandString2 = "INSert into Students" + str + "Values('Fiz-tech', 4, 3.76, 1, '2015','2019', 101, '971104401334', 'Asem', 'Askatova', 'Askatkyzy', '22.05.98', 'asem@gmail.com')";
                    string commandString3 = "INSert into Students" + str + "Values('Fiz-tech', 3, 3.3, 1, '2014', '2018', 101, '960904401335', 'Arai', 'Samatova', 'Samatkyzy', '17.01.99','arai@gmail.com')";
                    string commandString4 = "INSert into Students" + str + "Values('Fiz-tech', 4, 2.67, 0, '2016', '2020', 100, '990504401336', 'Sunkar', 'Madiyarov', 'Madiyaruly', '29.06.97','sunkar@gmail.com')";
                    string commandString5 = "INSert into Students" + str + "Values('Fiz-tech', 3, 3.0, 1, '2017', null, 101, '001204401337', 'Madina', 'Alieva', 'Alikyzy', '11.07.01','madina@gmail.com')";
                    string commandString6 = "INSert into Students" + str + "Values('Fiz-tech', 3, 3.0, 1, '2017', null, 101, '001204401337', 'Madina', 'Alieva', 'Alikyzy', '11.07.01','madina@gmail.com')";
                    string commandString7 = "INSert into Students" + str + "Values('Fiz-tech', 2, 2.3, 0, '2018', null, 100, '010204401338', 'Askar', 'Zhanibekov', 'Kozhauly', '21.09.02','askar@gmail.com')";

                    List<string> commandStrings = new List<string>();
                    commandStrings.Add(commandString1);
                    commandStrings.Add(commandString2);
                    commandStrings.Add(commandString3);
                    commandStrings.Add(commandString4);
                    commandStrings.Add(commandString5);
                    commandStrings.Add(commandString6);
                    commandStrings.Add(commandString7);

                    foreach (string commandString in commandStrings)
                    {
                        SqlCommand command = new SqlCommand(commandString, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("success");
                }

                if (_pupilRepository.GetAll().Count() == 0)
                {
                    string str = "(Subject, ClassYear, AverageMark, GraduatedYear, ENT, SchoolId, IIN, Name, Surname, Patronymic, Birthday, Email)";
                    string commandString1 = "INSert into Pupils" + str + "Values('Physics', 9, 12, null, null, 10, '061231401442', 'Aigerim', 'Askarova', 'Maksatkyzy', '11.12.06', 'aigerim@gmail.com')";
                    string commandString2 = "INSert into Pupils" + str + "Values('Geography', 10, 10, null, null, 12, '050131401441', 'Aliya', 'Zheksen', 'Zhuniskyzy', '31.01.05', 'aliya@gmail.com')";
                    string commandString3 = "INSert into Pupils" + str + "Values('Matem', 11, 9, '2021.06.15', 109, 11, '041005401442', 'Damir', 'Sadykov', 'Nursatuli', '05.10.04', 'damir@gmail.com')";
                    string commandString4 = "INSert into Pupils" + str + "Values('History', 8, 11, null, null, 12, '070915401443', 'Aybek', 'Alishev', 'Bekzhanuli', '15.09.07', 'aybek@gmail.com')";
                    string commandString5 = "INSert into Pupils" + str + "Values('Matem', 11, 9, '2021.06.15', 115, 11, '051105401442', 'Kaisar', 'Karimov', 'Nursatuli', '05.11.05', 'kaisar@gmail.com')";
                    string commandString6 = "INSert into Pupils" + str + "Values('Matem', 11, 9, '2021.06.15', 110, 11, '051104401442', 'Kairat', 'Karimov', 'Nursatuli', '05.11.04', 'kairat@gmail.com')";

                    List<string> commandStrings = new List<string>();
                    commandStrings.Add(commandString1);
                    commandStrings.Add(commandString2);
                    commandStrings.Add(commandString3);
                    commandStrings.Add(commandString4);
                    commandStrings.Add(commandString5);
                    commandStrings.Add(commandString6);

                    foreach (string commandString in commandStrings)
                    {
                        SqlCommand command = new SqlCommand(commandString, connection);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("success");
                }
                if (_universityRepository.GetAll().Count() == 0)
                {
                    string commandText = "DBCC CHECKIDENT (N'dbo.Universities', RESEED, 100);";
                    SqlCommand command1 = new SqlCommand(commandText, connection);
                    command1.ExecuteNonQuery();

                    string str = "(Name, Address, Description)";
                    string commandString1 = "Insert into Universities" + str + "Values('KazNU','Al-Farabi 71, Almaty 050000, Kazakhstan', " +
                        "'In connection with declaration of independence of the Republic of Kazakhstan a name of the great scientist, " +
                        "the great thinker and scholar, «the Second teacher» East – Abu nasr al - Farabi was assigned to the Kazakh " +
                        "state university named after Kirov оn October, 23rd, 1991.Assignment of the name al-Farabi leading national " +
                        "university is of great importance for education of students and all youth in spirit of fidelity to the Native " +
                        "land on examples of history and culture of our ancestors.Al - Farabi is the native of " +
                        "the Kazakhstan ground He has written the works in Turkic, Arabian and Persian languages. " +
                        "The name al - Farabi has taken his place in the world of history, science and culture. " +
                        "His works, have rendered dramatic influence not only on progress of Turkish and Kazakh " +
                        "philosophy, but built the bridge for rapproachement of cultures of the West and the East.')";

                    string commandString2 = "Insert into Universities" + str + "Values('AUPET', 'Nauryzbai 27, Almaty 02457874, " +
                        "Kazakhstan', 'Almaty University of Power Engineering and Telecommunications on January 10, 1997, " +
                        "on the basis of the Almaty Energy Institute(AEI), which existed from 1975 to 1997, was created. " +
                        "AUPET is the first non - state technical university with the status of a non-profit organization. " +
                        "Training is conducted in the Kazakh and Russian languages.In 2013, education in English in two " +
                        "specialties: “Radio Engineering, Electronics and Telecommunications” and “Electric Power " +
                        "Engineering” was started so far. In 1989, the Almaty Energy Institute was the first in " +
                        "Kazakhstan and one of the few in the Soviet Union which was certified by a commission " +
                        "of the State Inspectorate of the USSR State Administration. The high level of training " +
                        "at AEI was officially recognized at the Union level.')";


                    List<string> commandStrings = new List<string>();
                    commandStrings.Add(commandString1);
                    commandStrings.Add(commandString2);

                    foreach (string commandString in commandStrings)
                    {
                        SqlCommand command2 = new SqlCommand(commandString, connection);
                        command2.ExecuteNonQuery();
                    }

                    Console.WriteLine("success");
                }
                if (_schoolRepository.GetAll().Count() == 0)
                {
                    string commandText = "DBCC CHECKIDENT (N'dbo.Schools', RESEED, 10);";
                    SqlCommand command1 = new SqlCommand(commandText, connection);
                    command1.ExecuteNonQuery();

                    string str = "(Name, Address, Description)";
                    string commandString1 = "insert into Schools" + str + "values('Miras International School Almaty', " +
                        "'Al-Farabi Avenue 190, Almaty, Kazakhstan', 'Miras International School Almaty, " +
                        "is an IB World school, accredited by the Council of International Schools, provides " +
                        "high quality education based on Integrated Kazakhstani and International Standards " +
                        "in a multilingual environment devoted to empowering every student to become an " +
                        "internationally minded life-long learner who is responsible, productive and actively " +
                        "engaged in making a positive contribution to an ever changing world.ECIS member, " +
                        "UNESCO associated, IBSA(International Baccalaureate Schools Association for CIS countries) member.')";

                    string commandString2 = "insert into Schools" + str + "values('Kazakhstan International School', " +
                        "'Al-Farabi Avenue 118/15, Almaty 050000, Kazakhstan', 'Kazakhstan International " +
                        "School is proud to be an IB World School, offering the prestigious International " +
                        "Baccalaureate programme of education from Early Years to the IB Diploma, for children " +
                        "aged 2 - 19.We are a friendly, family school with close ties between parents, students " +
                        "and teachers.')";

                    string commandString3 = "insert into Schools" + str + "values('Haileybury Almaty', " +
                        "'Al-Farabi Avenue 112, Almaty, Kazakhstan', 'Haileybury Almaty opened in 2008, in " +
                        "association with Haileybury in the United Kingdom.It offers a broad, liberal education " +
                        "in the best traditions of British independent schooling.Pupils are prepared for Advanced " +
                        "Levels (‘A levels’), which enable them to apply to and enter the best universities both " +
                        "abroad and in Kazakhstan.')";

                    List<string> commandStrings = new List<string>();
                    commandStrings.Add(commandString1);
                    commandStrings.Add(commandString2);
                    commandStrings.Add(commandString3);

                    foreach (string commandString in commandStrings)
                    {
                        SqlCommand command2 = new SqlCommand(commandString, connection);
                        command2.ExecuteNonQuery();
                    }

                    Console.WriteLine("success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }



    }
}

