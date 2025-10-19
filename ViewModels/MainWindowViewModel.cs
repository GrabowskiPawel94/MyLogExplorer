
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyLogExplorer.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string? _loadedLogs;
    
    // Command the ImportButton will call with the imported text
    public IRelayCommand<string> LoadTextCommand { get; }

    public MainWindowViewModel()
    {
        LoadTextCommand = new RelayCommand<string>(LoadLogs);
    }

    private void LoadLogs(string? text)
    {
        LoadedLogs = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
    }
}
