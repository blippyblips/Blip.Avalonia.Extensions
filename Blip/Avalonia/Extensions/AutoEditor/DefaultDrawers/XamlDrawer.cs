using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Blip.Avalonia.Extensions.AutoEditor;
using Blip.Avalonia.Extensions.AutoEditor.Interfaces;
using System.Reflection;

namespace Blip.Avalonia.Extensions.AutoEditor.DefaultDrawers;

public class XamlDrawer(string filename) : IPropertyControl
{
  public string Filename { get; set; } = filename;

  public Control CreateController(PropertyInfo prop, object obj)
  {
    //using var filestream = new FileStream(Filename, FileMode.Open);
    //AvaloniaRuntimeXamlLoader.Load(filestream); // Load from filestreams/memorystreams etc
    //var xaml = @"<UserControl xmlns='https://github.com/avaloniaui' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'><Button x:Name='button'>Foo</Button></UserControl>";
    //var control = AvaloniaRuntimeXamlLoader.Parse<UserControl>(xaml);
    //var button = control.FindControl<Button>("button");
    //return control;
    return new TextBlock() { Text = "Not yet implemented." }.WithLabel(prop.Name);
  }
}

