using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DTO.Empolyee
{
    public class EmployeeQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
        public int? DepartmentId { get; set; }

        // 🔥 Sorting
        public string? SortBy { get; set; } = "Id";
        public bool? IsAsc { get; set; } = true;    
    }
}
