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
        public static event EventHandler<bool> ThemeChanged;

        // Initialize settings page
        public SettingsPage()
        {
            InitializeComponent();
            InitializeSettingsAsync();
        }

        private async void InitializeSettingsAsync()
        {
            await InitializeDatabase();
            await InitializeUserSettings();
        }

        private async Task InitializeDatabase()
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
                NameEntry.Text = existingSettings.name;
                AgeEntry.Text = existingSettings.age.ToString();
                togTheme.IsToggled = existingSettings.lightOrDark;
                var currentTheme = existingSettings.lightOrDark ? AppTheme.Dark : AppTheme.Light;
                Application.Current.UserAppTheme = currentTheme;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            bool parsingSuccess = int.TryParse(AgeEntry.Text, out int age);

            if (parsingSuccess)
            {
                bool theme = togTheme.IsToggled;
                var userSettings = new UserSet
                {
                    name = name,
                    age = age,
                    lightOrDark = theme
                };

                // Save user settings to the database
                var existingSettings = await _database.Table<UserSet>().FirstOrDefaultAsync();
                if (existingSettings != null)
                {
                    userSettings.id = existingSettings.id; // Set the ID to update existing settings
                    await _database.UpdateAsync(userSettings);
                }
                else
                {
                    await _database.InsertAsync(userSettings);
                }

                // Show a confirmation message
                await DisplayAlert("Success", "User settings saved", "OK");
            }
            else
            {
                // Show an error message
                await DisplayAlert("Error", "Invalid age input", "OK");
            }
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the MainPage
            await Navigation.PopToRootAsync();
        }

        private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
        {
            bool isDarkTheme = e.Value;

            // Raise the ThemeChanged event
            ThemeChanged?.Invoke(this, isDarkTheme);

            // Update the background image based on the theme
            ChangeBackgroundImage(isDarkTheme);
        }

        private void ChangeBackgroundImage(bool isDarkTheme)
        {
            string imagePath;
            if (isDarkTheme)
            {
                // Dark theme selected
                imagePath = "black_texture_image.png";
            }
            else
            {
                // Light theme selected
                imagePath = "burnt_orange_texture_image.png";
            }

            var imageSource = ImageSource.FromFile(imagePath);
            BackgroundImage.Source = imageSource;
        }
    }
}

