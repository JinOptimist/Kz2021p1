using System;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff
{
    public class BallotViewModel
    {
        public Election Election { get; set; }

        public Citizen Citizen { get; set; }

        public Candidate Candidate { get; set; }

        public DateTime VoteTime { get; set; }
    }
}