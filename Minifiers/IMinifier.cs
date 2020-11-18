// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Minifiers.IMinifier
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System.Collections.Generic;

namespace BundleTransformer.Core.Minifiers
{
  /// <summary>Defines a interface of asset minifier</summary>
  public interface IMinifier
  {
    /// <summary>Minify a text content of asset</summary>
    /// <param name="asset">Asset</param>
    /// <returns>Asset with minified text content</returns>
    IAsset Minify(IAsset asset);

    /// <summary>Minify a text content of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of assets with minified text content</returns>
    IList<IAsset> Minify(IList<IAsset> assets);
  }
}
