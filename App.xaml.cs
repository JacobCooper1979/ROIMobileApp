using Microsoft.Maui.Controls;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace ROI_app
{
    public partial class App : Application
    {
        private readonly SQLiteAsyncConnection _database; // Declare _database at the class level

        public App()
        {
            InitializeComponent();

            // Set the MainPage of the application to an instance of the AppShell
            MainPage = new AppShell();

            // Initialize the SQLite database connection
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "settings.db");
            _database = new SQLiteAsyncConnection(databasePath);

            // Initialize the database and load user settings
            InitializeDatabase();
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

        private async void InitializeDatabase()
        {
            // Create the necessary table in the SQLite database
            await _database.CreateTableAsync<UserSet>();

            // Create an initial user settings entry if it doesn't exist
            var existingSettings = await _database.Table<UserSet>().FirstOrDefaultAsync();
            if (existingSettings == null)
            {
                var userSet = new UserSet
                {
                   /* Name = "John Doe",
                    Age = 25,*/
                    lightOrDark = false // Assuming light theme by default
                };

                await _database.InsertAsync(userSet);
            }
        }
    }

    public class UserSet
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public bool lightOrDark { get; set; }
    }
}
