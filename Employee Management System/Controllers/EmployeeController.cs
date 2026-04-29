using EMS.Application.Common.Responses;
using EMS.Application.DTO.Empolyee;
using EMS.Application.Sevices.Interfaces;
using EMS.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        private IActionResult MapResponse(ApiResponse result)
        {
            return result.StatusCode switch
            {
                AppStatusCodesEnum.Success => Ok(result),
                AppStatusCodesEnum.InvalidData => BadRequest(result),
                AppStatusCodesEnum.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO addEmployeeDTO)
        {
            _logger.LogInformation("Adding employee: {Name}", addEmployeeDTO.Name);

            var result = await _employeeService.AddEmployeeAsync(addEmployeeDTO);
            return MapResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            _logger.LogInformation("Getting all employees");

            var result = await _employeeService.GetEmployeesListAsync();
            return MapResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            _logger.LogInformation("Getting employee by id: {Id}", id);

            var result = await _employeeService.GetEmployeeByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            _logger.LogInformation("Updating employee: {Id}", id);

            var result = await _employeeService.EditEmployeeAsync(id, updateEmployeeDTO);
            return MapResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            _logger.LogInformation("Deleting employee: {Id}", id);

            var result = await _employeeService.DeleteEmployeeAsync(id);
            return MapResponse(result);
        }
    }
}