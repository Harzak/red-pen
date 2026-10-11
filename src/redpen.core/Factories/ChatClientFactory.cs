using OllamaSharp;

namespace redpen.core.Factories;

internal sealed class ChatClientFactory : IChatClientFactory
{
    private readonly IOptions<ChatClientOptions> _clientOptions;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IHttpClientFactory _httpClientFactory;

    public ChatClientFactory(IOptions<ChatClientOptions> clientOptions, ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
    {
        _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public IChatClient CreateChatClient()
    {
        Uri clientUrl = _clientOptions.Value?.Url ?? throw new InvalidOperationException("The client URL is not configured.");
        string model = _clientOptions.Value?.Model ?? throw new InvalidOperationException("The model is not configured.");
        int timeoutSeconds = _clientOptions.Value?.TimeoutSeconds ?? 100;

        HttpClient httpClient = _httpClientFactory.CreateClient(HttpClientConsts.CHAT_CLIENT_NAME);
        httpClient.BaseAddress = clientUrl;
        httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

        IChatClient chatClient = model switch
        {
            ChatClientModelConsts.qwen3_4b_instruct_2507_q8_0 => new OllamaApiClient(httpClient, model),
            _ => throw new NotSupportedException($"The model '{model}' is not supported."),
        };

        return chatClient.AsBuilder()
            .UseLogging(_loggerFactory)
            .Build();
    }
}
