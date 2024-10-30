using Microsoft.UI.Xaml;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using System;
using Windows.System.Profile;

namespace WinUIDemoApp;

// https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/notifications/app-notifications/app-notifications-quickstart?tabs=cs?wt.mc_id=MVP_356303
public class NotificationManager
{
    private bool IsRegistered { get; set; }

    public void Init()
    {
        // To ensure all Notification handling happens in this process instance, register for
        // NotificationInvoked before calling Register(). Without this a new process will
        // be launched to handle the notification.
        AppNotificationManager notificationManager = AppNotificationManager.Default;

        notificationManager.NotificationInvoked += OnNotificationInvoked;

        notificationManager.Register();
        IsRegistered = true;
    }

    public void Unregister()
    {
        if (IsRegistered is true)
        {
            AppNotificationManager.Default.Unregister();
            IsRegistered = false;
        }
    }

    private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        System.Diagnostics.Debug.WriteLine("Notification invoked.");
    }
}

public class Toast
{
    public static bool SendToast()
    {
        var appNotification = new AppNotificationBuilder()
            //.AddArgument("action", "ToastClick")
            //.AddArgument(Common.scenarioTag, ScenarioId.ToString())
            //.SetAppLogoOverride(new System.Uri("file://" + App.GetFullPathToAsset("Square150x150Logo.png")), AppNotificationImageCrop.Circle)
            //.AddText(ScenarioName)
            .AddText("This is an example message using XML")
            //.AddButton(new AppNotificationButton("Open App")
            //    .AddArgument("action", "OpenApp")
            //    .AddArgument(Common.scenarioTag, ScenarioId.ToString()))
            .BuildNotification();

        AppNotificationManager.Default.Show(appNotification);

        return appNotification.Id != 0; // return true (indicating success) if the toast was sent (if it has an Id)
    }
}

public partial class App : Application
{
    private Window? _window;
    private readonly NotificationManager _notificationManager;

    public App()
    {
        InitializeComponent();
        _notificationManager = new NotificationManager();
        AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
    }

    private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
    {
        _notificationManager.Unregister();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _notificationManager.Init();
        _window = new MainWindow();
        _window.Activate();
        Toast.SendToast();
    }
}
