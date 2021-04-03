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
            throw new System.NotImplementedException();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}
