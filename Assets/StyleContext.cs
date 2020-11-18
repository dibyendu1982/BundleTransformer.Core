// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.StyleContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Transformers;
using System;
using System.Web.Optimization;

namespace BundleTransformer.Core.Assets
{
  /// <summary>Style context</summary>
  public sealed class StyleContext : AssetContextBase
  {
    /// <summary>Transformer</summary>
    private readonly Lazy<StyleTransformer> _transformer = new Lazy<StyleTransformer>();

    /// <summary>Gets a output code type</summary>
    protected override string OutputCodeType => "CSS";

    /// <summary>Constructs a instance of style context</summary>
    /// <param name="styleConfig">Configuration settings of processing style assets</param>
    public StyleContext(StyleSettings styleConfig)
      : base((AssetSettingsBase) styleConfig)
    {
    }

    /// <summary>Gets a instance of default transform</summary>
    /// <returns>Instance of transformer</returns>
    public override IBundleTransform GetDefaultTransformInstance() => (IBundleTransform) this._transformer.Value;
  }
}
