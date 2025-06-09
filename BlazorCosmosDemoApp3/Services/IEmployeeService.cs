using BlazorCosmosDemoApp3.Models;

namespace BlazorCosmosDemoApp3.Services
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmployeeModel model);
        Task DeleteEmployee(EmployeeModel employee);
        Task<IQueryable<EmployeeModel>> GetEmployeeDetails();
        Task<EmployeeModel> GetEmployeeDetailsById(string? id, string? partitionKey);
        Task UpdateEmployee(EmployeeModel emp);
    }
}