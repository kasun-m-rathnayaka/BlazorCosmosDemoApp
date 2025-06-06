using Azure;
using BlazorCosmosDemoApp3.Components.Pages;
using BlazorCosmosDemoApp3.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDemoApp3.Services
{
    public class EmployeeService
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
                Console.WriteLine(model.Id);
                await _container.CreateItemAsync(model, new PartitionKey(model.Department));
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error adding employee: {ex.Message}", ex);
            }
        }

        //To do : add a clear naming conventions
        public async Task<List<EmployeeModel>> GetEmployeeDetails()
        {
            List<EmployeeModel> result = new List<EmployeeModel>();

            try
            {
                // To do : find a optimum
                var sqlQuery = "SELECT*FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<EmployeeModel> quearyResSelector = _container.GetItemQueryIterator<EmployeeModel>
                    (queryDefinition);
                while (quearyResSelector.HasMoreResults)
                {
                    FeedResponse<EmployeeModel> currentResSet = await quearyResSelector.ReadNextAsync();
                    foreach (EmployeeModel employee in currentResSet)
                    {
                        result.Add(employee);
                    }

                }

            }
            catch (Exception ex)
            {
                // to do 
                Console.WriteLine(ex.Message);
            }
            return result;
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

        public async Task updateEmployeeDetails(EmployeeModel emp)
        {
            try
            {
                ItemResponse<EmployeeModel> res = await _container.ReadItemAsync<EmployeeModel>(Convert.ToString(emp.Id), new PartitionKey(emp.Department));
                var existingItem = res.Resource;

                existingItem.Name = emp.Name;
                existingItem.Email = emp.Email;
                existingItem.Salary = emp.Salary;
                
                var updateRes = await _container.ReplaceItemAsync(existingItem, existingItem.Id, new PartitionKey(existingItem.Department));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteEmployee(string? id, string? department)
        {
            try
            {
                var response = await _container.DeleteItemAsync<EmployeeModel>(id, new PartitionKey(department));
            }
            catch (Exception ex)
            {
                // find a exception handeling : throw | throw new exceptions
                throw new Exception("Exception", ex);
            }
        }
    }
}
