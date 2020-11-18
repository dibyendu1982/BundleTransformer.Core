// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.StyleSettings
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Configuration settings of processing style assets</summary>
  public sealed class StyleSettings : AssetSettingsBase
  {
    /// <summary>
    /// Gets or sets a ordered comma-separated list of names of default postprocessors
    /// </summary>
    [ConfigurationProperty("defaultPostProcessors", DefaultValue = "UrlRewritingCssPostProcessor")]
    public override string DefaultPostProcessors
    {
      get => (string) this["defaultPostProcessors"];
      set => this["defaultPostProcessors"] = (object) value;
    }
  }
}
