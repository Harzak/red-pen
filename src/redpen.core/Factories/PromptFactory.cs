namespace redpen.core.Factories;

internal sealed class PromptFactory : IPromptFactory
{
    private static readonly Dictionary<ECorrectionMode, string> _correctionInstructions = new()
    {
        [ECorrectionMode.GrammarAndSpelling] = "Please correct the grammar and spelling errors in the following text",
        [ECorrectionMode.StyleAndTone] = "Please improve the style and tone of the following text",
    };

    private static readonly Dictionary<EResponseMode, string> _responseInstructions = new()
    {
        [EResponseMode.Concise] = "with a concise response",
        [EResponseMode.Detailed] = "with a detailed response and an explanation for each major correction",
    };

    public string CreateTextCorrectionPrompt(ECorrectionMode correctionMode, EResponseMode responseMode)
    {
        if (!_correctionInstructions.TryGetValue(correctionMode, out string? correctionInstruction))
        {
            throw new NotSupportedException($"Correction mode '{correctionMode}' is not supported.");
        }

        if (!_responseInstructions.TryGetValue(responseMode, out string? responseInstruction))
        {
            throw new NotSupportedException($"Response mode '{responseMode}' is not supported.");
        }

        return $"{correctionInstruction} {responseInstruction}.";
    }
}
