<<<<<<< HEAD
//using BlazorCosmosDemoApp3.Models;
//using Microsoft.AspNetCore.Components;

//namespace BlazorCosmosDemoApp3.Components.Pages
//{
//    public partial class Employee
//    {
//        [SupplyParameterFromForm]
//        private EmployeeModel employee { get; set; } = new();
//        private bool submitted = false;

//        [Parameter]
//        public List<EmployeeModel>? employees { get; set; }
//        FetchData fetch = new FetchData();

//        private async Task HandleValidSubmit()
//        {
//            employee.Id = Guid.NewGuid().ToString();
//            await employeeService.AddEmployee(employee);
//            submitted = true;
//            employees = await employeeService.GetEmployeeDetails();
//        }
//    }
//}
=======
using BlazorCosmosDemoApp3.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorCosmosDemoApp3.Components.Pages
{
    public partial class Employee
    {
        [SupplyParameterFromForm]
        private EmployeeModel employee { get; set; } = new();
        private bool submitted = false;

        [Parameter]
        public List<EmployeeModel>? employees { get; set; }
        FetchData fetch = new FetchData();

        private async Task HandleValidSubmit()
        {
            employee.Id = Guid.NewGuid().ToString();
            await employeeService.AddEmployee(employee);
            submitted = true;
            employees = await employeeService.GetEmployeeDetails();
        }
    }
}
>>>>>>> 2012f79270021d5f4a386a86a6d4032932060bc7
