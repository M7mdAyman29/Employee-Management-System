using EMS.Application.Common.Responses;
using EMS.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Sevices.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> Register(RegisterDTO dto);
        Task<ApiResponse> Login(LoginDTO dto);
    }
}
