using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.DefaultDrawers;

[TypeDrawer(typeof(bool))]
public class BooleanDrawer : IPropertyControl
{
  public Control CreateController(PropertyInfo prop, object obj)
  {
    return new CheckBox
    {
      [!ToggleButton.IsCheckedProperty] = new Binding(prop.Name) { Source = obj, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }
    }.WithLabel(Orientation.Horizontal, prop.Name);
  }
}
