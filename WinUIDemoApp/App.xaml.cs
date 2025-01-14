﻿using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using WinUI3Localizer;

namespace WinUIDemoApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    private IHost? Host { get; set; }

    private Window? Window { get; set; }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
            })
            .Build();

        await InitializeWinUI3Localizer();

        Window = new MainWindow();
        Window.Activate();
    }

    public static string StringsFolderPath { get; private set; } = string.Empty;

    private static async Task MakeSureStringResourceFileExists(StorageFolder stringsFolder, string language, string resourceFileName)
    {
        StorageFolder languageFolder = await stringsFolder.CreateFolderAsync(
            desiredName: language,
            CreationCollisionOption.OpenIfExists);

        string appResourceFilePath = Path.Combine(stringsFolder.Name, language, resourceFileName);
        StorageFile appResourceFile = await LoadStringResourcesFileFromAppResource(appResourceFilePath);

        IStorageItem? localResourceFile = await languageFolder.TryGetItemAsync(resourceFileName);

        if (localResourceFile is null ||
            (await GetModifiedDate(appResourceFile)) > (await GetModifiedDate(localResourceFile)))
        {
            _ = await appResourceFile.CopyAsync(
                destinationFolder: languageFolder,
                desiredNewName: appResourceFile.Name,
                option: NameCollisionOption.ReplaceExisting);
        }
    }

    private static async Task<DateTimeOffset> GetModifiedDate(IStorageItem file)
    {
        return (await file.GetBasicPropertiesAsync()).DateModified;
    }

    private static async Task<StorageFile> LoadStringResourcesFileFromAppResource(string filePath)
    {
        Uri resourcesFileUri = new($"ms-appx:///{filePath}");
        return await StorageFile.GetFileFromApplicationUriAsync(resourcesFileUri);
    }

    private static async Task InitializeWinUI3Localizer()
    {
#if IS_NON_PACKAGED
        PrepareEnvironmentForWinUI3LocalizerOnNonPackagedApp();
#else
        await PrepareEnvironmentForWinUI3LocalizerOnPackagedApp();
#endif

        _ = await new LocalizerBuilder()
            .AddStringResourcesFolderForLanguageDictionaries(StringsFolderPath)
            //.SetLogger(Host.Services
            //    .GetRequiredService<ILoggerFactory>()
            //    .CreateLogger<Localizer>())
            .SetOptions(options =>
            {
                options.DefaultLanguage = "en-US";
            })
            //.AddLocalizationAction(new LocalizationActionItem(typeof(Hyperlink), arguments =>
            //{
            //    if (arguments.DependencyObject is Hyperlink target && target.Inlines.Count is 0)
            //    {
            //        target.Inlines.Clear();
            //        target.Inlines.Add(new Run() { Text = arguments.Value });
            //    }
            //}))
            //.AddLocalizationAction(new LocalizationActionItem(typeof(Run), arguments =>
            //{
            //    if (arguments.DependencyObject is Run target)
            //    {
            //        target.Text = arguments.Value;
            //    }
            //}))
            .Build();
    }
#if IS_NON_PACKAGED
    private static void PrepareEnvironmentForWinUI3LocalizerOnNonPackagedApp()
    {
        // Initialize a "Strings" folder in the executables folder.
        StringsFolderPath = Path.Combine(AppContext.BaseDirectory, "Strings");
    }
#else
    private static async Task PrepareEnvironmentForWinUI3LocalizerOnPackagedApp()
    {
        // Initialize a "Strings" folder in the "LocalFolder" for the packaged app.
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        StorageFolder stringsFolder = await localFolder.CreateFolderAsync("Strings", CreationCollisionOption.OpenIfExists);
        StringsFolderPath = stringsFolder.Path;

        // Create string resources file from app resources if doesn't exists.
        await MakeSureStringResourceFileExists(stringsFolder, "en-US", "Resources.resw");
        //await MakeSureStringResourceFileExists(stringsFolder, "en-US", "ErrorMessages.resw");
        //await MakeSureStringResourceFileExists(stringsFolder, "es-ES", "Resources.resw");
        //await MakeSureStringResourceFileExists(stringsFolder, "es-ES", "ErrorMessages.resw");
        //await MakeSureStringResourceFileExists(stringsFolder, "ja", "Resources.resw");
        //await MakeSureStringResourceFileExists(stringsFolder, "ja", "ErrorMessages.resw");
    }
#endif
}
