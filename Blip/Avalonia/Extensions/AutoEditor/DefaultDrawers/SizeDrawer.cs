using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Blip.Avalonia.Extensions.AutoEditor;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.DefaultDrawers;

[TypeDrawer(typeof(Size))]
public class SizeDrawer : IPropertyControl
{
  public Control CreateController(PropertyInfo prop, object obj)
  {
    var x = new NumericUpDown
    {
      [!NumericUpDown.ValueProperty] = new Binding("Width") { Source = prop.GetValue(obj), Mode = BindingMode.TwoWay },
      VerticalAlignment = VerticalAlignment.Stretch,
      HorizontalAlignment = HorizontalAlignment.Stretch,
      ShowButtonSpinner = true,
    };
    var y = new NumericUpDown
    {
      [!NumericUpDown.ValueProperty] = new Binding("Height") { Source = prop.GetValue(obj), Mode = BindingMode.TwoWay },
      VerticalAlignment = VerticalAlignment.Stretch,
      HorizontalAlignment = HorizontalAlignment.Stretch,
      ShowButtonSpinner = true,
    };
    return new StackPanel() { Orientation = Orientation.Horizontal, Children = { x, y } }.WithLabel(prop.Name);
  }
}
