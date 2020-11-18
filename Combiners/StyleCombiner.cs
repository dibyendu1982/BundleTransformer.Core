// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Combiners.StyleCombiner
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using AdvancedStringBuilder;
using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BundleTransformer.Core.Combiners
{
  /// <summary>Style asset combiner</summary>
  public sealed class StyleCombiner : CombinerBase
  {
    /// <summary>
    /// Regular expression for working with CSS <code>@import</code> rules
    /// </summary>
    private static readonly Regex _cssImportRuleRegex = new Regex("@import\\s*(?:(?:(?<quote>'|\")(?<url>[\\w \\-+.:,;/?&=%~#$@()\\[\\]{}]+)(\\k<quote>))|(?:url\\(\\s*(?:(?<quote>'|\")(?<url>[\\w \\-+.:,;/?&=%~#$@()\\[\\]{}]+)(\\k<quote>)|(?<url>[\\w\\-+.:,;/?&=%~#$@\\[\\]{}]+))\\s*\\)))(?:\\s*(?<media>(?:[A-Za-z]+|\\([A-Za-z][^,;()\"']+?\\))(?:\\s*and\\s+\\([A-Za-z][^,;()\"']+?\\))*(?:\\s*,\\s*(?:[A-Za-z]+|\\([A-Za-z][^,;()\"']+?\\))(?:\\s*and\\s+\\([A-Za-z][^,;()\"']+?\\))*\\s*)*?))?\\s*;", RegexOptions.IgnoreCase);

    protected override string GenerateCombinedAssetVirtualPath(string bundleVirtualPath)
    {
      string str = bundleVirtualPath.TrimEnd();
      string css = FileExtension.Css;
      if (!str.EndsWith(css, StringComparison.OrdinalIgnoreCase))
        str += css;
      return str;
    }

    protected override string CombineAssetContent(IList<IAsset> assets)
    {
      StringBuilderPool shared = StringBuilderPool.Shared;
      StringBuilder stringBuilder = shared.Rent();
      string empty = string.Empty;
      List<string> stringList = new List<string>();
      int count = assets.Count;
      int num = count - 1;
      for (int index = 0; index < count; ++index)
      {
        IAsset asset = assets[index];
        if (this.EnableTracing)
          stringBuilder.AppendFormatLine("/*#region URL: {0} */", (object) asset.Url);
        stringBuilder.Append(StyleCombiner.EjectCssCharsetAndImports(asset.Content, ref empty, (IList<string>) stringList));
        if (this.EnableTracing)
        {
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("/*#endregion*/");
        }
        if (index != num)
          stringBuilder.AppendLine();
      }
      if (stringList.Count > 0)
      {
        string format = this.EnableTracing ? "/*#region CSS Imports */{0}{1}{0}/*#endregion*/{0}{0}" : "{1}{0}";
        stringBuilder.Insert(0, string.Format(format, (object) Environment.NewLine, (object) string.Join(Environment.NewLine, (IEnumerable<string>) stringList)));
      }
      if (!string.IsNullOrWhiteSpace(empty))
        stringBuilder.Insert(0, empty + Environment.NewLine);
      string str = stringBuilder.ToString();
      shared.Return(stringBuilder);
      return str;
    }

    /// <summary>
    /// Eject a <code>@charset</code> and <code>@import</code> rules
    /// </summary>
    /// <param name="content">Text content of style asset</param>
    /// <param name="topCharset">Processed top <code>@charset</code> rule</param>
    /// <param name="imports">List of processed <code>@import</code> rules</param>
    /// <returns>Text content of style asset without <code>@charset</code> and <code>@import</code> rules</returns>
    private static string EjectCssCharsetAndImports(
      string content,
      ref string topCharset,
      IList<string> imports)
    {
      int length = content.Length;
      if (length == 0)
        return content;
      MatchCollection matchCollection1 = CommonRegExps.CssCharsetRuleRegex.Matches(content);
      MatchCollection matchCollection2 = StyleCombiner._cssImportRuleRegex.Matches(content);
      if (matchCollection1.Count == 0 && matchCollection2.Count == 0)
        return content;
      List<CssNodeMatch> source = new List<CssNodeMatch>();
      foreach (Match match in matchCollection1)
      {
        CssNodeMatch cssNodeMatch = new CssNodeMatch(match.Index, match.Length, CssNodeType.CharsetRule, match);
        source.Add(cssNodeMatch);
      }
      foreach (Match match in matchCollection2)
      {
        CssNodeMatch cssNodeMatch = new CssNodeMatch(match.Index, match.Length, CssNodeType.ImportRule, match);
        source.Add(cssNodeMatch);
      }
      foreach (Match match in CommonRegExps.CssMultilineCommentRegex.Matches(content))
      {
        CssNodeMatch cssNodeMatch = new CssNodeMatch(match.Index, match.Length, CssNodeType.MultilineComment, match);
        source.Add(cssNodeMatch);
      }
      List<CssNodeMatch> list = source.OrderBy<CssNodeMatch, int>((Func<CssNodeMatch, int>) (n => n.Position)).ThenByDescending<CssNodeMatch, int>((Func<CssNodeMatch, int>) (n => n.Length)).ToList<CssNodeMatch>();
      StringBuilderPool shared = StringBuilderPool.Shared;
      StringBuilder stringBuilder = shared.Rent();
      int num = length - 1;
      int currentPosition = 0;
      foreach (CssNodeMatch cssNodeMatch in list)
      {
        CssNodeType nodeType = cssNodeMatch.NodeType;
        int position = cssNodeMatch.Position;
        Match match = cssNodeMatch.Match;
        if (position >= currentPosition)
        {
          switch (nodeType)
          {
            case CssNodeType.CharsetRule:
            case CssNodeType.ImportRule:
              StyleCombiner.ProcessOtherContent(stringBuilder, content, ref currentPosition, position);
              if (nodeType == CssNodeType.CharsetRule)
              {
                string str1 = match.Groups["charset"].Value;
                string str2 = match.Value;
                if (string.IsNullOrWhiteSpace(topCharset))
                  topCharset = string.Format("@charset \"{0}\";", (object) str1);
                currentPosition += str2.Length;
                continue;
              }
              if (nodeType == CssNodeType.ImportRule)
              {
                GroupCollection groups = match.Groups;
                string str1 = groups["url"].Value;
                string str2 = groups["media"].Success ? " " + groups["media"].Value : string.Empty;
                string str3 = match.Value;
                string str4 = string.Format("@import \"{0}\"{1};", (object) str1, (object) str2);
                imports.Add(str4);
                currentPosition += str3.Length;
                continue;
              }
              continue;
            case CssNodeType.MultilineComment:
              int nextPosition = position + match.Length;
              StyleCombiner.ProcessOtherContent(stringBuilder, content, ref currentPosition, nextPosition);
              continue;
            default:
              continue;
          }
        }
      }
      if (currentPosition > 0 && currentPosition <= num)
        StyleCombiner.ProcessOtherContent(stringBuilder, content, ref currentPosition, num + 1);
      string str = stringBuilder.ToString();
      shared.Return(stringBuilder);
      return str;
    }

    /// <summary>Process a other stylesheet content</summary>
    /// <param name="contentBuilder">Content builder</param>
    /// <param name="assetContent">Text content of style asset</param>
    /// <param name="currentPosition">Current position</param>
    /// <param name="nextPosition">Next position</param>
    private static void ProcessOtherContent(
      StringBuilder contentBuilder,
      string assetContent,
      ref int currentPosition,
      int nextPosition)
    {
      if (nextPosition <= currentPosition)
        return;
      string str = assetContent.Substring(currentPosition, nextPosition - currentPosition);
      contentBuilder.Append(str);
      currentPosition = nextPosition;
    }
  }
}
