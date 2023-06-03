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

        //Initialize settings page
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
                NameEntry.Text = existingSettings.Name;
                AgeEntry.Text = existingSettings.Age.ToString();
                togTheme.IsToggled = existingSettings.lightOrDark;
                var currentTheme = existingSettings.lightOrDark ? AppTheme.Dark : AppTheme.Light;
                Application.Current.UserAppTheme = currentTheme;
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            bool parsingSuccess = int.TryParse(AgeEntry.Text, out int age);

            if (parsingSuccess)
            {
                bool theme = togTheme.IsToggled;
                var userSettings = new UserSet
                {
                    Name = name,
                    Age = age,
                    lightOrDark = theme
                };

                _database.InsertOrReplaceAsync(userSettings);
                // Show a confirmation message
                DisplayAlert("Success", "User settings saved", "OK");
            }
            else
            {
                // Age parsing failed, show an error message or handle the failure
                DisplayAlert("Error", "Invalid age input", "OK");
            }
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



        //not working
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double newValue = e.NewValue;
            //SetTheme(newValue);
        }

        private static void SetTheme(double brightness)
        {
            // Adjust the theme brightness here
            Color lightThemeColor = Color.FromHex("#FFFFFFFF");  // Light theme color (white)
            Color darkThemeColor = Color.FromHex("#FF000000");   // Dark theme color (black)

            Color newThemeColor;
            if (brightness < 0.5)
            {
                // Brightness is low use dark theme
                newThemeColor = darkThemeColor;
            }
            else
            {
                // Brightness is high use light theme
                newThemeColor = lightThemeColor;
            }

            // Apply the new theme color to the app
            Application.Current.Resources["MyAppThemeColor"] = newThemeColor;
        }

        private void ChangeBackgroundImage()
        {
            // Get the current theme
            var currentTheme = Application.Current.RequestedTheme;

            // Set the background image based on the theme
            string imagePath;
            /*if (currentTheme == AppTheme.Dark)
            {
                // Set the dark theme background image
                imagePath = "black_texture_image.png";
            }
            else
            {
                // Set the light theme background image
                imagePath = "burnt_orange_texture_image.png";
            }*/

            // Update the background image
            //ImageBackground.Source = imagePath;
            }
        }
    }
