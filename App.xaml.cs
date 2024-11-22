namespace ROI_app;
using ROI_app;
using SQLite;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Set the MainPage of the application to an instance of the AppShell
        MainPage = new AppShell();

        // Initialize the database
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        EmployeeDbContext dbContext = new EmployeeDbContext();
    }
}
