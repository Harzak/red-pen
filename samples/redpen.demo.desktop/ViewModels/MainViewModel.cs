namespace redpen.demo.desktop.ViewModels;

internal sealed class MainViewModel : ViewModelBase
{
    private readonly ILocalizer _localizer;

    public string Title => _localizer[StringKeys.MainWindowTitle];

    public TextEditorViewModel TextEditor { get; }

    public MainViewModel(TextEditorViewModel textEditor, ILocalizer localizer)
    {
        ArgumentNullException.ThrowIfNull(textEditor);
        ArgumentNullException.ThrowIfNull(localizer);

        this.TextEditor = textEditor;
        _localizer = localizer;
    }
}