namespace redpen.core.Handlers;

/// <summary>
/// Logs outgoing HTTP requests and responses
/// </summary>
internal sealed class LoggingHttpMessageHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHttpMessageHandler> _logger;

    public LoggingHttpMessageHandler(ILogger<LoggingHttpMessageHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string requestBody = request.Content is null
            ? string.Empty
            : await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        LoggingHttpMessageHandlerLogs.LogHttpRequest(_logger, request.Method, request.RequestUri, requestBody);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        string responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        LoggingHttpMessageHandlerLogs.LogHttpResponse(_logger, (int)response.StatusCode, request.RequestUri, responseBody);

        return response;
    }
}
