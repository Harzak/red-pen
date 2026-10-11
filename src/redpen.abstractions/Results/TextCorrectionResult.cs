using System.Runtime.CompilerServices;

namespace redpen.abstractions.Results;

public sealed class TextCorrectionResult : OperationResult
{
    public string OriginalText { get; set; } = string.Empty;
    public string CorrectedText { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;

    public new TextCorrectionResult WithSuccess()
    {
        base.WithSuccess();
        return this;
    }

    public new TextCorrectionResult WithFailure()
    {
        base.WithFailure();
        return this;
    }
}
