using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using School.Models.DBManager;

namespace School.Models
{
    public class Admin : Person
    {
        public int AdminId { get; set; }

        public ResultCode AddScholl(school school)
        {
            return AdminDB.AddScholl(school);
        }

        public ResultCode AddTeachers(List<Teacher> teachers)
        {
            return AdminDB.AddTeachers(teachers);
        }

        public ResultCode AddPupils(List<Pupil> Pupils)
        {
            return AdminDB.AddPupil(Pupils);
        }

        public ResultCode AddAdmin(List<Admin> Admins)
        {
            return AdminDB.AddAdmin(Admins);
        }

        public ResultCode AddSchoolWithDetails(school schoolOb)
        {
            school schoolDetails = new school { Address = schoolOb.Address, Name = schoolOb.Name };

            List<Teacher> teachers = new List<Teacher>();
            if (schoolOb.Teachers.Count > 0)
            {
                schoolOb.Teachers?.ForEach(teacher =>
                {
                    teachers.Add(teacher);
                });
            }

            List<Pupil> pupils = new List<Pupil>();
            if (schoolOb.Pupils.Count > 0)
            {
                schoolOb.Pupils?.ForEach(pupil =>
                {
                    pupils.Add(pupil);
                });
            }

            List<Admin> admins = new List<Admin>();
            if (schoolOb.Admins.Count > 0)
            {
                schoolOb.Admins?.ForEach(adminItem =>
                {
                    admins.Add(adminItem);
                });
            }
            return AdminDB.AddSchoolWithDetails(schoolDetails, teachers, pupils, admins);
        }

        public ResultCode EditSchool(school schoolDetails)
        {
            return AdminDB.EditSchool(schoolDetails);
        }

        public ResultCode DeleteSchool(int schoolNumber)
        {
            return AdminDB.DeleteSchool(schoolNumber);
        }

        public school getSchoolDetails(string SchoolName)
        {
            return AdminDB.getSchoolDetails(SchoolName);
        }

        public Dictionary<int?, string> GetSchoolList()
        {
            return AdminDB.GetSchoolList();
        }

    }
}
