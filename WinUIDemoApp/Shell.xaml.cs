using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;

namespace WinUIDemoApp;

public class SomeValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    private void BindButton_Click(object sender, RoutedEventArgs e)
    {
        TargetTextBlock.ClearValue(TextBlock.TextProperty);

        var binding = new Binding()
        {
            Mode = BindingMode.OneWay,
            Source = SourceTextBox,
            Path = new PropertyPath(nameof(TargetTextBlock.Text)),
            Converter = new SomeValueConverter(),
            ConverterLanguage = "en-US",
            ConverterParameter = "SomeParameter",
            FallbackValue = "FallbackValue",
            TargetNullValue = "TargetNullValue"

        };

        TargetTextBlock.SetBinding(TextBlock.TextProperty, binding);
    }

    private void UnbindButton_Click(object sender, RoutedEventArgs e)
    {
        TargetTextBlock.ClearValue(TextBlock.TextProperty);
    }
}
