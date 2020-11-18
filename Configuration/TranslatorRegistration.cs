// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.TranslatorRegistration
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Translator registration</summary>
  public sealed class TranslatorRegistration : AssetProcessorRegistrationBase
  {
    /// <summary>Gets or sets a flag for whether to enable translator</summary>
    [ConfigurationProperty("enabled", DefaultValue = true)]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }
  }
}
