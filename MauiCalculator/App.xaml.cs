namespace MauiCalculator;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
    
    public static Window Window { get; private set; }
    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);
        Window = window;
        return window;
    }
}