using System;
using System.Collections.Generic;

namespace AutoEditor.Interfaces;

public interface IDataSourceProvider
{
  /// <summary>Provides a backing source to SelectingItemsControls like ListBox.</summary>
  /// <param name="type">The type we want all the available options for</param>
  /// <returns>An enumerable of all options or empty enumerable if none are available</returns>
  IEnumerable<object?> GetItems (Type type);
}
