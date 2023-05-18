using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ROI_app
{
    // Represents the UpdateEmployees content page
    public partial class UpdateEmployees : ContentPage
    {
        public UpdateEmployees()
        {
            InitializeComponent();
            UpdateButton.Clicked += OnUpdateButtonClicked;
        }

        // Event handler for the Home button click
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        // Event handler for the Update button click
        private void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string employeeID = EmployeeID.Text;

            // Check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(employeeID))
            {
                DisplayAlert("Error", "Sorry, you missed one or more fields. Please don't include spaces.", "OK");
            }
            else
            {
                // Create an instance of the Employee class
                Employee employee = new Employee(firstName, lastName, employeeID);

                // Process the employee data (update the employee record in the database)
                ProcessEmployeeData(employee);

                // Display a success message
                DisplayAlert("Success", "Thank you, the employee has been updated.", "OK");

                // Clear the text entries
                FirstName.Text = string.Empty;
                LastName.Text = string.Empty;
                EmployeeID.Text = string.Empty;
            }
        }

        // Method to process the employee data into SQLite
        private void ProcessEmployeeData(Employee employee)
        {
            // TODO: Implement SQLite operations

        }
    }

    // Employee class definition
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID { get; set; }

        public Employee(string firstName, string lastName, string employeeID)
        {
            FirstName = firstName;
            LastName = lastName;
            EmployeeID = employeeID;
        }
    }
}
