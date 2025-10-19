using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using MyLogExplorer.Models;

namespace MyLogExplorer.Controls;

public partial class Toast : UserControl
{
    private readonly ObservableCollection<ToastItem> _items = new();
    
    public Toast()
    {
        InitializeComponent();
        PART_Items.ItemsSource = _items;
    }
    
    public async Task ShowAsync(ToastItem toast)
    {
        await Dispatcher.UIThread.InvokeAsync(() => _items.Add(toast));
        try
        {
            await Task.Delay(toast.Duration);
        }
        finally
        {
            await Dispatcher.UIThread.InvokeAsync(() => _items.Remove(toast));
        }
    }
}