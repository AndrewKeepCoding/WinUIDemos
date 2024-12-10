using System;
using System.Threading.Tasks;

namespace WinUIDemoApp;

public sealed partial class SplashScreen : WinUIEx.SplashScreen
{
    public SplashScreen(Type window) : base(window)
    {
        InitializeComponent();
    }

    public Func<Task>? Function { get; set; }

    protected override async Task OnLoading()
    {
        if (Function is not null)
        {
            await Function.Invoke();
        }
        
        await base.OnLoading();
    }
}
