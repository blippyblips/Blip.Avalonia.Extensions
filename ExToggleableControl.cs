using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using System;

namespace Ex;

// What it says on the tin, lets you hide another control behind a ToggleButton.
// Can assign/bind text to the ToggleButton.Content after creating.
public class ExToggleableControl : StackPanel
{
  public ToggleButton ToggleButton { get; init; }

  public object? Content {
    get { return GetValue(ContentProperty); }
    set { SetValue(ContentProperty, value); }
  }
  public static readonly StyledProperty<object?> ContentProperty = AvaloniaProperty.Register<ExTitleBar, object?>("Content");


  static ExToggleableControl () {
    HorizontalAlignmentProperty.OverrideDefaultValue<ExToggleableControl>(HorizontalAlignment.Left);
    VerticalAlignmentProperty.OverrideDefaultValue<ExToggleableControl>(VerticalAlignment.Stretch);
    MarginProperty.OverrideDefaultValue<ExToggleableControl>(new Thickness(10));
  }

  [Obsolete("temporary duplication to continue supporting passing as argument too.")]
  public ExToggleableControl (Control control) {
    ToggleButton = new ToggleButton() {
      IsChecked = false, MinWidth = 300,
      HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Left,
      VerticalContentAlignment = VerticalAlignment.Center, BorderThickness = new Thickness(1), Height = 28,
      CornerRadius = new CornerRadius(1), Margin = new Thickness(0), FontSize = 11, FontWeight = FontWeight.DemiBold
    };
    ToggleButton.IsCheckedChanged += (sender, e) => control.IsVisible = (sender as ToggleButton)?.IsChecked ?? false;

    Children.Add(ToggleButton);
    Children.Add(control);
  }

  // Possible improvement later would be to take the measured width of the content as the width of the button.
  // Also forwarding settings to button could be good.
  public ExToggleableControl () {
    ToggleButton = new ToggleButton() {
      IsChecked = false, MinWidth = 300,
      HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Left,
      VerticalContentAlignment = VerticalAlignment.Center, BorderThickness = new Thickness(1), Height = 28,
      CornerRadius = new CornerRadius(1), Margin = new Thickness(0), FontSize = 11, FontWeight = FontWeight.DemiBold
    };
    var control = new ContentControl() { Content = Content, IsVisible = false };
    ToggleButton.IsCheckedChanged += (sender, e) => control.IsVisible = (sender as ToggleButton)?.IsChecked ?? false;

    Children.Add(ToggleButton);
    Children.Add(control);
  }

  protected override Type StyleKeyOverride => typeof(StackPanel);
}
