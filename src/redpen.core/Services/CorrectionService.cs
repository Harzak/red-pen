using System.Text.Json;

namespace redpen.core.Services;

internal sealed class CorrectionService : ICorrectionService
{
    private const string RESPONSE_SCHEMA_DESCRIPTION = "The corrected text and an explanation of the correction.";
    private const string USER_PROMPT_PREFIX = "The text to be corrected";

    private readonly IChatClient _chatClient;
    private readonly IPromptFactory _promptFactory;

    public CorrectionService(IChatClientFactory chatClientFactory, IPromptFactory promptFactory)
    {
        ArgumentNullException.ThrowIfNull(chatClientFactory);
        ArgumentNullException.ThrowIfNull(promptFactory);

        _chatClient = chatClientFactory.CreateChatClient();
        _promptFactory = promptFactory;
    }

    public async Task<TextCorrectionResult> CorrectAsync(string text, ECorrectionMode correctionMode, EResponseMode responseMode)
    {
        TextCorrectionResult result = new();

        string instructionText = _promptFactory.CreateTextCorrectionPrompt(correctionMode, responseMode);

        JsonElement schema = AIJsonUtilities.CreateJsonSchema(typeof(CorrectionResponse));
        ChatOptions options = new()
        {
            Temperature = 0.1f,
            TopP = 0.9f,
            ResponseFormat = ChatResponseFormat.ForJsonSchema(
                schema,
                schemaName: nameof(CorrectionResponse),
                schemaDescription: RESPONSE_SCHEMA_DESCRIPTION),
        };

        ChatMessage instruction = new(ChatRole.System, instructionText);
        ChatMessage userText = new(ChatRole.User, $"{USER_PROMPT_PREFIX}: \"{text}\"");

        ChatResponse<CorrectionResponse> response = await _chatClient.GetResponseAsync<CorrectionResponse>([instruction, userText], options).ConfigureAwait(false);

        if (response.Result == null
            || string.IsNullOrWhiteSpace(response.Result.CorrectedText))
        {
            return result.WithFailure();
        }

        result.OriginalText = text;
        result.CorrectedText = response.Result.CorrectedText;
        result.Explanation = response.Result.Explanation;

        return result.WithSuccess();
    }
}

