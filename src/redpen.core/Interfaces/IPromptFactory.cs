namespace redpen.core.Interfaces;

internal interface IPromptFactory
{
    string CreateTextCorrectionPrompt(
        ETextCorrectionMode correctionMode,
        ETextResponseMode responseMode,
        CultureInfo? textLanguage = null);
}
