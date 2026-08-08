using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Dtos;
using StudentManagement.Api.Services;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_studentService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _studentService.GetById(id);
            return student == null ? NotFound("Student not found.") : Ok(student);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateStudentDto dto)
        {
            var result = _studentService.Add(dto);
            if (!result.Success) return BadRequest(result.Message);
            return CreatedAtAction(nameof(GetById), new { id = result.Student!.Id }, result.Student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateStudentDto dto)
        {
            var result = _studentService.Update(id, dto);
            if (!result.Success) return BadRequest(result.Message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _studentService.Delete(id) ? NoContent() : NotFound("Student not found.");
        }

        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name) => Ok(_studentService.SearchByName(name));

        [HttpGet("age-range")]
        public IActionResult GetByAgeRange() => Ok(_studentService.GetStudentsBetween18And22());
    }
}