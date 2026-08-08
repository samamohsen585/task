using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Models;
using StudentManagement.Api.Services;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_departmentService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dept = _departmentService.GetById(id);
            return dept == null ? NotFound("Department not found.") : Ok(dept);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Department dept)
        {
            var created = _departmentService.Add(dept);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Department dept)
        {
            return _departmentService.Update(id, dept) ? NoContent() : NotFound("Department not found");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _departmentService.Delete(id) ? NoContent() : NotFound("Department not found");
        }

        [HttpGet("statistics")]
        public IActionResult GetStatistics() => Ok(_departmentService.GetStatistics());

        [HttpGet("highest-lowest")]
        public IActionResult GetHighestAndLowest() => Ok(_departmentService.GetHighestAndLowest());
    }
}