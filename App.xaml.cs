using Microsoft.Maui.Controls;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace ROI_app
{
    public partial class App : Application
    {
        private SQLiteAsyncConnection _database; // Declare _database at the class level

        public App()
        {
            InitializeComponent();

            // Set the MainPage of the application to an instance of the AppShell
            MainPage = new AppShell();

            // Initialize the database
            InitializeDatabase();

            // Initialize the SQLite database connection
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "settings.db");
            _database = new SQLiteAsyncConnection(databasePath);
            //_database.CreateTableAsync<UserSet>().Wait();

            // Load the user settings
            LoadUserSettings();
        }

        private async void LoadUserSettings()
        {
            // Check if the user settings already exist in the database
            var existingSettings = await _database.Table<UserSet>().FirstOrDefaultAsync();
            if (existingSettings != null)
            {
                var currentTheme = existingSettings.lightOrDark;
                if (currentTheme)
                    Application.Current.UserAppTheme = AppTheme.Dark;
                else
                    Application.Current.UserAppTheme = AppTheme.Light;
            }
        }

        private void InitializeDatabase()
        {
            EmployeeDbContext dbContext = new EmployeeDbContext();
            dbContext.InitializeDatabaseAsync().Wait();
        }
    }

    public class UserSet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool lightOrDark { get; set; }
    }
}
