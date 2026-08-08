using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public class DepartmentService : IDepartmentService
    {
        public static readonly List<Department> Departments = new()
        {
            new Department { Id = 1, Name = "IT" },
            new Department { Id = 2, Name = "HR" },
            new Department { Id = 3, Name = "Finance" },
            new Department { Id = 4, Name = "Sales" }
        };

        public List<Department> GetAll() => Departments;

        public Department? GetById(int id) => Departments.FirstOrDefault(d => d.Id == id);

        public Department Add(Department department)
        {
            department.Id = Departments.Any() ? Departments.Max(d => d.Id) + 1 : 1;
            Departments.Add(department);
            return department;
        }

        public bool Update(int id, Department department)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Name = department.Name;
            return true;
        }

        public bool Delete(int id)
        {
            var dept = GetById(id);
            if (dept == null) return false;
            Departments.Remove(dept);
            return true;
        }

        public List<DepartmentStatsDto> GetStatistics()
        {
            return Departments.Select(d =>
            {
                var studentsInDept = StudentService.Students.Where(s => s.DepartmentId == d.Id).ToList();
                return new DepartmentStatsDto
                {
                    DepartmentName = d.Name,
                    StudentCount = studentsInDept.Count,
                    AverageAge = studentsInDept.Any() ? studentsInDept.Average(s => s.Age) : 0,
                    OldestAge = studentsInDept.Any() ? studentsInDept.Max(s => s.Age) : 0,
                    YoungestAge = studentsInDept.Any() ? studentsInDept.Min(s => s.Age) : 0
                };
            }).ToList();
        }

        public HighestLowestDeptDto GetHighestAndLowest()
        {
            var stats = GetStatistics();
            if (!stats.Any()) return new HighestLowestDeptDto();

            var maxStudents = stats.Max(s => s.StudentCount);
            var minStudents = stats.Min(s => s.StudentCount);

            return new HighestLowestDeptDto
            {
                HighestDepartment = stats.FirstOrDefault(s => s.StudentCount == maxStudents)?.DepartmentName ?? "N/A",
                LowestDepartment = stats.FirstOrDefault(s => s.StudentCount == minStudents)?.DepartmentName ?? "N/A"
            };
        }
    }
}