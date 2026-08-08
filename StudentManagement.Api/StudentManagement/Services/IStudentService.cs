using StudentManagement.Api.Dtos;

namespace StudentManagement.Api.Services
{
    public interface IStudentService
    {
        List<StudentDetailsDto> GetAll();
        StudentDetailsDto? GetById(int id);
        (bool Success, string Message, StudentDetailsDto? Student) Add(CreateStudentDto dto);
        (bool Success, string Message) Update(int id, UpdateStudentDto dto);
        bool Delete(int id);
        List<StudentDetailsDto> SearchByName(string name);
        List<StudentDetailsDto> GetStudentsBetween18And22();
    }
}