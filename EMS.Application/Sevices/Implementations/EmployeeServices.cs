using AutoMapper;
using EMS.Application.Common.Helpers;
using EMS.Application.Common.Pagination;
using EMS.Application.Common.Responses;
using EMS.Application.DTO.Empolyee;
using EMS.Application.Sevices.Interfaces;
using EMS.Domain.Entities;
using EMS.Domain.Enums;
using EMS.Infrastructure.Repositry.Interface;
using EMS.Infrastructure.UnitOfWork.Interfaces;
using FluentNHibernate.Conventions;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var employees =  _repo.GetAllAsync();
            var result = _mapper.Map<List<EmployeeDTO>>(employees);

            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Message = "Data retrieved successfully",
                Data = result
            };
        }


        // Get Paged

        // GET ALL
        public async Task<ApiResponse> GetPaged(EmployeeQuery query)
        {
            //  Base query
            var employees = _repo.GetPaged();

            //  Filter first (best performance)
            if (query.DepartmentId.HasValue)
                employees = employees.Where(e => e.DepartmentId == query.DepartmentId);

            //  Search
            if (!string.IsNullOrEmpty(query.Search))
                employees = employees.Where(e => e.Name.Contains(query.Search));

            //  Sorting
            employees = SortHepler.ApplySorting(employees, query.SortBy, query.IsAsc ?? true);

            //  Pagination
            var pagedData = await PaginationHelper.CreateAsync(
                employees,
                query.Page,
                query.PageSize
            );

            var result = _mapper.Map<List<EmployeeDTO>>(pagedData.Data);

            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Message = "Data retrieved successfully",
                Data = new
                {
                    pagedData.TotalCount,
                    pagedData.Page,
                    pagedData.PageSize,
                    Data = result
                }
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

            var fullEmployee = _mapper.Map<EmployeeDTO>(employee);


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