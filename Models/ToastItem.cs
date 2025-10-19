using System;
using Avalonia.Media;

namespace MyLogExplorer.Models;

public sealed class ToastItem
{
    public string Text { get; init; } = string.Empty;
    public IBrush Background { get; init; } = Brushes.DarkOliveGreen;
    public TimeSpan Duration { get; init; } = TimeSpan.FromSeconds(5);
}