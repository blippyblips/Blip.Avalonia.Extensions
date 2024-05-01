using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Blip.Avalonia.Extensions.AutoEditor;
using Blip.Avalonia.Extensions.AutoEditor.Controls;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using Splat;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.DefaultDrawers;

//TODO: Enumerables/Collections are another base case and probably shouldnt use the typedrawer system.
[TypeDrawer(typeof(IEnumerable<>), typeof(ObservableCollection<>))]
public class IEnumerableDrawer : IPropertyControl
{
#pragma warning disable CS8600,CS8604 //TODO
  public Control CreateController(PropertyInfo prop, object obj)
  {
    var collectionAttr = prop.AttributeValue<DrawAsCollectionAttribute>();
    // Handle single selected value with backing collection and multiple selected values with backing collection
    if (collectionAttr != null && (collectionAttr.CollectionType == CollectionType.Grid || collectionAttr.CollectionType == CollectionType.LargeGrid))
    {
      return CreateGrid(prop, obj, collectionAttr.CollectionType == CollectionType.LargeGrid ? 125 : double.NaN);
    }
    else
    {
      return CreateCheckListBox(prop, obj);
    }
  }

  private static Control CreateCheckListBox(PropertyInfo prop, object obj)
  {
    var box = new ExCheckListBox
    {
      [!ItemsControl.ItemsSourceProperty] = new Binding("") { Source = GetItemSourceValues(prop), Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger.Default },
      SelectedItems = (IList)prop.GetValue(obj)
    };
    //NOTE: Id love to inline this in the object initalizer(construction) but if you try to do this any other way it will overwrite the original source with an empty list.
    box[!ExCheckListBox.SelectedItemsProperty] = new Binding(prop.Name, BindingMode.TwoWay) { Source = obj, UpdateSourceTrigger = UpdateSourceTrigger.Default };

    var toggleableControl = new ExToggleableControl(box);
    toggleableControl.ToggleButton.Content = string.Join(", ", (IEnumerable<object>)prop.GetValue(obj)); // Update one time at start
    box.SelectionChanged += (sender, args) => { toggleableControl.ToggleButton.Content = string.Join(", ", (IEnumerable<object>)prop.GetValue(obj)); };
    return toggleableControl;
  }

  private static Control CreateGrid(PropertyInfo prop, object obj, double RowHeight = double.NaN)
  {
    DockPanel panel = new() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, };
    var buttonPanel = new ListButtonPanel((IList)prop.GetValue(obj));
    panel.Children.Add(buttonPanel);
    DockPanel.SetDock(buttonPanel, Dock.Top);

    var grid = new AutoDataGrid(prop.BaseType())
    {
      ContextMenu = new ContextMenu { ItemsSource = new[] { new MenuItem { Header = "Copy" } } },
      ItemsSource = prop.GetValue(obj) as IEnumerable,
      RowHeight = RowHeight
    };
    grid[!DataGrid.ItemsSourceProperty] = new Binding(prop.Name, BindingMode.TwoWay) { Source = obj, UpdateSourceTrigger = UpdateSourceTrigger.Default };
    panel.Children.Add(grid.WithScroll());
    return panel;
  }

  private static IEnumerable<object> GetItemSourceValues(PropertyInfo prop) => Locator.Current.GetService<IDataSourceProvider>()!.GetItems(prop.BaseType())!;
#pragma warning restore CS8600,CS8604 // Converting null literal or possible null value to non-nullable type.
}
