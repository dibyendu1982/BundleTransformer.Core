// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Translators.ITranslator
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System.Collections.Generic;

namespace BundleTransformer.Core.Translators
{
  /// <summary>
  /// This interface used in implementation of containers for
  /// translators of code written on intermediate languages
  /// (LESS, Sass, SCSS, CoffeeScript, TypeScript and etc) to CSS or JS code
  /// </summary>
  public interface ITranslator
  {
    /// <summary>
    /// Gets or sets a flag that web application is in debug mode
    /// </summary>
    bool IsDebugMode { get; set; }

    /// <summary>
    /// Translates a code of asset written on intermediate language to CSS or JS code
    /// </summary>
    /// <param name="asset">Asset with code written on intermediate language</param>
    /// <returns>Asset with translated code</returns>
    IAsset Translate(IAsset asset);

    /// <summary>
    /// Translates a code of assets written on intermediate languages to CSS or JS code
    /// </summary>
    /// <param name="assets">Set of assets with code written on intermediate languages</param>
    /// <returns>Set of assets with translated code</returns>
    IList<IAsset> Translate(IList<IAsset> assets);
  }
}
