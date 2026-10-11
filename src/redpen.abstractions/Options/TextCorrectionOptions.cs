namespace redpen.abstractions.Options;

public class TextCorrectionOptions
{
    public ETextCorrectionMode CorrectionMode { get; set; } = ETextCorrectionMode.GrammarAndSpelling;
    public ETextResponseMode ResponseMode { get; set; } = ETextResponseMode.Concise;
    public CultureInfo? TextLanguage { get; set; }
}