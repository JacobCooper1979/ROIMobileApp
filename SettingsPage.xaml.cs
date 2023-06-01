namespace ROI_app;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}
    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // Navigate back to the MainPage by popping to the root page
        await Navigation.PopToRootAsync();
    }
}