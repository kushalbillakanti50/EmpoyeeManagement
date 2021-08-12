using Emp.Models;
using Emp.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeProviderInterface _empprovider { get; set; }

        public EmployeeController(EmployeeProviderInterface emp)
        {
            _empprovider = emp;
        }


        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employeedetails = _empprovider.GetAllEmployees();
            return Ok(employeedetails);
        }

        [HttpGet("GetEmployeesByEmpId", Name="GetEmployeesByEmpId")]
        public IActionResult GetEmployeesByEmpId(int Empid)
        {
            List<EmployeeDetails> employeedetails = _empprovider.GetEmployeesByEmpId(Empid);
            return Ok(employeedetails);
        }

        [HttpGet("GetEmployeesbyDepartmentId", Name = "GetEmployeesbyDepartmentId")]
        public IActionResult GetEmployeesbyDepartmentId(int deptid)
        {
            List<EmployeeDetails> employeedetails = _empprovider.GetEmployeesbyDepartmentId(deptid);
            return Ok(employeedetails);
        }

        
        [HttpPost("UpdateEmployee", Name = "UpdateEmployee")]
        public IActionResult UpdateEmployee(EmployeeDetails employeeDetail)
        {
            EmployeeDetails employeeDetails = _empprovider.UpdateEmployee(employeeDetail);
            return Ok(employeeDetails);
        }

        [HttpPost("CreateEmployee",Name = "CreateEmployee")]
        public IActionResult CreateEmployee( EmployeeDetails employeeDetail)
        {
            EmployeeDetails employeeDetails = _empprovider.CreateEmployee(employeeDetail);
            return Ok(employeeDetails);
        }


        [HttpDelete("DeleteEmployee", Name = "DeleteEmployee")]
        public IActionResult DeleteEmployees(int id)
        {
            EmployeeDetails empdetails = _empprovider.DeleteEmployees(id);
            return Ok(empdetails.EmpName + " is deleted from records");
        }


       [HttpGet("InactiveEmployees",Name ="InactiveEmployees")]
        public IActionResult InactiveEmployees()
        {
            var empdetails = _empprovider.InactiveEmployees();
            return Ok(empdetails);
        }
    }
}
