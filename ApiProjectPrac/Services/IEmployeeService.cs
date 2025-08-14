using ApiProjectPrac.Models;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiProjectPrac.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<Employee> UpdateAsync(int id, Employee employee);
        Task<Employee> PatchAsync(int id, JsonPatchDocument<Employee> patchDoc);

    }
}
