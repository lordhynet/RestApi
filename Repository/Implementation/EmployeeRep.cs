using Microsoft.EntityFrameworkCore;
using RestApi_5._0.Model;
using RestApi_5._0.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi_5._0.Repository.Implementation
{
    public class EmployeeRep : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRep(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Employee>> Search(string name, Gender gender)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();

        }

        public async Task<Employee> GetEmployeeById(int employeeid)
        {
            return await _appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(c => c.EmployeeId == employeeid);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;


        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteEmployee(int employeeid)
        {
            var result = await _appDbContext.Employees
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeid);
            if (result != null)
            {
                _appDbContext.Employees.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
