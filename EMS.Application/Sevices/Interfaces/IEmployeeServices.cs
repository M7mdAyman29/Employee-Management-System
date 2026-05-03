using EMS.Application.Common.Responses;
using EMS.Application.DTO.Empolyee;
using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Sevices.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApiResponse> GetEmployeesListAsync();

        Task<ApiResponse> GetPaged(EmployeeQuery query);
        Task<ApiResponse> GetEmployeeByIdAsync(int id);

        Task<ApiResponse> AddEmployeeAsync(AddEmployeeDTO dto);

        Task<ApiResponse> EditEmployeeAsync(int id, UpdateEmployeeDTO dto);

        Task<ApiResponse> DeleteEmployeeAsync(int id);
    }
}
