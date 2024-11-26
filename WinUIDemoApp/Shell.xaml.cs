using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace WinUIDemoApp;

public enum Units
{
    Micrometer,
    Millimeter,
    Meter,
}

public partial class ComboBoxEx : ComboBox
{
    public static new readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register(
            nameof(SelectedValue),
            typeof(object),
            typeof(ComboBoxEx),
            new PropertyMetadata(default, (d, e) => (d as ComboBoxEx)?.OnSelectedValueChanged(e.NewValue)));

    public ComboBoxEx() : base()
    {
        Style = (Style)Application.Current.Resources["DefaultComboBoxStyle"];
        RegisterPropertyChangedCallback(SelectedItemProperty, OnSelectedItemPropertyChanged);
    }

    public new object? SelectedValue
    {
        get => (object?)GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    private bool IgnoreSelectedValueChanged { get; set; }

    private bool IgnoreSelectedItemChanged { get; set; }

    private void OnSelectedItemPropertyChanged(DependencyObject sender, DependencyProperty dp)
    {
        try
        {
            if (IgnoreSelectedValueChanged is true)
            {
                return;
            }

            IgnoreSelectedValueChanged = true;
            SelectedValue = SelectedItem?.GetType().GetProperty(SelectedValuePath)?.GetValue(SelectedItem);
        }
        finally
        {
            IgnoreSelectedValueChanged = false;
        }
    }

    private void OnSelectedValueChanged(object newValue)
    {
        try
        {
            if (IgnoreSelectedItemChanged is true)
            {
                return;
            }

            IgnoreSelectedItemChanged = true;

            if (ItemsSource is not IEnumerable items ||
                string.IsNullOrEmpty(SelectedValuePath) is true)
            {
                return;
            }

            PropertyInfo? propertyInfo = null;

            foreach (var item in items)
            {
                propertyInfo ??= item.GetType().GetProperty(SelectedValuePath);

                if (propertyInfo?.GetValue(item)?.Equals(newValue) is true)
                {
                    SelectedItem = item;
                    return;
                }
            }
        }
        finally
        {
            IgnoreSelectedItemChanged = false;
        }
    }
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private Units _selectedItem = Units.Millimeter;

    [ObservableProperty]
    private Units _selectedValue = Units.Millimeter;

    [ObservableProperty]
    private int _selectedNumber = 1;

    public Units[] UnitsEnumOptions { get; } = Enum.GetValues<Units>();

    public List<Tuple<Units, string>> UnitsOptions { get; } =
        [
            new Tuple<Units, string>(Units.Micrometer, "um"),
            new Tuple<Units, string>(Units.Millimeter, "mm"),
            new Tuple<Units, string>(Units.Meter, "m"),
        ];

    public List<Tuple<int, string>> NumberOptions { get; } =
        [
            new Tuple<int, string>(0, "Zero"),
            new Tuple<int, string>(1, "One"),
            new Tuple<int, string>(2, "Two"),
        ];
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel { get; } = new();

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.SelectedValue = ViewModel.SelectedValue == Units.Meter ? Units.Micrometer : Units.Meter;
    }
}
