using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Blip.Avalonia.Extensions.AutoEditor;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.DefaultDrawers;

[TypeDrawer(typeof(double))]
public class DoubleDrawer : IPropertyControl
{
  public Control CreateController(PropertyInfo prop, object obj)
  {
    var controller = new NumericUpDown
    {
      [!NumericUpDown.ValueProperty] = new Binding(prop.Name) { Source = obj, Mode = BindingMode.TwoWay },
      VerticalAlignment = VerticalAlignment.Stretch,
      HorizontalAlignment = HorizontalAlignment.Stretch,
      ShowButtonSpinner = true,
    }.WithLabel(prop.Name);
    return controller.WithScroll();
  }
}
