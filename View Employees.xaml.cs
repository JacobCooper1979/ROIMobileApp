using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ROI_app
{
    // Represents the ROI_app.ViewEmployees content page
    public partial class ViewEmployees : ContentPage

    {
        public ViewEmployees()
        {
            InitializeComponent();
        }

        // Event handler for the Home button click
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}
