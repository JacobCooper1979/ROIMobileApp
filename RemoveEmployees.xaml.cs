using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace ROI_app
{
    //RemoveEmployees content page
    public partial class RemoveEmployees : ContentPage
    {
        public RemoveEmployees()
        {
            InitializeComponent();
        }

        // Event handler for the home button
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

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
                    await DisplayAlert("Success", $"Employee with ID {employeeId} has been deleted.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Employee not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please enter an employee ID.", "OK");
            }
        }
    }
}



