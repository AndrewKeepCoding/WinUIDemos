using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WinUIDemoApp;

public sealed class PeriodicExecutor
{
    private Task PeriodicActionTask { get; set; } = Task.CompletedTask;

    private CancellationTokenSource? CancellationTokenSource { get; set; }

    public async Task Start(
        TimeSpan interval,
        Action action,
        bool keepSameThread = true,
        bool throwOperationCanceledException = true)
    {
        await Stop();

        CancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = CancellationTokenSource.Token;

        PeriodicActionTask = RunPeriodicAction(interval, action, keepSameThread, throwOperationCanceledException, cancellationToken);
    }

    public async Task Stop()
    {
        CancellationTokenSource?.Cancel();
        CancellationTokenSource = null;

        await PeriodicActionTask;
        PeriodicActionTask = Task.CompletedTask;
    }

    private static async Task RunPeriodicAction(
        TimeSpan interval,
        Action action,
        bool keepSameThread,
        bool throwOperationCanceledException,
        CancellationToken cancellationToken)
    {
        using PeriodicTimer timer = new(interval);

        try
        {
            while (await timer.WaitForNextTickAsync(cancellationToken).ConfigureAwait(keepSameThread) is true)
            {
                action();
            }
        }
        catch (OperationCanceledException)
        {
            if (throwOperationCanceledException is true)
            {
                throw;
            }
        }
        catch (Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception);
            throw;
        }
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    public enum IntervalTimeSpanUnit
    {
        Microseconds,
        Milliseconds,
        Seconds,
    }

    public string[] IntervalTimeSpanUnitOptions { get; } = Enum.GetNames(typeof(IntervalTimeSpanUnit));

    private PeriodicExecutor? PeriodicExecutor { get; set; }

    private Task PeriodicExecutorTask { get; set; } = Task.CompletedTask;

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if (IntervalValueNumberBox.Value <= 0 ||
            IntervalTimeSpanUnitComboBox.SelectedItem is not string selectedUnit ||
            Enum.TryParse<IntervalTimeSpanUnit>(selectedUnit, out var intervalUnit) is false)
        {
            return;
        }

        var intervalValue = IntervalValueNumberBox.Value;

        TimeSpan intervalTimespan = intervalUnit switch
        {
            IntervalTimeSpanUnit.Microseconds => TimeSpan.FromMilliseconds(intervalValue),
            IntervalTimeSpanUnit.Milliseconds => TimeSpan.FromMilliseconds(intervalValue),
            IntervalTimeSpanUnit.Seconds => TimeSpan.FromSeconds(intervalValue),
            _ => throw new InvalidOperationException("Invalid interval TimeSpan unit.")
        };

        PeriodicExecutor = new PeriodicExecutor();
        PeriodicExecutorTask = PeriodicExecutor.Start(intervalTimespan, UpdateTimer);

        System.Diagnostics.Debug.WriteLine("Exiting StartButton_Click.");
    }

    private void UpdateTimer()
    {
        TimerTextBlock.Text = DateTime.Now.ToString("HH:mm:ss.fff");
    }

    private async void StopButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        PeriodicExecutor?.Stop();
        await PeriodicExecutorTask;
        PeriodicExecutor = null;

        System.Diagnostics.Debug.WriteLine("Exiting StopButton_Click.");
    }
}
