namespace redpen.demo.desktop.ViewModels;

internal sealed partial class TextEditorViewModel : ViewModelBase
{
    private readonly ITextCorrectionService _correctionService;
    private readonly ILocalizer _localizer;

    public string CorrectButtonLabel => _localizer[StringKeys.TextEditorCorrectButton];

    public IReadOnlyList<ETextCorrectionMode> CorrectionModes { get; } = [ETextCorrectionMode.GrammarAndSpelling, ETextCorrectionMode.StyleAndTone];

    public IReadOnlyList<ETextResponseMode> ResponseModes { get; } = [ETextResponseMode.Concise, ETextResponseMode.Detailed];

    [ObservableProperty]
    private ETextCorrectionMode _selectedCorrectionMode = ETextCorrectionMode.GrammarAndSpelling;

    [ObservableProperty]
    private ETextResponseMode _selectedResponseMode = ETextResponseMode.Concise;

    [ObservableProperty]
    private string _text = string.Empty;

    [ObservableProperty]
    private string _correctedText = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public TextEditorViewModel(ITextCorrectionService correctionService, ILocalizer localizer)
    {
        ArgumentNullException.ThrowIfNull(correctionService);
        ArgumentNullException.ThrowIfNull(localizer);

        _correctionService = correctionService;
        _localizer = localizer;
    }

    [RelayCommand]
    private async Task CorrectAsync(CancellationToken cancellationToken)
    {
        this.ErrorMessage = string.Empty;

        TextCorrectionOptions options = new()
        {
            TextLanguage = CultureInfo.CurrentUICulture,
            CorrectionMode = this.SelectedCorrectionMode,
            ResponseMode = this.SelectedResponseMode,
        };
        TextCorrectionResult result = await _correctionService.CorrectAsync(this.Text, options, cancellationToken).ConfigureAwait(false);

        this.CorrectedText = result.IsSuccess ? result.CorrectedText : string.Empty;
        this.ErrorMessage = result.IsSuccess ? string.Empty : result.ErrorMessage;
    }
}