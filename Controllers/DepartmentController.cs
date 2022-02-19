using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi_5._0.Model;
using RestApi_5._0.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace RestApi_5._0.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _deptRepo = departmentRepository;
        }

        [HttpGet("Get-Departments")]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await _deptRepo.GetDepartments());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreivng data");
            }
        }
        [HttpGet("Get-department-by-id{id :int}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            try
            {
                var result = await _deptRepo.GetDepartment(id);
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

    }
}
