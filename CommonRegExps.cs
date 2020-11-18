// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.CommonRegExps
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Text.RegularExpressions;

namespace BundleTransformer.Core
{
  /// <summary>Common regular expressions</summary>
  public static class CommonRegExps
  {
    /// <summary>
    /// Regular expression for working with the CSS multiline comments
    /// </summary>
    public static readonly Regex CssMultilineCommentRegex = new Regex("/\\*[^*]*\\*+(?:[^/][^*]*\\*+)*/");
    /// <summary>
    /// Regular expression for working with CSS <code>@charset</code> rules
    /// </summary>
    public static readonly Regex CssCharsetRuleRegex = new Regex("@charset\\s*(?<quote>'|\")(?<charset>[A-Za-z0-9\\-]+)(\\k<quote>)\\s*;", RegexOptions.IgnoreCase);
    /// <summary>
    /// Regular expression for working with CSS <code>url</code> rule
    /// </summary>
    public static readonly Regex CssUrlRuleRegex = new Regex("url\\(\\s*(?:(?<quote>'|\")(?<url>[\\w \\-+.:,;/?&=%~#$@()\\[\\]{}]+)(\\k<quote>)|(?<url>[\\w\\-+.:,;/?&=%~#$@\\[\\]{}]+))\\s*\\)", RegexOptions.IgnoreCase);
  }
}
