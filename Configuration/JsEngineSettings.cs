// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.JsEngineSettings
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Configuration settings of JS engine</summary>
  public sealed class JsEngineSettings : ConfigurationElement
  {
    /// <summary>Gets or sets a JS engine name</summary>
    [ConfigurationProperty("name", DefaultValue = "")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }
  }
}
