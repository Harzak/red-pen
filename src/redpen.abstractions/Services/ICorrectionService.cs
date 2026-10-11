namespace redpen.abstractions.Services;

public interface ICorrectionService
{
    Task<TextCorrectionResult> CorrectAsync(string text, ECorrectionMode correctionMode, EResponseMode responseMode);
}
