using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ROI_app
{
    // Represents the RemoveEmployees content page
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
        private void OnDeleteButtonClicked(object sender, EventArgs e)
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
        }

    }


}
