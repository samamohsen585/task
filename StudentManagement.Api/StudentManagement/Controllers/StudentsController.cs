using Microsoft.AspNetCore.Mvc;
namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        
        [HttpGet("welcome")]
        public IActionResult Welcome()
        {
            return Ok("Welcome to Student Management API");
        }
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Ahmed", Age = 20, DepartmentName="IS" },
            new Student { Id = 2, Name = "Sama", Age = 22, DepartmentName="CS" }
        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(students);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} was not found");
            }
            return Ok(student);
        }
        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name parameter is required");
            }
            var result = students
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(result);
        }
        [HttpGet("filter-by-age")]
        public IActionResult FilterByAge()
        {
            var result = students
                .Where(s => s.Age >= 18 && s.Age <= 22)
                .OrderBy(s => s.Age)
                .ToList();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student newStudent)
        {
            if (newStudent == null || string.IsNullOrWhiteSpace(newStudent.Name))
            {
                return BadRequest("Invalid student data");
            }
            newStudent.Id = students.Any() ? students.Max(s => s.Id) + 1 : 1;
            students.Add(newStudent);
            return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var existingStudent = students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} was not found");
            }
            if (updatedStudent == null || string.IsNullOrWhiteSpace(updatedStudent.Name))
            {
                return BadRequest("Invalid student data");
            }
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.DepartmentName = updatedStudent.DepartmentName;
            return Ok(existingStudent);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} was not found");
            }
            students.Remove(student);
            return Ok(new { message = $"Student with ID {id} has been deleted successfully" });
        }
    } 
}