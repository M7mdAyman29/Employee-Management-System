    using EMS.Application.Common.Responses;
    using EMS.Application.DTO.Empolyee;
    using EMS.Application.Sevices.Interfaces;
    using EMS.Domain.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

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

            [Authorize(Policy = "HROrAdmin")]
            [HttpPost]
            public async Task<IActionResult> AddEmployee(AddEmployeeDTO addEmployeeDTO)
            {
                _logger.LogInformation("Adding employee: {Name}", addEmployeeDTO.Name);

                var result = await _employeeService.AddEmployeeAsync(addEmployeeDTO);
                return MapResponse(result);
            }

            [Authorize(Policy = "HROrAdmin")]
            [HttpGet]
            public async Task<IActionResult> GetEmployees()
            {
                _logger.LogInformation("Getting all employees");

                var result = await _employeeService.GetEmployeesListAsync();
                return MapResponse(result);
            }


            [Authorize(Policy = "HROrAdmin")]
            [HttpGet("paged")]
            public async Task<IActionResult> GetEmployeesInPage([FromQuery] EmployeeQuery query )
            {
                _logger.LogInformation("Getting all employees");

                var result = await _employeeService.GetPaged(query);
                return MapResponse(result);
            }

        [Authorize(Policy = "HROrAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;

            if (role == "Employee")
            {
                if (!int.TryParse(employeeIdClaim, out int empId))
                    return Unauthorized();

                if (empId != id)
                    return Forbid();
            }
            _logger.LogInformation("Getting employee by id: {Id}", id);
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            return MapResponse(result);
        }
            [Authorize(Policy = "HROrAdmin")]
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDTO updateEmployeeDTO)
            {
                _logger.LogInformation("Updating employee: {Id}", id);

                var result = await _employeeService.EditEmployeeAsync(id, updateEmployeeDTO);
                return MapResponse(result);
            }

            [Authorize(Policy = "HROrAdmin")]
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEmployee(int id)
            {
                _logger.LogInformation("Deleting employee: {Id}", id);

                var result = await _employeeService.DeleteEmployeeAsync(id);
                return MapResponse(result);
            }

            [Authorize]
            [HttpGet("me")]
            public async Task<IActionResult> GetMyData()
            {
                var employeeIdClaim = User.FindFirst("EmployeeId");

                if (employeeIdClaim == null)
                    return Unauthorized("EmployeeId not found in token");

                if (!int.TryParse(employeeIdClaim.Value, out int employeeId))
                    return Unauthorized("Invalid EmployeeId");

                _logger.LogInformation("Getting data for employee: {EmployeeId}", employeeId);

                var result = await _employeeService.GetEmployeeByIdAsync(employeeId);

                return MapResponse(result);
            }
        }
    }