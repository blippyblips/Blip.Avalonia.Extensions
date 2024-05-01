using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Metadata;
using Avalonia.Styling;
using Avalonia.Controls.Chrome;

namespace Blip.Avalonia.Extensions;

/// <summary>
/// Draws window minimize / maximize / close buttons in a <see cref="TitleBar"/> when managed client decorations are enabled.
/// </summary>
[TemplatePart(PART_CloseButton, typeof(Button))]
[TemplatePart(PART_RestoreButton, typeof(Button))]
[TemplatePart(PART_MinimizeButton, typeof(Button))]
[TemplatePart(PART_FullScreenButton, typeof(Button))]
[TemplatePart(PART_ThemeButton, typeof(Button))]
[PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
public class ExCaptionButtons : CaptionButtons
{
  //protected override Type StyleKeyOverride => typeof(CaptionButtons);

  private const string PART_CloseButton = "PART_CloseButton";
  private const string PART_RestoreButton = "PART_RestoreButton";
  private const string PART_MinimizeButton = "PART_MinimizeButton";
  private const string PART_FullScreenButton = "PART_FullScreenButton";
  private const string PART_ThemeButton = "PART_ThemeButton";

  public bool IsCancel
  {
    get => GetValue(IsCancelProperty);
    set => SetValue(IsCancelProperty, value);
  }
  public static readonly StyledProperty<bool> IsCancelProperty =
           AvaloniaProperty.Register<ExTitleBar, bool>(nameof(IsCancel));

  public void OnTheme()
  {
    if (Application.Current != null)
    {
      Application.Current.RequestedThemeVariant = Application.Current.RequestedThemeVariant == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
    }
  }

  protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
  {
    base.OnApplyTemplate(e);

    if (e.NameScope.Find<Button>(PART_ThemeButton) is { } themeButton)
    {
      themeButton.Click += (sender, e) => OnTheme();
    }
  }
}