using Microsoft.Extensions.Logging;

namespace ROI_app;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Create a builder for the Maui app
        var builder = MauiApp.CreateBuilder();

        // Configure the Maui app
        builder
            .UseMauiApp<App>() // Specify the App class as the entry point for the Maui app
            .ConfigureFonts(fonts =>
            {
                // Add custom fonts to the font collection
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        // Enable debug logging when in DEBUG mode
        builder.Logging.AddDebug();
#endif

        // Build and return the Maui app
        return builder.Build();
    }
}
