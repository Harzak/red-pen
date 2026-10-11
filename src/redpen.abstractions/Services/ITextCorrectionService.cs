namespace redpen.abstractions.Services;

public interface ITextCorrectionService
{
    Task<TextCorrectionResult> CorrectAsync(string text, TextCorrectionOptions options);
}
