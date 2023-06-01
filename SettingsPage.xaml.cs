using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ROI_app
{
    public partial class SettingsPage : ContentPage
    {
        private SQLiteAsyncConnection _database;

        public SettingsPage()
        {
            InitializeComponent();
            InitializeDatabase();
            InitializeUserSettings();
        }

        private async void InitializeDatabase()
        {
            // Get the path to the database file
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "settings.db");

            // Create the database connection
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<UserSet>();
        }

        private async Task InitializeUserSettings()
        {
            // Check if the user settings already exist in the database
            var existingSettings = await _database.Table<UserSet>().FirstOrDefaultAsync();
            if (existingSettings != null)
            {
                NameEntry.Text = existingSettings.Name;
                AgeEntry.Text = existingSettings.Age.ToString();

                togTheme.IsToggled = existingSettings.lightOrDark;

                var currentTheme = existingSettings.lightOrDark ? AppTheme.Dark : AppTheme.Light;
                Application.Current.UserAppTheme = currentTheme;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            int.TryParse(AgeEntry.Text, out int age);
            bool theme = togTheme.IsToggled;

            var userSettings = new UserSet
            {
                Name = name,
                Age = age,
                lightOrDark = theme
            };

            await _database.InsertOrReplaceAsync(userSettings);

            // Show a confirmation message
            await DisplayAlert("Success", "User settings saved", "OK");
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the MainPage by popping to the root page
            await Navigation.PopToRootAsync();
        }

        // Sets to dark theme vs light theme
        private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
        {
            bool isDarkTheme = e.Value;
            Preferences.Set("DarkThemeOn", isDarkTheme ? "Dark" : "Light");

            // Apply the theme
            var currentTheme = isDarkTheme ? AppTheme.Dark : AppTheme.Light;
            Application.Current.UserAppTheme = currentTheme;
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double newValue = e.NewValue;
            SetThemeBrightness(newValue);
        }

        private void SetThemeBrightness(double brightness)
        {
            // Adjust the theme brightness here
            // Example: Update the theme based on the provided brightness value
            var currentTheme = Application.Current.RequestedTheme;
            var newTheme = currentTheme; // Replace this with your custom theme adjustment logic
            Application.Current.UserAppTheme = newTheme;
        }
    }
}
