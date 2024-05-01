using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace Blip.Avalonia.Extensions.AutoEditor;

// Apparently Avalonia had the same idea of declerative construction
// https://github.com/AvaloniaUI/Avalonia.Markup.Declarative
// Could use that instead to build up stuff when needed.
// Basically it looks like this
/*  new TextBlock()
                    .Col(1)
                    .IsVisible( @vm.HideGreeting, bindingMode: BindingMode.OneWay, converter: InverseBooleanConverter ),
                    .Text( "Hello Avalonia" ), 
 */
public static class AvaloniaExtensions
{
  public static Panel WithLabel(this Control control, string label)
  {
    var panel = new DockPanel() { Children = { new Label { Content = label, HorizontalAlignment = HorizontalAlignment.Left }, control } };
    DockPanel.SetDock(panel.Children[0], Dock.Top);
    return panel;
  }

  public static Panel WithLabel(this Control control, Orientation orientation, string label)
  {
    var panel = new StackPanel() { Orientation = orientation, Children = { control, new Label { Content = label, HorizontalAlignment = HorizontalAlignment.Left } } };
    return panel;
  }

  public static Control WithScroll(this Control control) => new ScrollViewer() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto, Content = control };

  public static Control WithBorder(this Control control) => new Border() { BorderBrush = new SolidColorBrush(new Color(0xff, 0xa5, 0xa5, 0xaa)), BorderThickness = new Thickness(2), Child = control };

  public static Control WithGrid(this Control control) => new Grid()
  {
    RowDefinitions = { new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } },
    ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) } },
    Children = { control }
  };

  public static Control WithStackPanel(this Control control, Orientation orientation = Orientation.Vertical, int spacing = 2) => new StackPanel() { Orientation = orientation, Spacing = spacing, Children = { control } };

  public static Control WithDockPanel(this Control control) => new DockPanel() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, LastChildFill = true, Children = { control } };
}
