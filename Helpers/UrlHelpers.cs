// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Helpers.UrlHelpers
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using AdvancedStringBuilder;
using BundleTransformer.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BundleTransformer.Core.Helpers
{
  /// <summary>URL helpers</summary>
  public static class UrlHelpers
  {
    /// <summary>Regular expression for determine protocol in URL</summary>
    private static readonly Regex _protocolRegExp = new Regex("^(?:(?:https?|ftp)\\://)|(?://)", RegexOptions.IgnoreCase);
    /// <summary>
    /// Regular expression for working with multiple forward slashes
    /// </summary>
    private static readonly Regex _multipleForwardSlashesRegex = new Regex("/{2,}");

    /// <summary>
    /// Determines whether the beginning of this url matches the protocol
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>Result of check (true - is starts with the protocol;
    /// false - is not starts with the protocol)</returns>
    public static bool StartsWithProtocol(string url) => UrlHelpers._protocolRegExp.IsMatch(url);

    /// <summary>
    /// Determines whether the beginning of this url matches the data URI scheme
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>Result of check (true - is starts with the data URI scheme;
    /// false - is not starts with the data URI scheme)</returns>
    public static bool StartsWithDataUriScheme(string url) => url.StartsWith("data:", StringComparison.OrdinalIgnoreCase);

    /// <summary>Converts a back slashes to forward slashes</summary>
    /// <param name="url">URL with back slashes</param>
    /// <returns>URL with forward slashes</returns>
    public static string ProcessBackSlashes(string url)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (url)));
      return string.IsNullOrWhiteSpace(url) ? url : url.Replace('\\', '/');
    }

    /// <summary>Removes a first slash from URL</summary>
    /// <param name="url">URL</param>
    /// <returns>URL without the first slash</returns>
    public static string RemoveFirstSlash(string url)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (url)));
      if (string.IsNullOrWhiteSpace(url) || !url.StartsWith("/"))
        return url;
      return url.TrimStart('/');
    }

    /// <summary>Removes a last slash from URL</summary>
    /// <param name="url">URL</param>
    /// <returns>URL without the last slash</returns>
    public static string RemoveLastSlash(string url)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (url)));
      if (string.IsNullOrWhiteSpace(url) || !url.EndsWith("/"))
        return url;
      return url.TrimEnd('/');
    }

    /// <summary>Finds a last directory seperator</summary>
    /// <param name="url">URL</param>
    /// <returns>Position of last directory seperator</returns>
    private static int FindLastDirectorySeparator(string url)
    {
      int val1 = url != null ? url.LastIndexOf('/') : throw new ArgumentNullException(nameof (url), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (url)));
      int val2 = url.LastIndexOf('\\');
      return val1 == -1 || val2 == -1 ? (val1 == -1 ? val2 : val1) : Math.Max(val1, val2);
    }

    /// <summary>Gets a directory name for the specified URL</summary>
    /// <param name="url">URL</param>
    /// <returns>The string containing directory name for URL</returns>
    public static string GetDirectoryName(string url)
    {
      int num = url != null ? UrlHelpers.FindLastDirectorySeparator(url) : throw new ArgumentNullException(nameof (url), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (url)));
      return num == -1 ? string.Empty : url.Substring(0, num + 1);
    }

    /// <summary>Gets a file name and extension of the specified URL</summary>
    /// <param name="url">URL</param>
    /// <returns>The consisting of the characters after the last directory character in URL</returns>
    public static string GetFileName(string url)
    {
      int num = url != null ? UrlHelpers.FindLastDirectorySeparator(url) : throw new ArgumentNullException(nameof (url), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (url)));
      return num == -1 ? url : url.Substring(num + 1);
    }

    /// <summary>Normalizes a URL</summary>
    /// <param name="url">URL</param>
    /// <returns>Normalized URL</returns>
    public static string Normalize(string url)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (url)));
      if (string.IsNullOrWhiteSpace(url))
        return url;
      string input = url;
      if (input.IndexOf("./", StringComparison.Ordinal) != -1)
      {
        string[] strArray = input.Split('/');
        int length = strArray.Length;
        if (length == 0)
          return url;
        List<string> stringList = new List<string>();
        for (int index1 = 0; index1 < length; ++index1)
        {
          string str = strArray[index1];
          if (!(str == ".."))
          {
            if (!(str == "."))
              stringList.Add(str);
          }
          else
          {
            int count = stringList.Count;
            int index2 = count - 1;
            if (count == 0 || stringList[index2] == "..")
              stringList.Add(str);
            else
              stringList.RemoveAt(index2);
          }
        }
        input = string.Join("/", (IEnumerable<string>) stringList);
        stringList.Clear();
      }
      return UrlHelpers._multipleForwardSlashesRegex.Replace(input, "/");
    }

    /// <summary>Combines a two URLs</summary>
    /// <param name="baseUrl">The base URL</param>
    /// <param name="relativeUrl">The relative URL to add to the base URL</param>
    /// <returns>The absolute URL</returns>
    public static string Combine(string baseUrl, string relativeUrl)
    {
      if (baseUrl == null)
        throw new ArgumentNullException(nameof (baseUrl), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (baseUrl)));
      if (relativeUrl == null)
        throw new ArgumentNullException(nameof (relativeUrl), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (relativeUrl)));
      string str1 = UrlHelpers.ProcessBackSlashes(baseUrl);
      string str2 = UrlHelpers.ProcessBackSlashes(relativeUrl);
      string str3 = str1;
      if (str3.EndsWith("/"))
      {
        if (str2.StartsWith("/"))
          str2 = str2.TrimStart('/');
      }
      else if (!str2.StartsWith("/"))
        str3 += "/";
      return UrlHelpers.Normalize(str3 + str2);
    }

    /// <summary>
    /// Converts a long string (more than 65 519 characters) to its escaped representation
    /// </summary>
    /// <param name="stringToEscape">The long string to escape</param>
    /// <returns>Escaped representation of long string</returns>
    public static string EscapeLongDataString(string stringToEscape)
    {
      int length = stringToEscape.Length;
      if (length <= 65519)
        return Uri.EscapeDataString(stringToEscape);
      StringBuilderPool shared = StringBuilderPool.Shared;
      StringBuilder builder = shared.Rent();
      int num = length / 65519;
      for (int index = 0; index <= num; ++index)
      {
        int startIndex = 65519 * index;
        string stringToEscape1 = index < num ? stringToEscape.Substring(startIndex, 65519) : stringToEscape.Substring(startIndex);
        builder.Append(Uri.EscapeDataString(stringToEscape1));
      }
      string str = builder.ToString();
      shared.Return(builder);
      return str;
    }
  }
}
