using System.Globalization;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

IServiceCollection services = new ServiceCollection();
services.AddLogging(builder => builder
    .AddConsole()
    .SetMinimumLevel(LogLevel.Debug));
services.AddCoreDependencies();
services.ConfigureCoreDependencies(configuration);

using ServiceProvider serviceProvider = services.BuildServiceProvider();

ITextCorrectionService correctionService = serviceProvider.GetRequiredService<ITextCorrectionService>();

string inputText = Console.ReadLine() ?? string.Empty;

TextCorrectionResult result = await correctionService.CorrectAsync(inputText, new TextCorrectionOptions()
{
    CorrectionMode = ETextCorrectionMode.GrammarAndSpelling,
    ResponseMode = ETextResponseMode.Concise,
    TextLanguage = new CultureInfo("fr-FR"),
}).ConfigureAwait(false);

Console.WriteLine(result.IsSuccess ? result.CorrectedText : result.ErrorMessage);
Console.WriteLine($"Total Duration: {result.TotalDuration}");
Console.WriteLine($"Total Token Usage: {result.TotalTokenUsage}");
