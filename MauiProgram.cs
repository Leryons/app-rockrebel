namespace RockRebel;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        // Services
        builder.Services.AddSingleton<Database>();
        builder.Services.AddTransient<SongServices>();

        // ViewModels
        builder.Services.AddTransient<SongViewModel>();

        // Pages
        builder.Services.AddSingleton<HomePage>();

        return builder.Build();
    }
}
