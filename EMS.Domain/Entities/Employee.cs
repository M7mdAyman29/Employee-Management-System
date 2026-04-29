using EMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entities
{
       public class Employee : BaseEntity
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public int DepartmentId { get; set; }
            public Department? Department { get; set; }
            public int RoleId { get; set; }
            public Role? Role { get; set; }
           public User? User { get; set; }
    }
}
