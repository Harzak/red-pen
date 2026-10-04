
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

IServiceCollection services = new ServiceCollection();
services.AddCoreDependencies();
services.ConfigureCoreDependencies(configuration);

using ServiceProvider serviceProvider = services.BuildServiceProvider();

ICorrectionService correctionService = serviceProvider.GetRequiredService<ICorrectionService>();


string inputText = Console.ReadLine() ?? string.Empty;

string corrected = await correctionService.CorrectAsync(inputText).ConfigureAwait(false);




Console.WriteLine(corrected);
