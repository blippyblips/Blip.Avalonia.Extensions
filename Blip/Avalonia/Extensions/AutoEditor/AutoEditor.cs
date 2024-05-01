using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Blip.Avalonia.Extensions.AutoEditor.Controls;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using Splat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor;

// Sample on how to register a data source provider. Assuming you have any Collections in the classes you are trying to autoedit they will need a backing source and this is how you provide one.
//public class DatabaseSourceProvider : IDataSourceProvider {
//  public IEnumerable<object?> GetItems (Type type) {
//    using var _context = DatabaseContextFactory.CreateDbContext();
//    return _context.GetAllEntitiesOfType(type).AsEnumerable();
//  }
//}
// Locator.CurrentMutable.Register(() => new DatabaseSourceProvider(), typeof(IDataSourceProvider));

public class AutoEditorBase : VerticalStackPanelWithLastChildFill { }

public class AutoEditor : AutoEditorBase
{
  public AutoEditor()
  {
    PropertyChanged += (s, e) =>
    {
      if (e.Property == DataContextProperty)
      {
        Children.Clear();

        object? obj = e.NewValue;
        if (obj is IList list)
        {
          foreach (object? item in list)
          {
            Children.Add(GenerateControlsForProperties(item).WithStackPanel().WithBorder().WithScroll());
          }
          Children.Add(new ListButtonPanel(list));
        }
        else if (obj is IEnumerable enumerable)
        {
          foreach (object? item in enumerable)
          {
            Children.Add(GenerateControlsForProperties(item).WithStackPanel().WithBorder().WithScroll());
          }
        }
        else if (obj != null)
        {
          Children.Add(GenerateControlsForProperties(obj));
        }
      }
    };
  }


  private Control GenerateControlsForProperties(object obj, bool useTabGroups = true)
  {
    var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    var propertiesByGroup = GetGroupedProperties(properties);

    if (!useTabGroups || propertiesByGroup.Count == 1)
    {
      return CreateProperties([.. properties], obj);
    }

    TabControl tabControl = new();
    foreach (var groupProperties in propertiesByGroup)
    {
      var tabContent = CreateProperties(groupProperties.Value, obj);
      tabControl.Items.Add(new TabItem { Header = groupProperties.Key, Content = tabContent });
    }
    return tabControl;
  }

  private VerticalStackPanelWithLastChildFill CreateProperties(List<PropertyInfo> properties, object obj)
  {
    var panel = new VerticalStackPanelWithLastChildFill();
    foreach (var property in properties)
    {
      var control = ControlFactory.CreateControl(property, obj);
      if (control != null)
      {
        panel.Children.Add(control);
      }
    }
    return panel;
  }

  private static Dictionary<string, List<PropertyInfo>> GetGroupedProperties(PropertyInfo[] properties, string DefaultTab = "General")
  {
    var propertiesByGroup = new Dictionary<string, List<PropertyInfo>> {
      { DefaultTab, properties.Where(p => p.AttributeValue<TabGroupAttribute>() == null).ToList() }
    };
    foreach (var property in properties)
    {
      var tabGroup = property.AttributeValue<TabGroupAttribute>();
      if (tabGroup != null)
      {
        if (propertiesByGroup.TryGetValue(tabGroup.GroupName, out var group))
        {
          group.Add(property);
        }
        else
        {
          propertiesByGroup[tabGroup.GroupName] = [property];
        }
      }
    }
    return propertiesByGroup;
  }
}

public static class ControlFactory
{
  static ControlFactory()
  {
    var types = Assembly.GetExecutingAssembly().GetTypes().Where(type => Attribute.IsDefined(type, typeof(TypeDrawer)));
    foreach (var type in types)
    {
      if (type.GetCustomAttribute(typeof(TypeDrawer)) is TypeDrawer attribute)
      {
        foreach (var t in attribute.Types)
        {
          Locator.CurrentMutable.RegisterLazySingleton(() => type.Construct<IPropertyControl>(), typeof(IPropertyControl), t.Name);
        }
      }
    }
    // Check some folder or Resource for Xaml files to map and then map them by Filename to type for example Guid.axaml
    // Locator.CurrentMutable.RegisterLazySingleton(() => new XamlDrawer("Guid.axaml"), typeof(IPropertyControl), "Guid");
    // Locator.CurrentMutable.RegisterLazySingleton(() => new XamlDrawer(Filename), typeof(IPropertyControl), Path.GetFileNameWithoutExtension(Filename));
  }

  public static Control? CreateControl(PropertyInfo property, object obj)
  {
    var ctx = property.PropertyType.IsGenericType ?
         Locator.Current.GetService<IPropertyControl>(property.PropertyType.GetGenericTypeDefinition().Name) :
         Locator.Current.GetService<IPropertyControl>(property.PropertyType.Name);
    return ctx?.CreateController(property, obj);
  }
}