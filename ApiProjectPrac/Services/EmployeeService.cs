using ApiProjectPrac.Data;
using ApiProjectPrac.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ApiProjectPrac.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(int id, Employee employee)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null) return null;

            existing.Name = employee.Name;
            existing.Department = employee.Department;
            existing.Salary = employee.Salary;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Employee> PatchAsync(int id, JsonPatchDocument<Employee> patchDoc)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            patchDoc.ApplyTo(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
