﻿// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Filters.ScriptDuplicateAssetsFilter
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Constants;
using BundleTransformer.Core.Resources;
using System;
using System.Collections.Generic;

namespace BundleTransformer.Core.Filters
{
  /// <summary>
  /// Filter that responsible for removal of duplicate script assets
  /// </summary>
  public sealed class ScriptDuplicateAssetsFilter : IFilter
  {
    /// <summary>Removes a duplicate script assets</summary>
    /// <param name="assets">Set of script assets</param>
    /// <returns>Set of unique script assets</returns>
    public IList<IAsset> Transform(IList<IAsset> assets)
    {
      if (assets == null)
        throw new ArgumentNullException(nameof (assets), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (assets)));
      if (assets.Count <= 1)
        return assets;
      List<IAsset> assetList = new List<IAsset>();
      List<string> stringList = new List<string>();
      foreach (IAsset asset in (IEnumerable<IAsset>) assets)
      {
        string assetVirtualPath = asset.VirtualPath;
        if (asset.AssetTypeCode == AssetTypeCode.JavaScript)
          assetVirtualPath = Asset.RemoveAdditionalJsFileExtension(assetVirtualPath);
        string upperInvariant = assetVirtualPath.ToUpperInvariant();
        if (!stringList.Contains(upperInvariant))
        {
          assetList.Add(asset);
          stringList.Add(upperInvariant);
        }
      }
      stringList.Clear();
      return (IList<IAsset>) assetList;
    }
  }
}
