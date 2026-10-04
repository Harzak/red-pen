using OllamaSharp;

namespace redpen.core.Factories;

internal sealed class ChatClientFactory : IChatClientFactory
{
    private readonly IOptions<ChatClientOptions> _clientOptions;

    public ChatClientFactory(IOptions<ChatClientOptions> clientOptions)
    {
        _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
    }

    public IChatClient CreateChatClient()
    {
        Uri clientUrl = _clientOptions.Value?.Url ?? throw new InvalidOperationException("The client URL is not configured.");
        string model = _clientOptions.Value?.Model ?? throw new InvalidOperationException("The model is not configured.");

        switch (model)
        {
            case ChatClientModelConsts.qwen3_4b_instruct_2507_q8_0:
                return new OllamaApiClient(clientUrl, model);
            default:
                throw new NotSupportedException($"The model '{model}' is not supported.");
        }
    }
}