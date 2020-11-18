// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Filters.ScriptUnnecessaryAssetsFilter
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Resources;
using System;
using System.Collections.Generic;

namespace BundleTransformer.Core.Filters
{
  /// <summary>
  /// Filter that responsible for removal of unnecessary script assets
  /// </summary>
  public sealed class ScriptUnnecessaryAssetsFilter : UnnecessaryAssetsFilterBase
  {
    /// <summary>
    /// Constructs a instance of unnecessary script assets filter
    /// </summary>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    public ScriptUnnecessaryAssetsFilter(string[] ignorePatterns)
      : base(ignorePatterns)
    {
    }

    /// <summary>Removes a unnecessary script assets</summary>
    /// <param name="assets">Set of script assets</param>
    /// <returns>Set of necessary script assets</returns>
    public override IList<IAsset> Transform(IList<IAsset> assets)
    {
      if (assets == null)
        throw new ArgumentNullException(nameof (assets), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (assets)));
      if (assets.Count == 0 || this._ignoreRegExps == null || this._ignoreRegExps.Count == 0)
        return assets;
      List<IAsset> assetList = new List<IAsset>();
      foreach (IAsset asset in (IEnumerable<IAsset>) assets)
      {
        if (!this.IsUnnecessaryAsset(Asset.RemoveAdditionalJsFileExtension(asset.VirtualPath)))
          assetList.Add(asset);
      }
      return (IList<IAsset>) assetList;
    }
  }
}
