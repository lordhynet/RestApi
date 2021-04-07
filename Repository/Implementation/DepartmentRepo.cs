using Microsoft.EntityFrameworkCore;
using RestApi_5._0.Model;
using RestApi_5._0.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi_5._0.Repository.Implementation
{
    public class DepartmentRepo : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Department.ToListAsync();
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            throw new System.NotImplementedException();
        }
    }
}
