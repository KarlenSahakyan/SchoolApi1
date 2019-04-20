using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace School.Models.DBManager
{
    public partial class AdminDB
    {
        internal static ResultCode AddScholl(school school)
        {
            ResultCode result = new ResultCode();
            result.codeResult = CodeResult.Normal;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (ApplicationContext dbContext = new ApplicationContext())
                    {
                        if (dbContext.Schools.Any())
                        {
                            school.schoolId = dbContext.Schools.Max(MAX => MAX.schoolId) + 1;
                        }
                        else
                        {
                            school.schoolId = 1;
                        }
                        dbContext.Schools.Add(school);
                        dbContext.SaveChanges();
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

        internal static ResultCode AddTeachers(List<Teacher> teachers)
        {
            ResultCode result = new ResultCode();
            result.codeResult = CodeResult.Normal;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (ApplicationContext dbContext = new ApplicationContext())
                    {

                        teachers?.ForEach(teacher =>
                        {
                            if (!dbContext.Schools.Any(s => s.schoolId == teacher.SchoolNumber))
                            {
                                throw new Exception("NOT FOUND SCHOOL");
                            }

                            if (dbContext.Persons.Any())
                            {
                                teacher.PersonId = dbContext.Persons.Max(MAX => MAX.PersonId) + 1;
                            }

                            dbContext.Add(teacher);
                            dbContext.SaveChanges();
                        });
                        scope.Complete();
                    }
                }

            }
            catch (Exception Ex)
            {
                result.codeResult = CodeResult.Faild;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

        internal static ResultCode AddPupil(List<Pupil> Pupils)
        {
            ResultCode result = new ResultCode();
            result.codeResult = CodeResult.Normal;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (ApplicationContext dbContext = new ApplicationContext())
                    {
                        Pupils?.ForEach(pupil =>
                        {
                            if (!dbContext.Schools.Any(s => s.schoolId == pupil.SchoolNumber))
                            {
                                throw new Exception("NOT FOUND SCHOOL");
                            }

                            if (dbContext.Persons.Any())
                            {
                                pupil.PersonId = dbContext.Persons.Max(MAX => MAX.PersonId) + 1;
                            }

                            dbContext.Add(pupil);
                            dbContext.SaveChanges();
                        });
                        scope.Complete();
                    }
                }
            }
            catch (Exception Ex)
            {
                result.codeResult = CodeResult.Faild;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

        internal static ResultCode AddAdmin(List<Admin> Admins)
        {
            ResultCode result = new ResultCode();
            result.codeResult = CodeResult.Normal;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (ApplicationContext dbContext = new ApplicationContext())
                    {
                        Admins?.ForEach(admin =>
                        {
                            if (!dbContext.Schools.Any(s => s.schoolId == admin.SchoolNumber))
                            {
                                throw new Exception("NOT FOUND SCHOOL");
                            }

                            if (dbContext.Persons.Any())
                            {
                                admin.PersonId = dbContext.Persons.Max(MAX => MAX.PersonId) + 1;
                            }

                            dbContext.Add(admin);
                            dbContext.SaveChanges();
                        });
                        scope.Complete();
                    }
                }
            }
            catch (Exception Ex)
            {
                result.codeResult = CodeResult.Faild;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

    }
}
