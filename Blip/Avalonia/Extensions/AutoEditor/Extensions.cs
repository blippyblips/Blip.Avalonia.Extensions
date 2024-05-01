using System;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor;

public static class Extensions
{
  public static T? Construct<T>(this Type type) where T : class
  {
    var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
    object? instance = constructor.Length > 0 ? Activator.CreateInstance(type) : type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null)?.Invoke(null);
    return instance as T;
  }

  public static U? AttributeValue<U>(this PropertyInfo property) where U : Attribute
  {
    return Attribute.IsDefined(property, typeof(U)) ? property.GetCustomAttribute(typeof(U)) as U : null;
  }

  public static Type BaseType(this PropertyInfo prop)
  {
    return prop.PropertyType.IsGenericType ? prop.PropertyType.GetGenericArguments()[0] : prop.PropertyType;
  }
}
