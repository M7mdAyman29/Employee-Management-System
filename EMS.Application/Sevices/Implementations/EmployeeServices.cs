using AutoMapper;
using EMS.Application.Common.Responses;
using EMS.Application.DTO.Empolyee;
using EMS.Application.Sevices.Interfaces;
using EMS.Domain.Entities;
using EMS.Domain.Enums;
using EMS.Infrastructure.Repositry.Interface;
using EMS.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository repo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET ALL
        public async Task<ApiResponse> GetEmployeesListAsync()
        {
            var employees = await _repo.GetAllAsync();
            var result = _mapper.Map<List<EmployeeDTO>>(employees);

            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Message = "Data retrieved successfully",
                Data = result
            };
        }

        // GET BY ID
        public async Task<ApiResponse> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
            {
                return new ApiResponse
                {
                    StatusCode = AppStatusCodesEnum.NotFound,
                    Message = "Employee not found"
                };
            }

            var result = _mapper.Map<EmployeeDTO>(employee);

            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Data = result
            };
        }

        // CREATE
        public async Task<ApiResponse> AddEmployeeAsync(AddEmployeeDTO dto)
        {
            if (dto == null)
            {
                return new ApiResponse
                {
                    StatusCode = AppStatusCodesEnum.InvalidData,
                    Message = "Invalid data"
                };
            }

            var employee = _mapper.Map<Employee>(dto);

            await _repo.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            var fullEmployee = await _repo.GetByIdAsync(employee.Id);


            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Message = "Employee added successfully",
                Data = _mapper.Map<EmployeeDTO>(employee)
            };
        }

        // UPDATE
        public async Task<ApiResponse> EditEmployeeAsync(int id, UpdateEmployeeDTO dto)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
            {
                return new ApiResponse
                {
                    StatusCode = AppStatusCodesEnum.NotFound,
                    Message = "Employee not found"
                };
            }

            _mapper.Map(dto, employee);

            _repo.Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Message = "Employee updated successfully",
                Data = _mapper.Map<EmployeeDTO>(employee)
            };
        }

        // DELETE
        public async Task<ApiResponse> DeleteEmployeeAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
            {
                return new ApiResponse
                {
                    StatusCode = AppStatusCodesEnum.NotFound,
                    Message = "Employee not found"
                };
            }

            _repo.Delete(employee);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Message = "Employee deleted successfully"
            };
        }
    }
}