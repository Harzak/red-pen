namespace redpen.abstractions.Services;

public interface ICorrectionService
{
    Task<string> CorrectAsync(string text);
}
