using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public int SchoolNumber { get; set; }
        public byte PersonType { get; set; }
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
