using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Models
{
    public class school
    {
        public int? schoolId { get; set; }
        [Key]
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Pupil> Pupils { get; set; }
        public List<Admin> Admins { get; set; }


        public school() { }

        public school(school s)
        {
            this.Name = s.Name;
            this.Address = s.Address;
            this.schoolId = s.schoolId;
        }
    }
}
