namespace redpen.demo.desktop.Views;

internal sealed partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        this.InitializeComponent();
        this.DataContext = viewModel;
    }
}