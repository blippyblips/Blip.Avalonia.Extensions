using System;
using Avalonia.Reactive;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Reactive.Disposables;

namespace Ex;

[TemplatePart("PART_ExCaptionButtons", typeof(ExCaptionButtons), IsRequired = true)]
[PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
public class ExTitleBar : TemplatedControl
{
  private CompositeDisposable? _disposables;
  private ExCaptionButtons? _captionButtons;

  public object? InnerLeftContent {
    get { return GetValue(InnerLeftContentProperty); }
    set { SetValue(InnerLeftContentProperty, value); }
  }
  public static readonly StyledProperty<object?> InnerLeftContentProperty = AvaloniaProperty.Register<ExTitleBar, object?>("InnerLeftContent");

  public object? InnerRightContent {
    get { return GetValue(InnerRightContentProperty); }
    set { SetValue(InnerRightContentProperty, value); }
  }
  public static readonly StyledProperty<object?> InnerRightContentProperty = AvaloniaProperty.Register<ExTitleBar, object?>("InnerRightContent");

  public bool IsDialog {
    get => GetValue(IsDialogProperty);
    set => SetValue(IsDialogProperty, value);
  }
  public static readonly StyledProperty<bool> IsDialogProperty = AvaloniaProperty.Register<ExTitleBar, bool>(nameof(IsDialog));

  public ExTitleBar () {
  }

  private void UpdateSize (Window window) {
    Margin = new Thickness(
        window.OffScreenMargin.Left,
        window.OffScreenMargin.Top,
        window.OffScreenMargin.Right,
        window.OffScreenMargin.Bottom);

    if (window.WindowState != WindowState.FullScreen) {
      Height = window.WindowDecorationMargin.Top;

      if (_captionButtons != null) {
        _captionButtons.Height = Height;
      }
    }

    IsVisible = true;
  }

  protected override void OnApplyTemplate (TemplateAppliedEventArgs e) {
    base.OnApplyTemplate(e);

    _captionButtons?.Detach();

    _captionButtons = e.NameScope.Get<ExCaptionButtons>("PART_ExCaptionButtons");

    if (VisualRoot is Window window) {
      _captionButtons?.Attach(window);
      UpdateSize(window);
    }
  }



  /// <inheritdoc />
  protected override void OnAttachedToVisualTree (VisualTreeAttachmentEventArgs e) {
    base.OnAttachedToVisualTree(e);

    if (VisualRoot is Window window) {
      _disposables = new CompositeDisposable(6)
      {
                    window.GetObservable(Window.WindowDecorationMarginProperty).Subscribe(_ => UpdateSize(window)),
                    window.GetObservable(Window.ExtendClientAreaTitleBarHeightHintProperty).Subscribe(_ => UpdateSize(window)),
                    window.GetObservable(Window.OffScreenMarginProperty).Subscribe(_ => UpdateSize(window)),
                    window.GetObservable(Window.ExtendClientAreaChromeHintsProperty).Subscribe(_ => UpdateSize(window)),
                    window.GetObservable(Window.WindowStateProperty).Subscribe(x => {
                            PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                            PseudoClasses.Set(":normal", x == WindowState.Normal);
                            PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                            PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                        }),
                    window.GetObservable(Window.IsExtendedIntoWindowDecorationsProperty).Subscribe(_ => UpdateSize(window))
                };
    }
  }

  /// <inheritdoc />
  protected override void OnDetachedFromVisualTree (VisualTreeAttachmentEventArgs e) {
    base.OnDetachedFromVisualTree(e);

    _disposables?.Dispose();

    _captionButtons?.Detach();
    _captionButtons = null;
  }
}