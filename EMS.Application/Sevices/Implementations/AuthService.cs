using EMS.Application.Common.Helpers;
using EMS.Application.Common.Responses;
using EMS.Application.DTO.User;
using EMS.Application.Sevices.Interfaces;
using EMS.Domain.Entities;
using EMS.Domain.Enums;
using EMS.Infrastructure.Repository.Interfaces;
using EMS.Infrastructure.Repositry.Interface;
using EMS.Infrastructure.UnitOfWork.Interfaces;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Sevices.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwt;
        public AuthService(IUserRepository userRepo, IEmployeeRepository employeeRepo, IUnitOfWork unitOfWork, IJwtTokenGenerator jwt)
        {
            _userRepo = userRepo;
            _employeeRepo = employeeRepo;
            _unitOfWork = unitOfWork;
            _jwt = jwt;
        }

        public async Task<ApiResponse> Register(RegisterDTO dto)
        {
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                return new ApiResponse
                {
                    StatusCode = AppStatusCodesEnum.InvalidData,
                    Message = "Email already exists"
                };
            }
            var employee = await _employeeRepo.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
            {
                return new ApiResponse
                {
                    StatusCode = AppStatusCodesEnum.NotFound,
                    Message = "Employee not found"
                };
            }
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                EmployeeId = dto.EmployeeId
            };

            await _userRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Message = "User registered successfully"
            };
        }

        public async Task<ApiResponse> Login(LoginDTO dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null || user.PasswordHash != dto.Password)
            {
                return new ApiResponse
                {
                    StatusCode = AppStatusCodesEnum.Unauthorized,
                    Message = "Invalid email or password"
                };
            }
            var token = _jwt.GenerateToken(user);
            return new ApiResponse
            {
                StatusCode = AppStatusCodesEnum.Success,
                Data = token
            };
        }
    }
}
