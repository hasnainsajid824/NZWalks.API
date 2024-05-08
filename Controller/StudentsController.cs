using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
          public IActionResult GetAllStudents()
        {
            string [] studentNames = new string[]{"Hasnain", "Ali", "Raza", "Mujtaba"};
            return Ok(studentNames);
        }
    }
}