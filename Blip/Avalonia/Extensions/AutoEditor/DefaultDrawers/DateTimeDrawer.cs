using Avalonia.Controls;
using Blip.Avalonia.Extensions.AutoEditor;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using System;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.DefaultDrawers;

[TypeDrawer(typeof(DateTime?), typeof(DateTimeOffset?), typeof(DateTimeOffset), typeof(DateTime))]
public class DateTimeDrawer : IPropertyControl
{
  public Control CreateController(PropertyInfo prop, object obj)
  {
    var date = new CalendarDatePicker
    {
      DisplayDate = prop.GetValue(obj) as DateTime? ?? DateTime.Today,
      SelectedDate = prop.GetValue(obj) as DateTime?
    }.WithLabel(prop.Name);
    return date;
  }
}
