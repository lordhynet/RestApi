using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi_5._0.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace RestApi_5._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _deptRepo = departmentRepository;
        }

        [HttpGet]
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

    }
}
