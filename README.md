# WinUIDemos

This repo has branches for several WinUI demos.
Don't forget to give a ⭐ if you find it helpuf.🤩

## Branches

### Exceptions

#### Case 01

- Microsoft.UI.Xaml.Markup.XamlParseException:

    ```
    The text associated with this error code could not be found.

    Failed to assign to property 'Microsoft.UI.Xaml.ResourceDictionary.Source' because the type 'Windows.Foundation.String' cannot be assigned to the type 'Windows.Foundation.Uri'. [Line: 13 Position: 6]
    ```

    <details>
    <summary>Call Stack</summary>
 	    WinRT.Runtime.dll!WinRT.ExceptionHelpers.ThrowExceptionForHR.__Throw|38_0(int hr)	Unknown
 	    WinRT.Runtime.dll!WinRT.ExceptionHelpers.ThrowExceptionForHR(int hr)	Unknown
 	    Microsoft.WinUI.dll!ABI.Microsoft.UI.Xaml.IApplicationStaticsMethods.LoadComponent(WinRT.IObjectReference _obj, object component, System.Uri resourceLocator, Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation componentResourceLocation)	Unknown
 	    Microsoft.WinUI.dll!Microsoft.UI.Xaml.Application.LoadComponent(object component, System.Uri resourceLocator, Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation componentResourceLocation)	Unknown
    >	WinUIDemoApp.dll!WinUIDemoApp.Shell.InitializeComponent() Line 37	C#
 	    WinUIDemoApp.dll!WinUIDemoApp.Shell.Shell() Line 14	C#
 	    WinUIDemoApp.dll!WinUIDemoApp.WinUIDemoApp_XamlTypeInfo.XamlTypeInfoProvider.Activate_6_Shell() Line 302	C#
 	    WinUIDemoApp.dll!WinUIDemoApp.WinUIDemoApp_XamlTypeInfo.XamlUserType.ActivateInstance() Line 764	C#
 	    Microsoft.WinUI.dll!ABI.Microsoft.UI.Xaml.Markup.IXamlType.Do_Abi_ActivateInstance_13(nint thisPtr, nint* result)	Unknown
 	    [Native to Managed Transition]	
 	    [Managed to Native Transition]	
 	    Microsoft.WinUI.dll!ABI.Microsoft.UI.Xaml.IApplicationStaticsMethods.LoadComponent(WinRT.IObjectReference _obj, object component, System.Uri resourceLocator, Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation componentResourceLocation)	Unknown
 	    Microsoft.WinUI.dll!Microsoft.UI.Xaml.Application.LoadComponent(object component, System.Uri resourceLocator, Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation componentResourceLocation)	Unknown
 	    WinUIDemoApp.dll!WinUIDemoApp.MainWindow.InitializeComponent() Line 37	C#
 	    WinUIDemoApp.dll!WinUIDemoApp.MainWindow.MainWindow() Line 8	C#
 	    WinUIDemoApp.dll!WinUIDemoApp.App.OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) Line 25	C#
 	    Microsoft.WinUI.dll!Microsoft.UI.Xaml.Application.Microsoft.UI.Xaml.IApplicationOverrides.OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)	Unknown
 	    Microsoft.WinUI.dll!ABI.Microsoft.UI.Xaml.IApplicationOverrides.Do_Abi_OnLaunched_0(nint thisPtr, nint args)	Unknown
 	    [Native to Managed Transition]	
 	    [Managed to Native Transition]	
 	    Microsoft.WinUI.dll!ABI.Microsoft.UI.Xaml.IApplicationStaticsMethods.Start(WinRT.IObjectReference _obj, Microsoft.UI.Xaml.ApplicationInitializationCallback callback)	Unknown
 	    Microsoft.WinUI.dll!Microsoft.UI.Xaml.Application.Start(Microsoft.UI.Xaml.ApplicationInitializationCallback callback)	Unknown
 	    WinUIDemoApp.dll!WinUIDemoApp.Program.Main(string[] args) Line 26	C#
    </details>

- Cause

  Typo in the `Source` property of the `ResourceDictionary` element.

  ```xml
  <ResourceDictionary
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:local="using:WinUIDemoApp">

      <ResourceDictionary.MergedDictionaries>
          <!--  Incorrect Source  -->
          <ResourceDictionary Source="/Controls/AwesomeControl.xaml" />
          <!--  Correct Source  -->
          <!--<ResourceDictionary Source="/Controls/AwesomeCustomControl.xaml" />-->
      </ResourceDictionary.MergedDictionaries>

  </ResourceDictionary>
  ```