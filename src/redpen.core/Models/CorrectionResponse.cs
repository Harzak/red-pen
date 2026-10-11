using System.ComponentModel;

namespace redpen.core.Models;

internal sealed record CorrectionResponse
{
    [Description("The fully corrected version of the input text, preserving its original meaning and language.")]
    public string CorrectedText { get; init; } = string.Empty;

    [Description("A brief explanation of what was corrected and why, written in the same language as the input text.")]
    public string Explanation { get; init; } = string.Empty;
}