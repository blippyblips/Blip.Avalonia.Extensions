using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Metadata;
using DynamicData;
using System;
using System.Collections;
using System.Reactive.Linq;

namespace Ex;

public class ExCheckListBox : ListBox
{
  protected override Type StyleKeyOverride => typeof(ListBox);

  public new static readonly DirectProperty<SelectingItemsControl, IList?> SelectedItemsProperty;

  [InheritDataTypeFromItems("ItemsSource")]
  private static FuncDataTemplate<object?> DefaultPanel => new((data, s) => {
    if (data == null) { return null; }
    var result = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0.0), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
    var text = new TextBlock { Text = data.ToString(), HorizontalAlignment = HorizontalAlignment.Center };
    var check = new CheckBox() { [!ToggleButton.IsCheckedProperty] = new Binding("IsSelected") { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor) { AncestorType = typeof(ListBoxItem) } }, };
    result.Children.Add([check, text]);
    return result;
  }, true);

  static ExCheckListBox () {
    SelectedItemsProperty = SelectingItemsControl.SelectedItemsProperty;
    SelectionModeProperty.OverrideDefaultValue<ExCheckListBox>(SelectionMode.Multiple | SelectionMode.Toggle);
    VerticalAlignmentProperty.OverrideDefaultValue<ExCheckListBox>(VerticalAlignment.Center);
    ItemTemplateProperty.OverrideDefaultValue<ExCheckListBox>(DefaultPanel);
    MinHeightProperty.OverrideDefaultValue<ExCheckListBox>(100);
  }
}
