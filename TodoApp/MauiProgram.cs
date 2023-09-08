using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using TodoApp.Repository;

namespace TodoApp;

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
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddMudServices();

        // Statements for adding PersonRepository as a singleton
        string dbPath = FileAccessHelper.GetLocalFilePath("todos.db3");
        builder.Services.AddSingleton<TodoRepository>(s => ActivatorUtilities.CreateInstance<TodoRepository>(s, dbPath));
        dbPath = FileAccessHelper.GetLocalFilePath("Dailynotes.db3");
        builder.Services.AddSingleton<DailynoteRepositosy>(s => ActivatorUtilities.CreateInstance<DailynoteRepositosy>(s, dbPath));

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
