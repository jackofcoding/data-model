using System.Text.RegularExpressions;

namespace DataModel;

public static partial class Constants
{
  [GeneratedRegex(@"^[a-zA-Z0-9]+\z", RegexOptions.None, 100)]
  public static partial Regex FlatModelKeyPattern();
}
