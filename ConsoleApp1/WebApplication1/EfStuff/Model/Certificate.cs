﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Certificate : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Pupil> Pupils { get; set; }
    }
}
