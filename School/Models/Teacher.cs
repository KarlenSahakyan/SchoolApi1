﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Models
{
    public class Teacher:Person
    {        
        public int TeacherId { get; set; }
        public string Profession { get; set; }

       
    }
}