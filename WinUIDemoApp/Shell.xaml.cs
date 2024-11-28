using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using System;
using System.Collections.ObjectModel;

namespace WinUIDemoApp;

public partial class TestConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return (value as string) + "!";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

public partial class Member : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _name = string.Empty;
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Member> _members = [];

    public ShellViewModel()
    {
        for (int i = 0; i < 1; i++)
        {
            Members.Add(new Member { Id = i, Name = $"Name {i}" });
        }
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
        Loaded += Shell_Loaded;
    }

    public ShellViewModel ViewModel { get; } = new();

    private void Shell_Loaded(object sender, RoutedEventArgs e)
    {
        // To use a converter, you need to explicitly declare it in XAML.
        // https://github.com/microsoft/microsoft-ui-xaml/issues/7206

        string xamlString =
            """
            <DataTemplate
                xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:local="using:WinUIDemoApp">
                <Grid>
                    <Grid.Resources>
                        <local:TestConverter x:Key="TestConverter" />
                    </Grid.Resources>
                    <TextBlock Text="{Binding Name, Converter={StaticResource TestConverter}}" />
                </Grid>
            </DataTemplate>
            """;
        var dataTemplate = XamlReader.Load(xamlString) as DataTemplate;

        var listView = new ListView();
        listView.SetBinding(
            ItemsControl.ItemsSourceProperty,
            new Binding
            {
                Source = ViewModel,
                Path = new PropertyPath(nameof(ViewModel.Members)),
                Mode = BindingMode.OneWay,
            });
        
        listView.ItemTemplate = dataTemplate;
        Content = listView;
    }
}
