using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace WinUIDemoApp;

public class RootViewModel
{
    public IEnumerable<object> Sections { get; } = new[] { new NestedViewModel() };

    public class NestedViewModel
    {
        public string SampleText => "Hello world";
    }
}

public sealed partial class Shell : Page
{
    public Shell()
    {
        InitializeComponent();
    }
}
