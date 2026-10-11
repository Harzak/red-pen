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

ICorrectionService correctionService = serviceProvider.GetRequiredService<ICorrectionService>();

string inputText = Console.ReadLine() ?? string.Empty;

TextCorrectionResult corrected = await correctionService.CorrectAsync(inputText, ECorrectionMode.GrammarAndSpelling, EResponseMode.Concise).ConfigureAwait(false);

Console.WriteLine(corrected);
