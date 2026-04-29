using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using EMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Infrastructure.UnitOfWork.Interfaces;

namespace EMS.Infrastructure.UnitofWork.Implementations
{
        public class UnitOfWork : IUnitOfWork
        {
            private readonly AppDbContext _dbContext;
            public UnitOfWork(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public Task SaveChangesAsync()
            {
                return _dbContext.SaveChangesAsync();
            }
        }
    
}
