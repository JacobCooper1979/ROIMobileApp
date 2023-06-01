namespace ROIMobileApp;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}
    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // Navigate back to the MainPage by popping to the root page
        await Navigation.PopToRootAsync();
    }
}