namespace redpen.core.Services;

internal sealed class TextCorrectionService : ITextCorrectionService
{
    private const string RESPONSE_SCHEMA_DESCRIPTION = "The corrected text and an explanation of the correction.";
    private const string USER_PROMPT_PREFIX = "The text to be corrected";

    private readonly IChatClient _chatClient;
    private readonly IPromptFactory _promptFactory;

    public TextCorrectionService(IChatClientFactory chatClientFactory, IPromptFactory promptFactory)
    {
        ArgumentNullException.ThrowIfNull(chatClientFactory);
        ArgumentNullException.ThrowIfNull(promptFactory);

        _chatClient = chatClientFactory.CreateChatClient();
        _promptFactory = promptFactory;
    }

    public async Task<TextCorrectionResult> CorrectAsync(string text, TextCorrectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        TextCorrectionResult result = new();
        Stopwatch sp = new();
        sp.Start();

        string instructionText = _promptFactory.CreateTextCorrectionPrompt(options.CorrectionMode, options.ResponseMode, options.TextLanguage);

        JsonElement schema = AIJsonUtilities.CreateJsonSchema(typeof(TextCorrectionResponse));
        ChatOptions chatOptions = new()
        {
            Temperature = 0.1f,
            TopP = 0.9f,
            ResponseFormat = ChatResponseFormat.ForJsonSchema(
                schema,
                schemaName: nameof(TextCorrectionResponse),
                schemaDescription: RESPONSE_SCHEMA_DESCRIPTION),
        };

        ChatMessage instruction = new(ChatRole.System, instructionText);
        ChatMessage userText = new(ChatRole.User, $"{USER_PROMPT_PREFIX}: \"{text}\"");

        ChatResponse<TextCorrectionResponse> response = await _chatClient.GetResponseAsync<TextCorrectionResponse>([instruction, userText], chatOptions).ConfigureAwait(false);

        if (response.Result == null
            || string.IsNullOrWhiteSpace(response.Result.CorrectedText))
        {
            return result.WithFailure();
        }

        result.OriginalText = text;
        result.CorrectedText = response.Result.CorrectedText;
        result.Explanation = response.Result.Explanation;
        if (response.Usage != null)
        {
            result.TotalTokenUsage = response.Usage.TotalTokenCount ?? 0;
        }

        sp.Stop();
        result.TotalDuration = sp.Elapsed;

        return result.WithSuccess();
    }
}

