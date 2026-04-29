using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DTO.Empolyee
{
    public class AddEmployeeDTO
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
}
