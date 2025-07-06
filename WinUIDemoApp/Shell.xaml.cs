using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WinUIDemoApp;

public static class Helpers
{
    public static string GetErrorMessage(string propertyName, IEnumerable<ValidationResult> validationResults) =>
        validationResults.FirstOrDefault(result => result.MemberNames.Contains(propertyName))?.ErrorMessage ?? string.Empty;
}

public partial class ShellViewModel : ObservableValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "Name must be at least 1 character long.")]
    public partial string Name { get; set; } = string.Empty;


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, 200)]
    public partial int Age { get; set; }

    public ShellViewModel()
    {
        ErrorsChanged += ShellViewModel_ErrorsChanged;
    }

    public ObservableCollection<ValidationResult> ValidationResults { get; private set; } = [];

    private void ShellViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
    {
        ValidationResults.Clear();

        foreach (ValidationResult validationResult in GetErrors())
        {
            ValidationResults.Add(validationResult);
        }
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel { get; } = new();
}
