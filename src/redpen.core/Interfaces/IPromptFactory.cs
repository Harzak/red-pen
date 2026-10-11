namespace redpen.core.Interfaces;

internal interface IPromptFactory
{
    string CreateTextCorrectionPrompt(ECorrectionMode correctionMode, EResponseMode responseMode);
}
