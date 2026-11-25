using Microsoft.Extensions.Hosting;

namespace redpen.demo.desktop;

internal sealed partial class App : Application
{
    private IHost? _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        HostApplicationBuilder builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            Args = e.Args,
            ContentRootPath = AppContext.BaseDirectory,
        });

        builder.Services.AddCoreDependencies();
        builder.Services.ConfigureCoreDependencies(builder.Configuration);
        builder.Services.AddDesktopDependencies();

        _host = builder.Build();
        await _host.StartAsync().ConfigureAwait(true);

        _host.Services.GetRequiredService<MainWindow>().Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host is not null)
        {
            await _host.StopAsync().ConfigureAwait(true);
            _host.Dispose();
        }

        base.OnExit(e);
    }
}