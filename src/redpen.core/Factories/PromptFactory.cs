namespace redpen.core.Factories;

internal sealed class PromptFactory : IPromptFactory
{
    public string CreateTextCorrectionPrompt(
        ETextCorrectionMode correctionMode,
        ETextResponseMode responseMode,
        CultureInfo? textLanguage = null)
    {
        string correctionInstruction = GetCorrectionInstruction(correctionMode);
        string responseInstruction = GetResponseInstruction(responseMode);

        List<string> sentences = [$"{correctionInstruction} {responseInstruction}"];

        if (textLanguage != null)
        {
            sentences.Add($"The text is in {textLanguage.DisplayName}");
        }

        sentences.Add("Keep the response in the same language as the input text");

        return string.Join(". ", sentences) + ".";
    }

    private static string GetCorrectionInstruction(ETextCorrectionMode correctionMode)
    {
        Dictionary<ETextCorrectionMode, string> correctionInstructions = new()
        {
            [ETextCorrectionMode.GrammarAndSpelling] = "Please correct the grammar and spelling errors in the following text",
            [ETextCorrectionMode.StyleAndTone] = "Please improve the style and tone of the following text",
        };

        if (!correctionInstructions.TryGetValue(correctionMode, out string? correctionInstruction))
        {
            throw new NotSupportedException($"Correction mode '{correctionMode}' is not supported.");
        }

        return correctionInstruction;
    }

    private static string GetResponseInstruction(ETextResponseMode responseMode)
    {
        Dictionary<ETextResponseMode, string> responseInstructions = new()
        {
            [ETextResponseMode.Concise] = "with a concise response",
            [ETextResponseMode.Detailed] = "with a detailed response and an explanation for each major correction",
        };

        if (!responseInstructions.TryGetValue(responseMode, out string? responseInstruction))
        {
            throw new NotSupportedException($"Response mode '{responseMode}' is not supported.");
        }

        return responseInstruction;
    }
}