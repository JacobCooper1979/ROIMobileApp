namespace ROI_app;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Set the MainPage of the application to an instance of the AppShell
        MainPage = new AppShell();
    }
}
