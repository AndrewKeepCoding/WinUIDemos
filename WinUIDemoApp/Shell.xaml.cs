using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System;
using Microsoft.UI.Xaml.Data;

namespace WinUIDemoApp;

public class IntToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value is int intValue
            ? intValue.ToString()
            : string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return value is string stringValue
            ? int.Parse(stringValue)
            : 0;
    }
}

public class  StringToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value is string stringValue
            ? int.Parse(stringValue)
            : 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return value is int intValue
            ? intValue.ToString()
            : string.Empty;
    }
}

public sealed class GreaterThanAttribute(string propertyName) : ValidationAttribute
{
    public string PropertyName { get; } = propertyName;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        object instance = validationContext.ObjectInstance;

        if (instance.GetType().GetProperty(PropertyName)?.GetValue(instance) is object otherValue &&
            (value as IComparable)?.CompareTo(otherValue) > 0 &&
            ValidationResult.Success is not null)
        {
            return ValidationResult.Success;
        }

        return new($"The {PropertyName} is smaller than the other one.");
    }
}

public partial class ShellPageViewModel : ObservableValidator
{
    [ObservableProperty]
    //[NotifyDataErrorInfo]
    //[Required(ErrorMessage = "Username cannot be empty.")]
    //[MinLength(2, ErrorMessage = "Provide more than 1 character.")]
    private string _name = string.Empty;

    [ObservableProperty]
    private string message = string.Empty;

    [ObservableProperty]
    //[GreaterThan(nameof(MinAge), ErrorMessage = "The length of the name must be greater than the minimum length.")]
    [Range(1, int.MaxValue, ErrorMessage = "Age must be between 0 and 120.")]
    private int _age;

    public ShellPageViewModel()
    {
        this.ErrorsChanged += ShellPageViewModel_ErrorsChanged;
        Name = "";
    }

    private int MinAge { get; set; } = 5;
    partial void OnNameChanging(string? oldValue, string newValue)
    {
    }

    private void ShellPageViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
    {
    }

    [RelayCommand]
    private void Submit()
    {
        ValidateAllProperties();

        if (HasErrors is true)
        {
            //Message = "Please fix the errors.";
            Message = GetErrors().FirstOrDefault()?.ErrorMessage ?? string.Empty;
            return;
        }

        Message = "Successed!";
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    public ShellPageViewModel ViewModel { get; } = new();
}
