using Avalonia.Controls;
using Avalonia;
using Avalonia.Layout;
using System.ComponentModel;
using Avalonia.Media;
using Splat;
using AutoEditor.Interfaces;
using Avalonia.Styling;
using System;

namespace AutoEditor.Controls;

public class AutoDataGrid : DataGrid
{
  protected override Type StyleKeyOverride => typeof(DataGrid);

  static AutoDataGrid() {
    IsReadOnlyProperty.OverrideDefaultValue<AutoDataGrid>(false);
    CanUserReorderColumnsProperty.OverrideDefaultValue<AutoDataGrid>(true);
    CanUserResizeColumnsProperty.OverrideDefaultValue<AutoDataGrid>(true);
    CanUserSortColumnsProperty.OverrideDefaultValue<AutoDataGrid>(false);
    GridLinesVisibilityProperty.OverrideDefaultValue<AutoDataGrid>(DataGridGridLinesVisibility.All);
    BorderThicknessProperty.OverrideDefaultValue<AutoDataGrid>(new Thickness(1));
    BorderBrushProperty.OverrideDefaultValue<AutoDataGrid>(Brushes.Gray);
    MinWidthProperty.OverrideDefaultValue<AutoDataGrid>(200);
    MinHeightProperty.OverrideDefaultValue<AutoDataGrid>(200);
    MaxColumnWidthProperty.OverrideDefaultValue<AutoDataGrid>(500);
    VerticalAlignmentProperty.OverrideDefaultValue<AutoDataGrid>(VerticalAlignment.Stretch);
    AutoGenerateColumnsProperty.OverrideDefaultValue<AutoDataGrid>(true);
    MarginProperty.OverrideDefaultValue<AutoDataGrid>(new Thickness(20));

    var app = Application.Current ?? throw new NullReferenceException("Application not found");
    if (app.Styles.TryGetResource("SystemBaseHighColor", app.ActualThemeVariant, out var background1)) {
    if(app.Styles.TryGetResource("TextControlBackground", app.ActualThemeVariant, out var background2)) {
      var evens = new Style(x => x.OfType<DataGridRow>().NthLastChild(2, 0)) { Setters = { new Setter(Button.BackgroundProperty, background1 as IBrush), } };
      var odds = new Style(x => x.OfType<DataGridRow>().NthLastChild(2, 1)) { Setters = { new Setter(Button.BackgroundProperty, background2 as IBrush), } };
      app.Styles.Add(evens);
        app.Styles.Add(odds);
      }
    }
  }

  public AutoDataGrid (Type type) {
    AutoGeneratingColumn += AutoGeneratingEntity(type);
    
  }

  public static EventHandler<DataGridAutoGeneratingColumnEventArgs> AutoGeneratingEntity (Type itemType) => (sender, e) => {
    if (e.PropertyName.Contains("Id")) { e.Cancel = true; }

    var columnProperty = itemType.GetProperty(e.PropertyName);
    if (columnProperty != null && columnProperty.AttributeValue<ReadOnlyAttribute>() != null) { e.Column.IsReadOnly = true; }
    var drawAsCollectionAttribute = columnProperty?.AttributeValue<DrawAsCollectionAttribute>();
    if (columnProperty != null && drawAsCollectionAttribute != null) {
      var itemSource = Locator.Current.GetService<IDataSourceProvider>()!.GetItems(columnProperty.BaseType());
      //TODO: Maybe implement checking for single vs multi select and use the appropriate template.
      e.Column = new DataGridComboBoxColumn(e.PropertyName) { Header = e.PropertyName, ItemsSource = itemSource };
    }
  };
}
