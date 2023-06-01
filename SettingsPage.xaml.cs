using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SQLite;
using System;
using System.IO;
using System.Linq;

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

        private void InitializeDatabase()
        {
            // Get the path to the database file
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "settings.db");

            // Create the database connection
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<UserSet>().Wait();
        }

        private async void InitializeUserSettings()
        {
            // Check if the user settings already exist in the database
            var existingSettings = await _database.Table<UserSet>().FirstOrDefaultAsync();
            if (existingSettings != null)
            {
                NameEntry.Text = existingSettings.Name;
                AgeEntry.Text = existingSettings.Age.ToString();

                if (existingSettings.lightOrDark)
                {
                    togTheme.IsToggled = true;
                }
                else
                {
                    togTheme.IsToggled = false;
                }

                var currentTheme = existingSettings.lightOrDark;
                if (currentTheme)
                    Application.Current.UserAppTheme = AppTheme.Dark;
                else
                    Application.Current.UserAppTheme = AppTheme.Light;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            int age = 0;
            int.TryParse(AgeEntry.Text, out age);
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

        private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
        {
            bool isDarkTheme = e.Value;
            Preferences.Set("DarkThemeOn", isDarkTheme ? "Dark" : "Light");

            // Apply the theme
            if (isDarkTheme)
                Application.Current.UserAppTheme = AppTheme.Dark;
            else
                Application.Current.UserAppTheme = AppTheme.Light;
        }
    }
}
