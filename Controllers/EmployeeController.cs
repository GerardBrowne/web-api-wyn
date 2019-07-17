using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WAPI.Models;

namespace WAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApiContext _ctx;

        public EmployeeController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var emps = _ctx.Employees.OrderBy(e => e.Id);

            return Ok(emps);
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult Get(int id)
        {
            var emp = _ctx.Employees.Find(id);

            return Ok(emp);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetEmployee", new Employee { Id = employee.Id }, employee);
        }
    }
}