using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
  
        [Authorize (Roles ="Admin")]
        [HttpPost("AddSchool")]
        public ActionResult AddSchool([FromBody]JObject SchoolJson)
        {
            ResultCode result = new ResultCode();
            Admin admin = new Admin();
            school schoolOb = new school();

            schoolOb = JsonConvert.DeserializeObject<school>(SchoolJson.ToString());
          
            if (schoolOb != null)
            {
                result = admin.AddSchoolWithDetails(schoolOb);
            }

            if (result.codeResult == CodeResult.Faild)
            {
                NotFound(result.ErrorMessage);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("EditSchool")]
        public ActionResult EditSchool([FromBody] JObject SchoolJson)
        {
            ResultCode result = new ResultCode();
            Admin admin = new Admin();
            school schoolOb = new school();
            schoolOb = JsonConvert.DeserializeObject<school>(SchoolJson.ToString());

            if (schoolOb != null)
            {
                result = admin.EditSchool(schoolOb);
            }

            if (result.codeResult == CodeResult.Faild)
            {
                NotFound(result.ErrorMessage);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteSchool")]
        public ActionResult DeleteSchool([FromBody] string SchoolNumber)
        {
            ResultCode Result = new ResultCode();
            Admin admin = new Admin();
            int number = int.Parse(SchoolNumber);
            Result = admin.DeleteSchool(number);

            if (Result.codeResult == CodeResult.Faild)
            {
                NotFound(Result.ErrorMessage);
            }
            return Ok(Result);
        }

        [Authorize]
        [HttpPost("getSchoolDetails")]
        public school getSchoolDetails([FromBody] string SchoolName)
        {
            Admin admin = new Admin();
            return admin.getSchoolDetails(SchoolName);
        }

        [Authorize]
        [HttpGet("GetSchoolDetails")]
        public Dictionary<int?,string> GetSchoolDetails()
        {
            Admin admin = new Admin();
            return admin.GetSchoolList();
        }
    }
}