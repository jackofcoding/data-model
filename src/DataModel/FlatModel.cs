using System.Collections.Concurrent;

namespace DataModel;

public class FlatModel
{
  private readonly ConcurrentDictionary<string, object?> _data = new();
  
  public void SetValue<T>(string key, T? value)
  {
    if (String.IsNullOrEmpty(key))
      throw new ArgumentNullException(nameof(key), "Key cannot be null or empty.");
    if (key.TrimStart().Length != key.Length || key.TrimEnd().Length != key.Length)
      throw new ArgumentException("Key cannot have surrounding whitespace.", nameof(key));
    
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
