using AutoEditor.Interfaces;
using Avalonia.Controls;
using System;
using System.Reflection;

namespace AutoEditor.Drawers;

[TypeDrawer(typeof(DateTime?), typeof(DateTimeOffset?), typeof(DateTimeOffset), typeof(DateTime))]
public class DateTimeDrawer : IPropertyControl
{
  public Control CreateController (PropertyInfo prop, object obj) {
    var date = new CalendarDatePicker { 
      DisplayDate = prop.GetValue(obj) as DateTime? ?? DateTime.Today,
      SelectedDate = prop.GetValue(obj) as DateTime?
    }.WithLabel(prop.Name);
    return date;
  }
}
