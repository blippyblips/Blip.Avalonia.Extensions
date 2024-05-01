using Avalonia.Controls;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.Interfaces;

public interface IPropertyControl
{
  /// <summary> Returns a controller for prop.GetValue(obj).</summary>
  /// <param name="prop">The PropertyInfo for the Type that we are creating a controller for</param>
  /// <param name="obj">The object or its parent if a class</param>
  /// <returns>The new controller</returns>
  Control CreateController(PropertyInfo prop, object obj);
}
