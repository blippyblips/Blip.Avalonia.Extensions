using AutoEditor.Interfaces;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using System.Reflection;

namespace AutoEditor.Drawers;

[TypeDrawer(typeof(bool))]
public class BooleanDrawer : IPropertyControl
{
  public Control CreateController (PropertyInfo prop, object obj) {
    return new CheckBox {
      [!Avalonia.Controls.Primitives.ToggleButton.IsCheckedProperty] = new Binding(prop.Name) { Source = obj, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }
    }.WithLabel(Orientation.Horizontal, prop.Name);
  }
}
