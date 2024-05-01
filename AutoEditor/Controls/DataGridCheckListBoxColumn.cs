using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using System.Collections;
using System.Collections.Generic;
using Ex;

namespace AutoEditor.Controls;

// Just a small extension to the DataGrid to let us support ComboBoxes (and potentially our own DropdownListCheckBox).
public class DataGridCheckListBoxColumn : DataGridTemplateColumn
{
  public static readonly StyledProperty<IEnumerable> ItemsSourceProperty = AvaloniaProperty.Register<DataGridComboBoxColumn, IEnumerable>(nameof(ItemsSource));
  public string ColumnName { get; set; }
  public IEnumerable ItemsSource { get => GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

  public DataGridCheckListBoxColumn (string columnName) {
    CellTemplate = CreateCellTemplate;
    ColumnName = columnName;
  }

  //Not used yet but would allow us to handle multi select inside grids too
  private IDataTemplate CreateCellTemplate => new FuncDataTemplate<object>((data, s) => new ExToggleableControl(new ExCheckListBox {
    ItemsSource = new List<object>(),
    HorizontalAlignment = HorizontalAlignment.Stretch,
    [!ItemsControl.ItemsSourceProperty] = this[!ItemsSourceProperty],
    [!Avalonia.Controls.Primitives.SelectingItemsControl.SelectedItemProperty] = new Binding(ColumnName) { Mode = BindingMode.TwoWay }
  }));
}