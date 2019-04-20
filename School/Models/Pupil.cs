using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Models
{
    public class Pupil:Person
    {
        public int PupilId { get; set; }
        public string StudyLevel { get; set; }
        public int Class { get; set; }

    }
}
