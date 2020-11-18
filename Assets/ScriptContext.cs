// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.ScriptContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Transformers;
using System;
using System.Web.Optimization;

namespace BundleTransformer.Core.Assets
{
  /// <summary>Script context</summary>
  public sealed class ScriptContext : AssetContextBase
  {
    /// <summary>Transformer</summary>
    private readonly Lazy<ScriptTransformer> _transformer = new Lazy<ScriptTransformer>();

    /// <summary>Gets a output code type</summary>
    protected override string OutputCodeType => "JS";

    /// <summary>Constructs a instance of script context</summary>
    /// <param name="scriptConfig">Configuration settings of processing script assets</param>
    public ScriptContext(ScriptSettings scriptConfig)
      : base((AssetSettingsBase) scriptConfig)
    {
    }

    /// <summary>Gets a instance of default transform</summary>
    /// <returns>Instance of transformer</returns>
    public override IBundleTransform GetDefaultTransformInstance() => (IBundleTransform) this._transformer.Value;
  }
}
