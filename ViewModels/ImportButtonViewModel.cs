using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyLogExplorer.Messages;

namespace MyLogExplorer.ViewModels;

public partial class ImportButtonViewModel : ObservableObject
{
    public IRelayCommand<string> SubmitTextCommand { get; set; }
    
    // The async commands exposed to the ImportButton view
    public IAsyncRelayCommand ImportLogsFromClipboardCommand { get; }
    public IAsyncRelayCommand ImportLogsFromFileCommand { get; }

    public ImportButtonViewModel()
    {
        ImportLogsFromClipboardCommand = new AsyncRelayCommand(ImportLogsFromClipboardAsync);
        ImportLogsFromFileCommand = new AsyncRelayCommand(ImportLogsFromFileAsync);
    }
    
    private Window? GetOwner() =>
        (App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

    private async Task ImportLogsFromClipboardAsync()
    {
        // Get the main window (desktop app)
        var lifetime = Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        var window = lifetime?.MainWindow;

        if (window?.Clipboard == null)
        {
            // Clipboard not available (e.g., non-desktop environment)
            return;
        }

        try
        {
            // Read text from clipboard
            var text = await window.Clipboard.GetTextAsync();

            // Validate and forward it to parent command
            if (!string.IsNullOrWhiteSpace(text))
            {
                SubmitTextCommand?.Execute(text);
                WeakReferenceMessenger.Default.Send(
                    new ToastMessage("Copied logs from clipboard", ToastMessage.InfoBg));
            }
            else
            {
                //TODO: Show up the error toast
            }
        }
        catch (Exception ex)
        {
            // In production, log or handle exceptions (clipboard locked, etc.)
            Console.WriteLine($"Clipboard read failed: {ex.Message}");
        }
    }

    private async Task ImportLogsFromFileAsync()
    {
        var onwer = GetOwner();
        
        //TODO: Move to some Error log or notification
        if (onwer is null)
            return;

        var fileDialog = new OpenFileDialog
        {
            Title = "Open log file",
            AllowMultiple = false,
            Filters =
            {
                new FileDialogFilter
                {
                    Name = "Text files",
                    Extensions = { "txt" }
                }
            }
        };
        var file = await fileDialog.ShowAsync(onwer);
        if (file is not null && file.Length == 1)
        {
            var text = await File.ReadAllTextAsync(file[0]);
            SubmitTextCommand?.Execute(text);
        }
    }

}