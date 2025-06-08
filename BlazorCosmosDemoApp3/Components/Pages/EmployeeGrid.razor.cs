using BlazorCosmosDemoApp3.Models;
using Radzen;
using Radzen.Blazor;
using System.Linq.Dynamic.Core;

namespace BlazorCosmosDemoApp3.Components.Pages
{
    public partial class EmployeeGrid
    {
        RadzenDataGrid<EmployeeModel> grid;
        int count;
        IEnumerable<EmployeeModel> employees;
        bool isLoading = false;

        async Task Reset()
        {
            grid.Reset(true);
            await grid.FirstPage(true);
        }

        async Task LoadData(LoadDataArgs args)
        {
            isLoading = true;
            await Task.Yield();

            var query = (await employeeService.GetEmployeeDetails()).AsQueryable();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                query = query.Where(args.Filter);
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                query = query.OrderBy(args.OrderBy);
            }

            count = query.Count();

            employees = query.Skip(args.Skip ?? 0).Take(args.Top ?? 5).ToList();

            isLoading = false;
            StateHasChanged();
        }

        async Task OnUpdateRow(EmployeeModel employee)
        {
            await employeeService.UpdateEmployee(employee);
            await grid.Reload();
        }

        async Task OnCreateRow(EmployeeModel employee)
        {
            await employeeService.AddEmployee(employee);
            await grid.Reload();
        }

        async Task InsertRow()
        {
            await grid.InsertRow(new EmployeeModel());
        }


    }
}