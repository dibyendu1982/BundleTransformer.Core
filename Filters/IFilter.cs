// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Filters.IFilter
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System.Collections.Generic;

namespace BundleTransformer.Core.Filters
{
  /// <summary>Defines interface of asset filter</summary>
  public interface IFilter
  {
    /// <summary>Performs processing of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of processed assets</returns>
    IList<IAsset> Transform(IList<IAsset> assets);
  }
}
