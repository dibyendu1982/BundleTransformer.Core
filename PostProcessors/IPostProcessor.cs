// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.PostProcessors.IPostProcessor
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System.Collections.Generic;

namespace BundleTransformer.Core.PostProcessors
{
  /// <summary>
  /// Defines interface of asset postprocessor (runs after translators and before minifiers)
  /// </summary>
  public interface IPostProcessor
  {
    /// <summary>
    /// Gets or sets a flag for whether to use postprocessor in the debugging HTTP handlers
    /// </summary>
    bool UseInDebugMode { get; set; }

    /// <summary>Postprocess a text content of asset</summary>
    /// <param name="asset">Asset</param>
    /// <returns>Asset with processed text content</returns>
    IAsset PostProcess(IAsset asset);

    /// <summary>Postprocess a text content of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of assets with processed code</returns>
    IList<IAsset> PostProcess(IList<IAsset> assets);
  }
}
