using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using System.Reflection;

namespace WinUIDemoApp;

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        Loaded += Shell_Loaded;
    }

    private void Shell_Loaded(object sender, RoutedEventArgs e)
    {
        if (Resources.TryGetValue("NodeStyle", out var resource) is not true ||
            resource is not Style nodeStyle)
        {
            return;
        }

        var styledButton = new Button
        {
            Style = nodeStyle
        };

        var properties = nodeStyle.TargetType
            .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .ToList();

        foreach (Setter setter in nodeStyle.Setters.Cast<Setter>())
        {
            if (properties
                    .Where(propertyInfo => (propertyInfo.GetValue(styledButton) as DependencyProperty) == setter.Property)
                    .FirstOrDefault() is not { } targetProperty ||
                styledButton.GetValue(setter.Property) is not { } value)
            {
                continue;
            }

            System.Diagnostics.Debug.WriteLine($"{targetProperty.Name} Value: {value}");
        }
    }
}
