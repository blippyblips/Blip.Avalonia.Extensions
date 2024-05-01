using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections;
using System;

namespace Blip.Avalonia.Extensions.AutoEditor.Controls;

public class ListButtonPanel : StackPanel
{
  public static readonly string EditIconPath = "M362.7 19.3L314.3 67.7 444.3 197.7l48.4-48.4c25-25 25-65.5 0-90.5L453.3 19.3c-25-25-65.5-25-90.5 0zm-71 71L58.6 323.5c-10.4 10.4-18 23.3-22.2 37.4L1 481.2C-1.5 489.7 .8 498.8 7 505s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L421.7 220.3 291.7 90.3z";
  public static readonly string DeleteIconPath = "M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z";
  public static readonly string AddIconPath = "M12 7C12.4142 7 12.75 7.33579 12.75 7.75V11.25H16.25C16.6642 11.25 17 11.5858 17 12C17 12.4142 16.6642 12.75 16.25 12.75H12.75V16.25C12.75 16.6642 12.4142 17 12 17C11.5858 17 11.25 16.6642 11.25 16.25V12.75H7.75C7.33579 12.75 7 12.4142 7 12C7 11.5858 7.33579 11.25 7.75 11.25H11.25V7.75C11.25 7.33579 11.5858 7 12 7Z M3 6.25C3 4.45507 4.45507 3 6.25 3H17.75C19.5449 3 21 4.45507 21 6.25V17.75C21 19.5449 19.5449 21 17.75 21H6.25C4.45507 21 3 19.5449 3 17.75V6.25ZM6.25 4.5C5.2835 4.5 4.5 5.2835 4.5 6.25V17.75C4.5 18.7165 5.2835 19.5 6.25 19.5H17.75C18.7165 19.5 19.5 18.7165 19.5 17.75V6.25C19.5 5.2835 18.7165 4.5 17.75 4.5H6.25Z";

  protected override Type StyleKeyOverride => typeof(StackPanel);

  static ListButtonPanel()
  {
    OrientationProperty.OverrideDefaultValue<ListButtonPanel>(Orientation.Horizontal);
    HorizontalAlignmentProperty.OverrideDefaultValue<ListButtonPanel>(HorizontalAlignment.Right);
    SpacingProperty.OverrideDefaultValue<ListButtonPanel>(2);
  }

  public ListButtonPanel(IList target)
  {
    var newButton = new Button { Content = new PathIcon() { Data = PathGeometry.Parse(AddIconPath) }, Margin = new Thickness(1, 0, 1, 0) };
    var editButton = new Button { Content = new PathIcon() { Data = PathGeometry.Parse(EditIconPath) }, Margin = new Thickness(1, 0, 1, 0) };
    var deleteButton = new Button { Content = new PathIcon() { Data = PathGeometry.Parse(DeleteIconPath) }, Margin = new Thickness(1, 0, 1, 0) };
    newButton.Click += (sender, e) => { target.Add(Activator.CreateInstance(target.GetType().GetGenericArguments()[0])); };
    Children.Add(newButton);
    Children.Add(editButton);
    Children.Add(deleteButton);
  }
}
