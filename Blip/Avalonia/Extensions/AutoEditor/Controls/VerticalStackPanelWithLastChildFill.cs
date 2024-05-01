using Avalonia;
using Avalonia.Controls;
using System;

namespace Blip.Avalonia.Extensions.AutoEditor.Controls;

public class VerticalStackPanelWithLastChildFill : Panel
{
  protected override Size MeasureOverride(Size availableSize)
  {
    double width = 0, height = 0;
    foreach (var child in Children)
    {
      child.Measure(availableSize);
      width = Math.Max(width, child.DesiredSize.Width);
      height += child.DesiredSize.Height;
    }
    return new Size(width, height);
  }

  protected override Size ArrangeOverride(Size finalSize)
  {
    double y = 0;
    foreach (var child in Children)
    {
      double childHeight = child.DesiredSize.Height;
      if (child == Children[^1]) { childHeight = Math.Max(0, finalSize.Height - y); }
      child.Arrange(new Rect(0, y, finalSize.Width, childHeight));
      y += childHeight;
    }
    return finalSize;
  }
}
