using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public class StudentService : IStudentService
    {
        public static readonly List<Student> Students = new()
        {
            new Student { Id = 1, Name = "Ahmed", Age = 20, DepartmentId = 1 },
            new Student { Id = 2, Name = "Sama", Age = 21, DepartmentId = 2 }
        };

        private StudentDetailsDto MapToDetailsDto(Student s)
        {
            var dept = DepartmentService.Departments.FirstOrDefault(d => d.Id == s.DepartmentId);
            return new StudentDetailsDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                DepartmentName = dept != null ? dept.Name : "Unknown"
            };
        }
        public List<StudentDetailsDto> GetAll() => Students.Select(MapToDetailsDto).ToList();
        public StudentDetailsDto? GetById(int id)
        {
            var student = Students.FirstOrDefault(s => s.Id == id);
            return student == null ? null : MapToDetailsDto(student);
        }
        public (bool Success, string Message, StudentDetailsDto? Student) Add(CreateStudentDto dto)
        {
            var deptExists = DepartmentService.Departments.Any(d => d.Id == dto.DepartmentId);
            if (!deptExists) return (false, "Department does not exist", null);
            var student = new Student
            {
                Id = Students.Any() ? Students.Max(s => s.Id) + 1 : 1,
                Name = dto.Name,
                Age = dto.Age,
                DepartmentId = dto.DepartmentId
            };
            Students.Add(student);
            return (true, string.Empty, MapToDetailsDto(student));
        }
        public (bool Success, string Message) Update(int id, UpdateStudentDto dto)
        {
            var student = Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return (false, "Student not found");
            var deptExists = DepartmentService.Departments.Any(d => d.Id == dto.DepartmentId);
            if (!deptExists) return (false, "Department does not exist");
            student.Name = dto.Name;
            student.Age = dto.Age;
            student.DepartmentId = dto.DepartmentId;

            return (true, string.Empty);
        }
        public bool Delete(int id)
        {
            var student = Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return false;
            Students.Remove(student);
            return true;
        }
        public List<StudentDetailsDto> SearchByName(string name) =>
            Students.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .Select(MapToDetailsDto).ToList();
        public List<StudentDetailsDto> GetStudentsBetween18And22() =>
            Students.Where(s => s.Age >= 18 && s.Age <= 22)
                    .Select(MapToDetailsDto).ToList();
    }
}