using System.Collections.Generic;

namespace WebApplication1.EfStuff.Model
{
    public class Candidate : BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string AvatarUrl { get; set; }
        public string Job { get; set; }
        public string Slogan { get; set; }
        public City City { get; set; }

        public Idea Idea { get; set; }


        public virtual Citizen Citizen { get; set; }

        public virtual Election Election { get; set; }

        public virtual ICollection<Election> Elections { get; set; }

        public virtual Ballot Ballot { get; set; }

        public virtual ICollection<Ballot> Ballots { get; set; }
    }
}