using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Citizen")]  
        public long CitizenId { get; set; }  
        
        public virtual Citizen Citizen { get; set; }
        
        public virtual Election Election {get; set;}
        
        public virtual ICollection<Election> Elections { get; } = new List<Election>();
        
        public virtual Ballot Ballot { get; set; }
        public virtual ICollection<Ballot> Ballots { get; } = new List<Ballot>();
    }
}

