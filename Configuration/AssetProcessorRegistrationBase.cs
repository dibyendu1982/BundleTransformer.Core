// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.AssetProcessorRegistrationBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Configuration settings of asset processor</summary>
  public abstract class AssetProcessorRegistrationBase : ConfigurationElement
  {
    /// <summary>Gets or sets processor name</summary>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets processor .NET type name</summary>
    [ConfigurationProperty("type", IsRequired = true)]
    public string Type
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
    }
  }
}
