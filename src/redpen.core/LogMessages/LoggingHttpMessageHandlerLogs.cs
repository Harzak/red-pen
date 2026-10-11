namespace redpen.core.LogMessages
{
    internal static partial class LoggingHttpMessageHandlerLogs
    {

        [LoggerMessage(Level = LogLevel.Debug, Message = "HTTP request {Method} {Uri}: {Body}")]
        internal static partial void LogHttpRequest(ILogger logger, HttpMethod method, Uri? uri, string body);

        [LoggerMessage(Level = LogLevel.Debug, Message = "HTTP response {StatusCode} from {Uri}: {Body}")]
        internal static partial void LogHttpResponse(ILogger logger, int statusCode, Uri? uri, string body);
    }
}
