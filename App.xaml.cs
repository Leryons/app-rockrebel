namespace RockRebel;

public partial class App : Application
{
    private readonly SongServices songServices;
    
    public App(SongServices songServices)
    {
        InitializeComponent();
        this.songServices = songServices;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}