using AutoEditor.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using System.Reflection;

namespace AutoEditor.Drawers;

[TypeDrawer(typeof(Point))]
public class PointDrawer : IPropertyControl
{
  public Control CreateController (PropertyInfo prop, object obj) {
    var x = new NumericUpDown {
      [!NumericUpDown.ValueProperty] = new Binding("X") { Source = prop.GetValue(obj), Mode = BindingMode.TwoWay },
      VerticalAlignment = VerticalAlignment.Stretch,
      HorizontalAlignment = HorizontalAlignment.Stretch,
      ShowButtonSpinner = true,
    };
    var y = new NumericUpDown {
      [!NumericUpDown.ValueProperty] = new Binding("Y") { Source = prop.GetValue(obj), Mode = BindingMode.TwoWay },
      VerticalAlignment = VerticalAlignment.Stretch,
      HorizontalAlignment = HorizontalAlignment.Stretch,
      ShowButtonSpinner = true,
    };
    return new StackPanel() { Orientation = Orientation.Horizontal, Children = { x, y }  }.WithLabel(prop.Name);
  }
}
