namespace redpen.demo.desktop.Configurations;

internal static class DesktopServiceCollectionExtension
{
    public static IServiceCollection AddDesktopDependencies(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ILocalizer, ResourceLocalizer>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<TextEditorViewModel>();

        services.AddSingleton<MainWindow>();

        return services;
    }
}