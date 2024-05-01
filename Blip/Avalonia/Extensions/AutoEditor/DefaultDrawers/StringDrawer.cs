using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using Blip.Avalonia.Extensions.AutoEditor;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.DefaultDrawers;

[TypeDrawer(typeof(string))]
public class StringDrawer : IPropertyControl
{
  public Control CreateController(PropertyInfo prop, object obj)
  {
    var controller = new TextBox
    {
      [!TextBox.TextProperty] = new Binding(prop.Name) { Source = obj, Mode = BindingMode.TwoWay },
      VerticalAlignment = VerticalAlignment.Stretch,
      HorizontalAlignment = HorizontalAlignment.Stretch,
      TextWrapping = TextWrapping.Wrap,
      AcceptsReturn = true,
      MaxLength = prop.AttributeValue<StringLengthAttribute>()?.MaximumLength ?? 300,
    };
    return controller.WithScroll().WithLabel(prop.Name);
  }
}
