// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.PostProcessors.UrlRewritingCssPostProcessor
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using AdvancedStringBuilder;
using BundleTransformer.Core.Assets;
using BundleTransformer.Core.FileSystem;
using BundleTransformer.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BundleTransformer.Core.PostProcessors
{
  /// <summary>
  /// Postprocessor that responsible for transformation of relative
  /// paths in CSS files to absolute
  /// </summary>
  public sealed class UrlRewritingCssPostProcessor : PostProcessorBase
  {
    /// <summary>Relative path resolver</summary>
    private readonly IRelativePathResolver _relativePathResolver;
    /// <summary>
    /// Regular expression for working with CSS <code>@import</code> rules
    /// </summary>
    private static readonly Regex _cssImportRuleRegex = new Regex("@import\\s*(?<quote>'|\")(?<url>[\\w \\-+.:,;/?&=%~#$@()\\[\\]{}]+)(\\k<quote>)", RegexOptions.IgnoreCase);

    /// <summary>
    /// Constructs a instance of URL rewriting CSS postprocessor
    /// </summary>
    public UrlRewritingCssPostProcessor()
      : this(BundleTransformerContext.Current.FileSystem.GetCommonRelativePathResolver())
    {
    }

    /// <summary>
    /// Constructs a instance of URL rewriting CSS postprocessor
    /// </summary>
    /// <param name="relativePathResolver">Relative path resolver</param>
    public UrlRewritingCssPostProcessor(IRelativePathResolver relativePathResolver) => this._relativePathResolver = relativePathResolver;

    /// <summary>Transforms relative paths to absolute in CSS file</summary>
    /// <param name="asset">CSS asset</param>
    /// <returns>Processed CSS asset</returns>
    public override IAsset PostProcess(IAsset asset)
    {
      if (asset == null)
        throw new ArgumentNullException(nameof (asset), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (asset)));
      this.InnerPostProcess(asset);
      return asset;
    }

    /// <summary>Transforms relative paths to absolute in CSS files</summary>
    /// <param name="assets">Set of CSS assets</param>
    /// <returns>Set of processed CSS assets</returns>
    public override IList<IAsset> PostProcess(IList<IAsset> assets)
    {
      if (assets == null)
        throw new ArgumentNullException(nameof (assets), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (assets)));
      if (assets.Count == 0)
        return assets;
      List<IAsset> list = assets.Where<IAsset>((Func<IAsset, bool>) (a => a.IsStylesheet && !a.RelativePathsResolved)).ToList<IAsset>();
      if (list.Count == 0)
        return assets;
      foreach (IAsset asset in list)
        this.InnerPostProcess(asset);
      return assets;
    }

    private void InnerPostProcess(IAsset asset)
    {
      string url = asset.Url;
      string str = this.ResolveAllRelativePaths(asset.Content, url);
      asset.Content = str;
      asset.RelativePathsResolved = true;
    }

    /// <summary>Transforms all relative paths to absolute in CSS code</summary>
    /// <param name="content">Text content of CSS asset</param>
    /// <param name="path">CSS file path</param>
    /// <returns>Processed text content of CSS asset</returns>
    public string ResolveAllRelativePaths(string content, string path)
    {
      int length = content.Length;
      if (length == 0)
        return content;
      MatchCollection matchCollection1 = CommonRegExps.CssUrlRuleRegex.Matches(content);
      MatchCollection matchCollection2 = UrlRewritingCssPostProcessor._cssImportRuleRegex.Matches(content);
      if (matchCollection1.Count == 0 && matchCollection2.Count == 0)
        return content;
      List<CssNodeMatch> source = new List<CssNodeMatch>();
      foreach (Match match in matchCollection1)
      {
        CssNodeMatch cssNodeMatch = new CssNodeMatch(match.Index, match.Length, CssNodeType.UrlRule, match);
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
            case CssNodeType.ImportRule:
            case CssNodeType.UrlRule:
              UrlRewritingCssPostProcessor.ProcessOtherContent(stringBuilder, content, ref currentPosition, position);
              if (nodeType == CssNodeType.UrlRule)
              {
                GroupCollection groups = match.Groups;
                string assetUrl = groups["url"].Value.Trim();
                string quote = groups["quote"].Success ? groups["quote"].Value : string.Empty;
                string str1 = match.Value;
                string str2 = this.ProcessUrlRule(path, assetUrl, quote);
                stringBuilder.Append(str2);
                currentPosition += str1.Length;
                continue;
              }
              if (nodeType == CssNodeType.ImportRule)
              {
                string assetUrl = match.Groups["url"].Value.Trim();
                string str1 = match.Value;
                string str2 = this.ProcessImportRule(path, assetUrl);
                stringBuilder.Append(str2);
                currentPosition += str1.Length;
                continue;
              }
              continue;
            case CssNodeType.MultilineComment:
              int nextPosition = position + match.Length;
              UrlRewritingCssPostProcessor.ProcessOtherContent(stringBuilder, content, ref currentPosition, nextPosition);
              continue;
            default:
              continue;
          }
        }
      }
      if (currentPosition > 0 && currentPosition <= num)
        UrlRewritingCssPostProcessor.ProcessOtherContent(stringBuilder, content, ref currentPosition, num + 1);
      string str = stringBuilder.ToString();
      shared.Return(stringBuilder);
      return str;
    }

    /// <summary>
    /// Process a CSS <code>@import</code> rule
    /// </summary>
    /// <param name="parentAssetUrl">URL of parent CSS asset file</param>
    /// <param name="assetUrl">URL of CSS asset file</param>
    /// <returns>Processed CSS <code>@import</code> rule</returns>
    private string ProcessImportRule(string parentAssetUrl, string assetUrl)
    {
      string str = assetUrl;
      if (!UrlHelpers.StartsWithProtocol(assetUrl) && !UrlHelpers.StartsWithDataUriScheme(assetUrl))
        str = this._relativePathResolver.ResolveRelativePath(parentAssetUrl, assetUrl);
      return string.Format("@import \"{0}\"", (object) str);
    }

    /// <summary>
    /// Process a CSS <code>url</code> rule
    /// </summary>
    /// <param name="parentAssetUrl">URL of parent CSS asset file</param>
    /// <param name="assetUrl">URL of CSS asset file</param>
    /// <param name="quote">Quote</param>
    /// <returns>Processed CSS <code>url</code> rule</returns>
    private string ProcessUrlRule(string parentAssetUrl, string assetUrl, string quote)
    {
      string str = assetUrl;
      if (!UrlHelpers.StartsWithProtocol(assetUrl) && !UrlHelpers.StartsWithDataUriScheme(assetUrl))
        str = this._relativePathResolver.ResolveRelativePath(parentAssetUrl, assetUrl);
      return string.Format("url({0}{1}{0})", (object) quote, (object) str);
    }

    /// <summary>Process a other stylesheet content</summary>
    /// <param name="contentBuilder">Content builder</param>
    /// <param name="assetContent">Text content of CSS asset</param>
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
