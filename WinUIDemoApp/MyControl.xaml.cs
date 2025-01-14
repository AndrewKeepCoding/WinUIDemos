using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIDemoApp;

public sealed partial class MyControl : UserControl
{
    public MyControl()
    {
        this.InitializeComponent();
        //System.Diagnostics.Debug.WriteLine("MyControl initialized.");
        //Loaded += MyControl_Loaded;
        //Unloaded += MyControl_Unloaded;
    }



    public int MyControlNumber
    {
        get => (int)GetValue(MyControlNumberProperty);
        set => SetValue(MyControlNumberProperty, value);
    }

    public static readonly DependencyProperty MyControlNumberProperty =
        DependencyProperty.Register(
            nameof(MyControlNumber),
            typeof(int),
            typeof(MyControl),
            new PropertyMetadata(default));




    //private void MyControl_Loaded(object sender, RoutedEventArgs e)
    //{
    //    System.Diagnostics.Debug.WriteLine($"MyControl {MyControlNumber} loaded.");
    //}

    //private void MyControl_Unloaded(object sender, RoutedEventArgs e)
    //{
    //    System.Diagnostics.Debug.WriteLine($"MyControl {MyControlNumber} unloaded.");
    //}

    //public TabViewModel ViewModel => DataContext as TabViewModel;


    //public string MyControlNumber
    //{
    //    get => (string)GetValue(MyNumberProperty);
    //    set => SetValue(MyNumberProperty, value);
    //}

    //public static readonly DependencyProperty MyNumberProperty =
    //    DependencyProperty.Register(
    //        nameof(MyControlNumber),
    //        typeof(string),
    //        typeof(MyControl),
    //        new PropertyMetadata(default, (d, e) =>
    //        {
    //            System.Diagnostics.Debug.WriteLine($"MyControl {((MyControl)d).MyControlNumber} property changed.");
    //        }));



    //public string Test
    //{
    //    get => (string)GetValue(TestProperty);
    //    set => SetValue(TestProperty, value);
    //}

    //public static readonly DependencyProperty TestProperty =
    //    DependencyProperty.Register(
    //        nameof(Test),
    //        typeof(string),
    //        typeof(MyControl),
    //        new PropertyMetadata(default));


}
