using Microsoft.Maui.Controls;

namespace ROI_app
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the MainPage by popping to the root page
            await Navigation.PopToRootAsync();
        }

        //Update Employees
        private async void OnUpdateEmployeesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateEmployees());
        }

        //Remove Employees
        private async void OnRemoveEmployeesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RemoveEmployees());
        }

        //View Employees
        private async void OnViewEmployeesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewEmployees());
        }

    }
}
