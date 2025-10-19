using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MyLogExplorer.Messages;

public class ToastMessage : ValueChangedMessage<(string Text, IBrush Bacground, TimeSpan Duration)>
{
    public ToastMessage(string text, IBrush bacground, TimeSpan? duration = null) 
        : base((text, bacground, duration ?? TimeSpan.FromSeconds(5))) { }
    
    
    public static IBrush InfoBg { get; } = Brushes.CornflowerBlue;
}