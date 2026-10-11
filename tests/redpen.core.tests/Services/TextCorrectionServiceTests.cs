namespace redpen.core.tests.Services;

[TestClass]
public sealed class TextCorrectionServiceTests
{
    [TestMethod]
    public async Task CorrectAsync_ChatClientReturnsValidJson_ReturnsSuccessfulResult()
    {
        // Arrange
        string correctedText = "This is the corrected text.";
        string explanation = "Fixed a grammar mistake.";
        string jsonResponse = JsonSerializer.Serialize(new
        {
            CorrectedText = correctedText,
            Explanation = explanation
        });

        ChatResponse chatResponse = new(new ChatMessage(ChatRole.Assistant, jsonResponse));

        IChatClient chatClient = A.Fake<IChatClient>();
        A.CallTo(() => chatClient.GetResponseAsync(A<IEnumerable<ChatMessage>>._, A<ChatOptions>._, A<CancellationToken>._))
            .Returns(chatResponse);

        IChatClientFactory chatClientFactory = A.Fake<IChatClientFactory>();
        A.CallTo(() => chatClientFactory.CreateChatClient()).Returns(chatClient);

        IPromptFactory promptFactory = A.Fake<IPromptFactory>();
        A.CallTo(() => promptFactory.CreateTextCorrectionPrompt(A<ETextCorrectionMode>._, A<ETextResponseMode>._, A<CultureInfo?>._))
            .Returns("Correct the following text.");

        TextCorrectionService service = new(chatClientFactory, promptFactory);
        TextCorrectionOptions options = new();
        string originalText = "This is teh text to correct.";

        // Act
        TextCorrectionResult result = await service.CorrectAsync(originalText, options).ConfigureAwait(false);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.OriginalText.Should().Be(originalText);
        result.CorrectedText.Should().Be(correctedText);
        result.Explanation.Should().Be(explanation);
    }

    [TestMethod]
    public async Task CorrectAsync_ChatClientReturnsEmptyCorrectedText_ReturnsFailureResult()
    {
        // Arrange
        string jsonResponse = JsonSerializer.Serialize(new
        {
            CorrectedText = string.Empty,
            Explanation = string.Empty
        });

        ChatResponse chatResponse = new(new ChatMessage(ChatRole.Assistant, jsonResponse));

        IChatClient chatClient = A.Fake<IChatClient>();
        A.CallTo(() => chatClient.GetResponseAsync(A<IEnumerable<ChatMessage>>._, A<ChatOptions>._, A<CancellationToken>._))
            .Returns(chatResponse);

        IChatClientFactory chatClientFactory = A.Fake<IChatClientFactory>();
        A.CallTo(() => chatClientFactory.CreateChatClient()).Returns(chatClient);

        IPromptFactory promptFactory = A.Fake<IPromptFactory>();
        A.CallTo(() => promptFactory.CreateTextCorrectionPrompt(A<ETextCorrectionMode>._, A<ETextResponseMode>._, A<CultureInfo?>._))
            .Returns("Correct the following text.");

        TextCorrectionService service = new(chatClientFactory, promptFactory);
        TextCorrectionOptions options = new();
        string originalText = "This is teh text to correct.";

        // Act
        TextCorrectionResult result = await service.CorrectAsync(originalText, options).ConfigureAwait(false);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public async Task CorrectAsync_ChatClientReturnsUsage_MapsTotalTokenUsage()
    {
        // Arrange
        string jsonResponse = JsonSerializer.Serialize(new
        {
            CorrectedText = "Corrected.",
            Explanation = "Explanation."
        });

        ChatResponse chatResponse = new(new ChatMessage(ChatRole.Assistant, jsonResponse))
        {
            Usage = new UsageDetails
            {
                TotalTokenCount = 42
            },
        };

        IChatClient chatClient = A.Fake<IChatClient>();
        A.CallTo(() => chatClient.GetResponseAsync(A<IEnumerable<ChatMessage>>._, A<ChatOptions>._, A<CancellationToken>._))
            .Returns(chatResponse);

        IChatClientFactory chatClientFactory = A.Fake<IChatClientFactory>();
        A.CallTo(() => chatClientFactory.CreateChatClient()).Returns(chatClient);

        IPromptFactory promptFactory = A.Fake<IPromptFactory>();
        A.CallTo(() => promptFactory.CreateTextCorrectionPrompt(A<ETextCorrectionMode>._, A<ETextResponseMode>._, A<CultureInfo?>._))
            .Returns("Correct the following text.");

        TextCorrectionService service = new(chatClientFactory, promptFactory);
        TextCorrectionOptions options = new();
        string originalText = "This is teh text to correct.";

        // Act
        TextCorrectionResult result = await service.CorrectAsync(originalText, options).ConfigureAwait(false);

        // Assert
        result.TotalTokenUsage.Should().Be(42);
    }
}
