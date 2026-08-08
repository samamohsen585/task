using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department? GetById(int id);
        Department Add(Department department);
        bool Update(int id, Department department);
        bool Delete(int id);
        List<DepartmentStatsDto> GetStatistics();
        HighestLowestDeptDto GetHighestAndLowest();
    }
}