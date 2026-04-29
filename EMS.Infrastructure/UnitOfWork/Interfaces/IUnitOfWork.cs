using EMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}