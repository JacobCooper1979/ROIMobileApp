using Microsoft.Maui.Controls;

namespace ROI_app
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnUpdateEmployeesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateEmployees());
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the MainPage by popping to the root page
            await Navigation.PopToRootAsync();
        }

    }
}
