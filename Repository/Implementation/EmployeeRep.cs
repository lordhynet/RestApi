using Microsoft.EntityFrameworkCore;
using RestApi_5._0.Model;
using RestApi_5._0.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
            IQueryable<Employee> query = _appDbContext.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.FirstName.Contains(name) ||
                                         a.LastName.Contains(name));
            }

            if (gender != null)
            {
                query = query.Where(b => b.Gender == gender);
            }

            return await query.ToListAsync();
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
            var result = await _appDbContext.Employees
                .FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBirth = employee.DateOfBirth;
                result.Gender = employee.Gender;
                result.EmployeeId = employee.EmployeeId;
                result.PhotoPath = employee.PhotoPath;

                await _appDbContext.SaveChangesAsync();
                return result;
            }

            return null;

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
