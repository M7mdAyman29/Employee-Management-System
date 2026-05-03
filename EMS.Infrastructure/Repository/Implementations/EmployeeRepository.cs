using EMS.Domain.Entities;
using EMS.Infrastructure.Data;
using EMS.Infrastructure.Repositry.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Repositry.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Include(e=>e.Department).
                Include(e=>e.Role)
                .ToListAsync();
        }

        public IQueryable<Employee> GetPaged()
        {
            return _dbContext.Employees
                .Include(e => e.Department).
                Include(e => e.Role)
                .AsQueryable();
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            return _dbContext.Employees.Include(e => e.Department).
                Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == id);
        }   

        public async Task AddAsync(Employee employee)
        {
          
            await _dbContext.Employees.AddAsync(employee);
        }

        public void Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
        }

        public void Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
        }
    }

}
