using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models.CustomValidationAttributes
{
    public class ElectionControlViewModel
    {
        public Ballot ballot { get; set; }
        
        public Candidate candidate { get; set; }
        
        public Election election { get; set; }
    }
}