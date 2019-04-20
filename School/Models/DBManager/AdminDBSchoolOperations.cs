using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace School.Models.DBManager
{
    public partial class AdminDB
    {
        internal static ResultCode AddSchoolWithDetails(school schoolDetales, List<Teacher> teachers, List<Pupil> pupils, List<Admin> admins)
        {
            ResultCode result = new ResultCode();
            result.codeResult = CodeResult.Normal;

            try
            {
                if (schoolDetales != null)
                {
                    AddScholl(schoolDetales);
                }
                else
                {
                    result.codeResult = CodeResult.Faild;
                    result.ErrorMessage = "School Details not found";
                    return result;
                }

                if (teachers.Count > 0)
                {
                    AddTeachers(teachers);
                }

                if (pupils.Count > 0)
                {
                    AddPupil(pupils);
                }

                if (admins.Count > 0)
                {
                    AddAdmin(admins);
                }

            }
            catch (Exception Ex)
            {
                result.codeResult = CodeResult.Faild;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

        internal static ResultCode EditSchool(school schoolDetails)
        {
            ResultCode result = new ResultCode();
            result.codeResult = CodeResult.Normal;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (schoolDetails != null)
                    {
                        using (ApplicationContext contextDB = new ApplicationContext())
                        {
                            List<school> oldSchoolFind = contextDB.Schools.Where(s => s.Name == schoolDetails.Name).ToList(); //<school>(s => s.Name == schoolDetails.Name);

                            if (oldSchoolFind.Count == 0)
                            {
                                result.codeResult = CodeResult.Faild;

                                result.ErrorMessage = "NOT FOUND";
                                return result;
                            }

                            if (schoolDetails.Teachers.Count != 0)
                            {
                                List<Teacher> Teachers = schoolDetails.Teachers;

                                Teachers?.ForEach(item =>
                                {
                                    if (contextDB.Teachers.Any(t => t.Login == item.Login))
                                    {
                                        Teacher teacher = contextDB.Teachers.First(t => t.Login == item.Login);
                                        teacher.Adress = item.Adress;
                                        teacher.Age = item.Age;
                                        teacher.FirstName = item.FirstName;
                                        teacher.LastName = item.LastName;
                                        teacher.Password = item.Password;
                                        teacher.PersonId = item.PersonId;
                                        teacher.Profession = item.Profession;
                                        contextDB.SaveChanges();
                                    }
                                    else
                                    {
                                        item.PersonId = contextDB.Persons.Max(MAX => MAX.PersonId) + 1;
                                        contextDB.Teachers.Add(item);
                                        contextDB.SaveChanges();
                                    }

                                });
                            }

                            if (schoolDetails.Pupils.Count != 0)
                            {
                                List<Pupil> Pupils = schoolDetails.Pupils;

                                Pupils?.ForEach(item =>
                                {
                                    if (contextDB.Pupils.Any(p => p.Login == item.Login))
                                    {
                                        Pupil pupil = contextDB.Pupils.First(p => p.Login == item.Login);
                                        pupil.Adress = item.Adress;
                                        pupil.Age = item.Age;
                                        pupil.FirstName = item.FirstName;
                                        pupil.LastName = item.LastName;
                                        pupil.Password = item.Password;
                                        pupil.PersonId = item.PersonId;
                                        pupil.Class = item.Class;
                                        pupil.StudyLevel = item.StudyLevel;
                                        contextDB.SaveChanges();
                                    }
                                    else
                                    {
                                        item.PersonId = contextDB.Persons.Max(MAX => MAX.PersonId) + 1;
                                        contextDB.Pupils.Add(item);
                                        contextDB.SaveChanges();
                                    }

                                });
                            }

                            if (schoolDetails.Admins.Count != 0)
                            {
                                List<Admin> Admins = schoolDetails.Admins;

                                Admins?.ForEach(item =>
                                {
                                    if (contextDB.Admins.Any(a => a.Login == item.Login))
                                    {
                                        Admin admin = contextDB.Admins.First(a => a.Login == item.Login);
                                        admin.Adress = item.Adress;
                                        admin.Age = item.Age;
                                        admin.FirstName = item.FirstName;
                                        admin.LastName = item.LastName;
                                        admin.Password = item.Password;
                                        admin.PersonId = item.PersonId;
                                        admin.Role = "Admin";
                                        contextDB.SaveChanges();
                                    }
                                    else
                                    {
                                        item.PersonId = contextDB.Persons.Max(MAX => MAX.PersonId) + 1;
                                        contextDB.Admins.Add(item);
                                        contextDB.SaveChanges();
                                    }

                                });
                            }
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.codeResult = CodeResult.Faild;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        internal static ResultCode DeleteSchool(int schoolNumber)
        {
            ResultCode result = new ResultCode();
            result.codeResult = CodeResult.Normal;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var pupils = db.Pupils.Where(p => p.SchoolNumber == schoolNumber).ToList();
                        if (pupils != null)
                        {
                            pupils?.ForEach(p =>
                            {
                                db.Remove(p);
                                db.SaveChanges();
                            });
                        }

                        var teachers = db.Teachers.Where(t => t.SchoolNumber == schoolNumber).ToList();
                        if (teachers != null)
                        {
                            teachers?.ForEach(t =>
                            {
                                db.Remove(t);
                                db.SaveChanges();
                            });
                        }

                        var admins = db.Admins.Where(a => a.SchoolNumber == schoolNumber).ToList();
                        if (admins != null)
                        {
                            admins?.ForEach(a =>
                            {
                                db.Remove(a);
                                db.SaveChanges();
                            });
                        }

                        var schools = db.Schools.Where(s => s.schoolId == schoolNumber).ToList();
                        if (schools != null)
                        {
                            schools?.ForEach(s =>
                            {
                                db.Remove(s);
                                db.SaveChanges();
                            });
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.codeResult = CodeResult.Faild;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        internal static school getSchoolDetails(string SchoolName)
        {
            school result = new school();
            using (ApplicationContext db = new ApplicationContext())
            {
                result = db.Schools.FirstOrDefault(s => s.Name == SchoolName);
                if (result != null)
                {
                    result.Pupils = db.Pupils.Where(w => w.SchoolNumber == result.schoolId).ToList();
                    result.Teachers = db.Teachers.Where(t => t.SchoolNumber == result.schoolId).ToList();
                    result.Admins = db.Admins.Where(a => a.SchoolNumber == result.schoolId).ToList();
                }
            }

            return result;
        }

        internal static  Dictionary<int?,string> GetSchoolList()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Dictionary<int?, string> List = db.Schools.ToDictionary(s => s.schoolId, s => s.Name);
                return List;
            }
        }
    }
}
