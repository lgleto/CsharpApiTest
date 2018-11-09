using System.Collections.Generic;
using iDomApiTest.models;
using Microsoft.AspNetCore.Mvc;

namespace iDomApiTest
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        // GET
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return Employee.getAll();
        }
        
        //Get api/employee/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return Employee.get(id);
        }
        
        //Post api/employee
        [HttpPost]
        public string Post([FromBody] Newtonsoft.Json.Linq.JObject value)
        {
            Employee e = Employee.FromJson(value);
            return Employee.Create(e);
        }
        
        //Update api/employee/5
        [HttpPut("{id}")]
        public string Update(int id,[FromBody] Newtonsoft.Json.Linq.JObject value)
        {
            Employee e = Employee.FromJson(value);
            return Employee.Update(id, e);
        }
        
        // DELETE api/employee/5
        [HttpDelete("{id}")]
        public string  Delete(int id)
        {
            return Employee.Delete( id);
        }


    }
}