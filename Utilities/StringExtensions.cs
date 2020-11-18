// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Utilities.StringExtensions
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;

namespace BundleTransformer.Core.Utilities
{
  /// <summary>Extensions for String</summary>
  public static class StringExtensions
  {
    /// <summary>Replaces a tabs by specified number of spaces</summary>
    /// <param name="source">String value</param>
    /// <param name="tabSize">Number of spaces in tab</param>
    /// <returns>Processed string value</returns>
    public static string TabsToSpaces(this string source, int tabSize) => source != null ? source.Replace("\t", "".PadRight(tabSize)) : throw new ArgumentNullException(nameof (source));

    /// <summary>
    /// Gets a character at the specified index from the string.
    /// A return value indicates whether the receiving succeeded.
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="index">The zero-based index of the character</param>
    /// <param name="result">When this method returns, contains the character from the string,
    /// if the receiving succeeded, or null character if the receiving failed.
    /// The receiving fails if the index out of bounds.</param>
    /// <returns>true if the character was received successfully; otherwise, false</returns>
    public static bool TryGetChar(this string source, int index, out char result)
    {
      int num = source != null ? source.Length : throw new ArgumentNullException(nameof (source));
      bool flag;
      if (num > 0 && index >= 0 && index < num)
      {
        result = source[index];
        flag = true;
      }
      else
      {
        result = char.MinValue;
        flag = false;
      }
      return flag;
    }
  }
}
