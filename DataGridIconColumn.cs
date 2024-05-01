using AutoEditor.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Metadata;

namespace Ex;

public class DataGridIconColumn : DataGridBoundColumn
{
  static DataGridIconColumn () {
    WidthProperty.OverrideDefaultValue<DataGridIconColumn>(new DataGridLength(20));
  }

  public DataGridIconColumn () {
    base.BindingTarget = Image.SourceProperty;
    Header = "";
  }


  protected override Control GenerateEditingElementDirect (DataGridCell cell, object dataItem) {
    var image = new Image {
      VerticalAlignment = VerticalAlignment.Center,
      HorizontalAlignment = HorizontalAlignment.Center,
      Width = 16,
    };
    if (Binding != null) {
      image.Bind(Image.SourceProperty, Binding);
    }
    return image;
  }

  protected override Control GenerateElement (DataGridCell cell, object dataItem) {
    var image = new Image {
      VerticalAlignment = VerticalAlignment.Center,
      HorizontalAlignment = HorizontalAlignment.Center,
      Width = 16,
    };
    if (Binding != null) {
      image.Bind(Image.SourceProperty, Binding);
    }
    return image;
  }

  protected override object PrepareCellForEdit (Control editingElement, RoutedEventArgs editingEventArgs) {
    return base.Binding;
  }
}