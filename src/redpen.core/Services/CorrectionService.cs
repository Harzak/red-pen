namespace redpen.core.Services;

internal sealed class CorrectionService : ICorrectionService
{
    private readonly IChatClient _chatClient;

    public CorrectionService(IChatClientFactory chatClientFactory)
    {
        ArgumentNullException.ThrowIfNull(chatClientFactory);
        _chatClient = chatClientFactory.CreateChatClient();

    }

    public async Task<string> CorrectAsync(string text)
    {
        string mesg = string.Format(CultureInfo.InvariantCulture, "Please correct the following text: {0}", text);
        ChatResponse response = await  _chatClient.GetResponseAsync([
                new(ChatRole.User, mesg),
            ]).ConfigureAwait(false);

        return string.Join(Environment.NewLine, response.Messages);
    }
}

