namespace redpen.core.Configurations;

/// <summary>
/// Provides extension methods for registering core-layer service dependencies with the dependency injection container.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds core-layer service dependencies to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ICorrectionService, CorrectionService>();
        services.AddSingleton<IChatClientFactory, ChatClientFactory>();

        return services;
    }

    public static IServiceCollection ConfigureCoreDependencies(this IServiceCollection services, IConfiguration configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        services.Configure<ChatClientOptions>(configureOptions.GetSection("ChatClient").Bind);

        return services;
    }
}
