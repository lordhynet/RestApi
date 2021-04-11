using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi_5._0.Repository.Interfaces;
using System;
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

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                return Ok(await _employeeRepo.GetEmployees());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreivng data")
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var result = await _employeeRepo.GetEmployeeById(id);
                if (result != null)
                {

                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreivng data")
            }
        }
    }
}
