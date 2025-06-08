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

<<<<<<< HEAD
        private async Task<List<EmployeeModel>> FetchEmployeeListAsync()
=======
        //To do : add a clear naming conventions
        public async Task<List<EmployeeModel>> GetEmployeeDetails()
>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
        {
            List<EmployeeModel> result = new List<EmployeeModel>();

            try
            {
<<<<<<< HEAD
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
=======
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
>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
                Console.WriteLine(ex.Message);
            }
            return result;
        }
<<<<<<< HEAD

        public async Task<IQueryable<EmployeeModel>> GetEmployeeDetails()
        {
            var list = await FetchEmployeeListAsync();
            return list.AsQueryable();
        }

=======
        
>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
        public async Task<EmployeeModel> GetEmployeeDetailsById(string? id, string? partitionKey)
        {
            try
            {
                ItemResponse<EmployeeModel> response = await _container.ReadItemAsync<EmployeeModel>(id, new PartitionKey(partitionKey));
                return response.Resource;
<<<<<<< HEAD
=======

>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
<<<<<<< HEAD
=======

>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
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
<<<<<<< HEAD

                var updateRes = await _container.ReplaceItemAsync(existingItem, existingItem.Id, new PartitionKey(existingItem.Department));
=======
                
                var updateRes = await _container.ReplaceItemAsync(existingItem, existingItem.Id, new PartitionKey(existingItem.Department));

>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
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
<<<<<<< HEAD
=======
                // find a exception handeling : throw | throw new exceptions
>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
                throw new Exception("Exception", ex);
            }
        }
    }
}
