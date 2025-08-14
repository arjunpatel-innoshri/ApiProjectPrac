using ApiProjectPrac.Helpers;
using ApiProjectPrac.Models;
using ApiProjectPrac.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectPrac.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetAllAsync();
            return Ok(new ApiResponse<List<Employee>> { Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ApiResponse<string> { Success = false, Message = "Employee not found" });
            return Ok(new ApiResponse<Employee> { Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            var result = await _employeeService.CreateAsync(employee);
            return Ok(new ApiResponse<Employee> { Data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _employeeService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<string> { Success = false, Message = "Employee not found", Error = "Error in deleting User" });
            return Ok(new ApiResponse<string> { Message = "Deleted successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Employee employee)
        {
            var result = await _employeeService.UpdateAsync(id, employee);
            if (result == null)
                return NotFound(new ApiResponse<string> { Success = false, Message = "Employee not found", Error = "No employee with given ID" });

            return Ok(new ApiResponse<Employee> { Data = result });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Employee> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(new ApiResponse<string> { Success = false, Message = "Invalid patch data", Error = "Patch document is null" });

            var result = await _employeeService.PatchAsync(id, patchDoc);
            if (result == null)
                return NotFound(new ApiResponse<string> { Success = false, Message = "Employee not found", Error = "No employee with given ID" });

            return Ok(new ApiResponse<Employee> { Data = result });
        }

    }
}
