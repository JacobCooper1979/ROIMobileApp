using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using SQLite;

namespace ROI_app
{
    // UpdateEmployees content page
    public partial class UpdateEmployees : ContentPage
    {
        private EmployeeRepository _employeeRepository;

        public UpdateEmployees()
        {
            InitializeComponent();
            UpdateButton.Clicked += OnUpdateButtonClicked;
            _employeeRepository = new EmployeeRepository();
        }

        // Event handler for the home button 
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        // Event handler for the update button 
        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string employeeID = EmployeeID.Text;

            // Check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(employeeID))
            {
                await DisplayAlert("Error", "Sorry, you missed one or more fields. Please don't include spaces.", "OK");
            }
            else
            {
                // Create an instance of the Employee class
                Employee employee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmployeeID = employeeID
                };

                // Process the employee data to the database
                await ProcessEmployee(employee);

                // Display a success message
                await DisplayAlert("Success", "Thank you, the employee has been updated.", "OK");

                // Clear the text entries
                FirstName.Text = string.Empty;
                LastName.Text = string.Empty;
                EmployeeID.Text = string.Empty;
            }
        }

        //process the employee data into SQLite
        private async Task ProcessEmployee(Employee employee)
        {
            // update the employee record in the database
            await  _employeeRepository.SaveEmployeeAsync(employee);
        }
    }
}
