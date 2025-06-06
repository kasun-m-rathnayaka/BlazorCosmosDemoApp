using Newtonsoft.Json;

namespace BlazorCosmosDemoApp3.Models
{
    public class EmployeeModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Name { get; set; }
        //to do : proper naming conventions
        [JsonProperty("dep")]
        public string Department { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }
    }
}
