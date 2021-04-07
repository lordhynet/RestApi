using RestApi_5._0.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi_5._0.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int departmentId);
    }
}
