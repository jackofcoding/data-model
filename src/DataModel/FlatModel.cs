using System.Collections.Concurrent;

namespace DataModel;

public class FlatModel
{
  private readonly ConcurrentDictionary<string, object?> _data = new();
  
  public void SetValue<T>(string key, T? value)
  {
    if (String.IsNullOrEmpty(key) || !Constants.FlatModelKeyPattern().IsMatch(key))
      throw new ArgumentException("Invalid key format.", nameof(key));
    
    _data.AddOrUpdate(key, _ => value, (_, _) => value);
  }

  public T? GetValue<T>(string key, out bool success)
  {
    success = _data.TryGetValue(key, out object? value);
    if (success && value is T result)
      return result;

    success = false;
    return default;
  }
}
