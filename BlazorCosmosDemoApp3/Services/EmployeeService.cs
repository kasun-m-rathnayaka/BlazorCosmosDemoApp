using Azure;
using BlazorCosmosDemoApp3.Components.Pages;
using BlazorCosmosDemoApp3.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDemoApp3.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Container _container;

        public EmployeeService(string conn, string key, string dataBaseName, string containerName)
        {
            var cosmosClient = new CosmosClient(conn, key, new CosmosClientOptions() { });
            _container = cosmosClient.GetContainer(dataBaseName, containerName);
        }

        public async Task AddEmployee(EmployeeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Employee model cannot be null.");
            }
            try
            {
                await _container.CreateItemAsync(model, new PartitionKey(model.Department));
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error adding employee: {ex.Message}", ex);
            }
        }

        private async Task<List<EmployeeModel>> FetchEmployeeListAsync()
        {
            List<EmployeeModel> result = new List<EmployeeModel>();

            try
            {
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<EmployeeModel> queryResultSetIterator = _container.GetItemQueryIterator<EmployeeModel>(queryDefinition);
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<EmployeeModel> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (EmployeeModel employee in currentResultSet)
                    {
                        result.Add(employee);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task<IQueryable<EmployeeModel>> GetEmployeeDetails()
        {
            var list = await FetchEmployeeListAsync();
            return list.AsQueryable();
        }

        public async Task<EmployeeModel> GetEmployeeDetailsById(string? id, string? partitionKey)
        {
            try
            {
                ItemResponse<EmployeeModel> response = await _container.ReadItemAsync<EmployeeModel>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeModel emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException(nameof(emp), "Employee model cannot be null.");
            }
            try
            {
                // Read the existing item
                ItemResponse<EmployeeModel> res = await _container.ReadItemAsync<EmployeeModel>(emp.Id, new PartitionKey(emp.Department));
                var existingItem = res.Resource;

                // Update fields
                existingItem.Name = emp.Name;
                existingItem.Email = emp.Email;
                existingItem.Salary = emp.Salary;
                existingItem.Department = emp.Department;

                // Replace the item in Cosmos DB
                await _container.ReplaceItemAsync(existingItem, existingItem.Id, new PartitionKey(existingItem.Department));
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error updating employee: {ex.Message}", ex);
            }
        }

        public async Task DeleteEmployee(EmployeeModel employee)
        {
            try
            {
                var response = await _container.DeleteItemAsync<EmployeeModel>(employee.Id, new PartitionKey(employee.Department));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception", ex);
            }
        }
    }
}
