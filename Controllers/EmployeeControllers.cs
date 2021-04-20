using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi_5._0.Model;
using RestApi_5._0.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_5._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeControllers : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeControllers(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }


        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender gender)
        {
            try
            {
                var result = await _employeeRepo.Search(name, gender);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreivng data");
            }


        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                return Ok(await _employeeRepo.GetEmployees());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreivng data");
            }
        }

        [HttpGet("{id :int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                var result = await _employeeRepo.GetEmployeeById(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreivng data");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();
                var emp = await _employeeRepo.GetEmployeeByEmail(employee.Email);
                if (emp != null)
                {
                    ModelState.AddModelError("Email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var newemployee = await _employeeRepo.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployeeById),
                    new { id = newemployee.EmployeeId }, newemployee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                    return BadRequest("Id mismatch");
                var employeeToUpdate = await _employeeRepo.GetEmployeeById(id);
                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with the Id = {id} not found");
                }

                return await _employeeRepo.UpdateEmployee(employee);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {

                var toDelete = await _employeeRepo.GetEmployeeById(id);
                if (toDelete == null)
                    return NotFound($"Employee with id = {id} not found");
                await _employeeRepo.DeleteEmployee(id);
                return Ok($"Employee with id = {id} Deleted");

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }

        }
    }
}
