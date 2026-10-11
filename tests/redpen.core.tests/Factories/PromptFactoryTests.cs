namespace redpen.core.tests.Factories;

[TestClass]
public sealed class PromptFactoryTests
{
    [TestMethod]
    public void CreateTextCorrectionPrompt_GrammarAndSpellingWithConciseResponse_ReturnsExpectedPrompt()
    {
        PromptFactory factory = new();

        string result = factory.CreateTextCorrectionPrompt(ETextCorrectionMode.GrammarAndSpelling, ETextResponseMode.Concise);

        result.Should().Be("Please correct the grammar and spelling errors in the following text with a concise response. Keep the response in the same language as the input text.");
    }

    [TestMethod]
    public void CreateTextCorrectionPrompt_WithTextLanguage_IncludesLanguageSentence()
    {
        PromptFactory factory = new();
        CultureInfo textLanguage = new("fr-FR");

        string result = factory.CreateTextCorrectionPrompt(ETextCorrectionMode.StyleAndTone, ETextResponseMode.Detailed, textLanguage);

        result.Should().Contain($"The text is in {textLanguage.DisplayName}.");
    }

    [TestMethod]
    public void CreateTextCorrectionPrompt_UnsupportedCorrectionMode_ThrowsNotSupportedException()
    {
        PromptFactory factory = new();
        ETextCorrectionMode unsupportedCorrectionMode = (ETextCorrectionMode)(-1);

        Action act = () => factory.CreateTextCorrectionPrompt(unsupportedCorrectionMode, ETextResponseMode.Concise);

        act.Should().Throw<NotSupportedException>()
            .WithMessage($"Correction mode '{unsupportedCorrectionMode}' is not supported.");
    }
}
