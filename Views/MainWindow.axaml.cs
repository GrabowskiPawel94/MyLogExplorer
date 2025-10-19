using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MyLogExplorer.Messages;
using MyLogExplorer.Models;

namespace MyLogExplorer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        WeakReferenceMessenger.Default.Register<ToastMessage>(this, async (_, msg) =>
            {
                var (text, bg, dur) = msg.Value;
                await ToastOverlay.ShowAsync(new ToastItem
                {
                    Text = text,
                    Background = bg,
                    Duration = dur,
                });
            });
    }
}