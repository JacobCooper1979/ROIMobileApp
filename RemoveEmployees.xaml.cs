using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ROI_app
{
    //RemoveEmployees content page
    public partial class RemoveEmployees : ContentPage
    {
        public RemoveEmployees()
        {
            InitializeComponent();
        }

        // Event handler for the Home button click
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        // Event handler for the Delete button click
        /*private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            string employeeName = TextInput.Text;

            // Check if the entered employee name is not empty
            if (!string.IsNullOrWhiteSpace(employeeName))
            {
                // Perform your processing logic here using the employeeName variable
                // For example, you can display a confirmation message or perform a deletion operation
                DisplayAlert("Delete Employee", $"Deleting employee: {employeeName}", "OK");
            }
            else
            {
                DisplayAlert("Error", "Please enter an employee name.", "OK");
            }
        }*/
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            string employeeId = TextInput.Text;

            // Check if the entered employee ID is not empty
            if (!string.IsNullOrWhiteSpace(employeeId))
            {
                EmployeeRepository repository = new EmployeeRepository();
                List<Employee> employees = await repository.GetEmployeesAsync();

                // Find the employee with the matching ID
                Employee employee = employees.FirstOrDefault(emp =>
                    string.Equals(emp.EmployeeID, employeeId, StringComparison.OrdinalIgnoreCase));

                if (employee != null)
                {
                    // Delete the employee from the database
                    await repository.DeleteEmployeeAsync(employee);
                    DisplayAlert("Success", $"Employee with ID {employeeId} has been deleted.", "OK");
                }
                else
                {
                    DisplayAlert("Error", "Employee not found.", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Please enter an employee ID.", "OK");
            }
        }

    }


}
