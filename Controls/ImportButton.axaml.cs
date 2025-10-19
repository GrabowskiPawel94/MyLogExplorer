using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MyLogExplorer.ViewModels;

namespace MyLogExplorer.Controls;

public partial class ImportButton : UserControl
{
    public ImportButton()
    {
        InitializeComponent();
        
        
        // Bridge SubmitTextCommand to the internal VM
        if (DataContext is ImportButtonViewModel vm)
            vm.SubmitTextCommand = SubmitTextCommand as IRelayCommand<string>;
        
        DataContextChanged += (_, __) => SyncSubmitCommandIntoVm();
        PropertyChanged += OnPropertyChanged;
    }
    
    // Command that parent ViewModel binds to — receives the imported text
    public static readonly StyledProperty<ICommand?> SubmitTextCommandProperty =
        AvaloniaProperty.Register<ImportButton, ICommand?>(nameof(SubmitTextCommand));
    
    public ICommand? SubmitTextCommand
    {
        get => GetValue(SubmitTextCommandProperty);
        set
        {
            SetValue(SubmitTextCommandProperty, value);
            if (DataContext is ImportButtonViewModel vm)
                vm.SubmitTextCommand = value as IRelayCommand<string>;
        }
    }
    
    private void SyncSubmitCommandIntoVm()
    {
        // Only forward when both sides are ready
        if (DataContext is ImportButtonViewModel vm)
        {
            // Accept ICommand from XAML but require IRelayCommand<string> in the VM
            vm.SubmitTextCommand = SubmitTextCommand as IRelayCommand<string>;
        }
    }
    
    private void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == SubmitTextCommandProperty)
            SyncSubmitCommandIntoVm();
    }
}