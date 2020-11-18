// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.FileExtensionRegistration
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>File extension registration</summary>
  public sealed class FileExtensionRegistration : ConfigurationElement
  {
    /// <summary>Gets or sets a file extension</summary>
    [ConfigurationProperty("fileExtension", IsKey = true, IsRequired = true)]
    public string FileExtension
    {
      get => (string) this["fileExtension"];
      set => this["fileExtension"] = (object) value;
    }

    /// <summary>Gets or sets a asset type code</summary>
    [ConfigurationProperty("assetTypeCode", IsRequired = true)]
    public string AssetTypeCode
    {
      get => (string) this["assetTypeCode"];
      set => this["assetTypeCode"] = (object) value;
    }
  }
}
