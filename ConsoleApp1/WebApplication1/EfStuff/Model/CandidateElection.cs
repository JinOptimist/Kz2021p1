using System;

namespace WebApplication1.EfStuff.Model
{
    public class CandidateElection : BaseModel
    {
        public long CandidateId { get; set; }
     
        public  virtual Candidate Candidate { get; set; }
        
        public long ElectionId { get; set; }
        
        public  virtual Election Election { get; set; }
        
        public DateTime CandidateRegistrationTime { get; set; }
            
    }

}