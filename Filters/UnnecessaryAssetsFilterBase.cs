// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Filters.UnnecessaryAssetsFilterBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BundleTransformer.Core.Filters
{
  /// <summary>
  /// Base class of filter is responsible for removal of unnecessary assets
  /// </summary>
  public abstract class UnnecessaryAssetsFilterBase : IFilter
  {
    /// <summary>
    /// List of regular expressions of files and directories that
    /// should be ignored when processing
    /// </summary>
    protected readonly List<Regex> _ignoreRegExps;

    /// <summary>Constructs a instance of unnecessary assets filter</summary>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    protected UnnecessaryAssetsFilterBase(string[] ignorePatterns)
    {
      if (ignorePatterns == null || ignorePatterns.Length == 0)
      {
        this._ignoreRegExps = new List<Regex>();
      }
      else
      {
        List<Regex> regexList = new List<Regex>();
        foreach (string ignorePattern in ignorePatterns)
        {
          if (!string.IsNullOrWhiteSpace(ignorePattern))
          {
            string str1 = ignorePattern.Trim();
            string str2 = !(str1 == "*") && !(str1 == "*.*") ? Regex.Escape(str1) : throw new ArgumentException(Strings.Assets_InvalidIgnorePattern, nameof (ignorePatterns));
            string pattern = str1.IndexOf("*", StringComparison.Ordinal) == -1 ? str2 + "$" : "^" + str2.Replace("\\*", "(?:.*)") + "$";
            regexList.Add(new Regex(pattern, RegexOptions.IgnoreCase));
          }
        }
        this._ignoreRegExps = regexList;
      }
    }

    /// <summary>Removes a unnecessary assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of necessary assets</returns>
    public abstract IList<IAsset> Transform(IList<IAsset> assets);

    /// <summary>Checks a whether asset is unnecessary</summary>
    /// <param name="assetVirtualPath">Asset virtual file path</param>
    /// <returns>Checking result (true - unnecessary; false - necessary)</returns>
    protected bool IsUnnecessaryAsset(string assetVirtualPath)
    {
      bool flag = false;
      foreach (Regex ignoreRegExp in this._ignoreRegExps)
      {
        if (ignoreRegExp.IsMatch(assetVirtualPath))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }
  }
}
