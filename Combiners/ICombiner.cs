// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Combiners.ICombiner
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System.Collections.Generic;

namespace BundleTransformer.Core.Combiners
{
  /// <summary>Defines interface of asset combiner</summary>
  public interface ICombiner
  {
    /// <summary>
    /// Gets or sets a flag that web application is in debug mode
    /// </summary>
    bool IsDebugMode { get; set; }

    /// <summary>Gets or sets a flag for whether to enable tracing</summary>
    bool EnableTracing { get; set; }

    /// <summary>Combines a text content of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <returns>Combined asset</returns>
    IAsset Combine(IList<IAsset> assets, string bundleVirtualPath);
  }
}
