// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Combiners.CombinerBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BundleTransformer.Core.Combiners
{
  /// <summary>Base class of asset combiner</summary>
  public abstract class CombinerBase : ICombiner
  {
    /// <summary>
    /// Gets or sets a flag that web application is in debug mode
    /// </summary>
    public bool IsDebugMode { get; set; }

    /// <summary>Gets or sets a flag for whether to enable tracing</summary>
    public bool EnableTracing { get; set; }

    /// <summary>Combines a text content of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <returns>Combined asset</returns>
    public IAsset Combine(IList<IAsset> assets, string bundleVirtualPath)
    {
      int count = assets.Count;
      bool flag1;
      bool flag2;
      switch (count)
      {
        case 0:
          throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (assets)), nameof (assets));
        case 1:
          IAsset asset1 = assets[0];
          flag1 = asset1.Minified;
          flag2 = asset1.RelativePathsResolved;
          break;
        default:
          int num1 = assets.Count<IAsset>((Func<IAsset, bool>) (a => a.Minified));
          int num2 = assets.Count<IAsset>((Func<IAsset, bool>) (a => a.RelativePathsResolved));
          flag1 = num1 == count;
          int num3 = count;
          flag2 = num2 == num3;
          break;
      }
      Asset asset2 = new Asset(this.GenerateCombinedAssetVirtualPath(bundleVirtualPath));
      asset2.Content = this.CombineAssetContent(assets);
      asset2.Combined = true;
      asset2.Minified = flag1;
      asset2.RelativePathsResolved = flag2;
      asset2.OriginalAssets = assets;
      asset2.VirtualPathDependencies = this.CombineAssetVirtualPathDependencies(assets);
      return (IAsset) asset2;
    }

    protected abstract string GenerateCombinedAssetVirtualPath(string bundleVirtualPath);

    protected abstract string CombineAssetContent(IList<IAsset> assets);

    protected IList<string> CombineAssetVirtualPathDependencies(IList<IAsset> assets)
    {
      List<string> stringList = new List<string>();
      bool isDebugMode = this.IsDebugMode;
      foreach (IAsset asset in (IEnumerable<IAsset>) assets)
      {
        stringList.Add(asset.VirtualPath);
        if (!isDebugMode && asset.VirtualPathDependencies.Count > 0)
          stringList.AddRange((IEnumerable<string>) asset.VirtualPathDependencies);
      }
      return (IList<string>) stringList;
    }
  }
}
