// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.AssetHandlerSettings
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>
  /// Configuration settings of the debugging HTTP handler, that responsible
  /// for text output of processed asset
  /// </summary>
  public sealed class AssetHandlerSettings : ConfigurationElement
  {
    /// <summary>
    /// Gets or sets a flag for whether to disable storage text content of
    /// processed asset in server cache
    /// </summary>
    [ConfigurationProperty("disableServerCache", DefaultValue = false)]
    public bool DisableServerCache
    {
      get => (bool) this["disableServerCache"];
      set => this["disableServerCache"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a duration of storage the text content of processed asset in
    /// server cache (in minutes)
    /// </summary>
    [ConfigurationProperty("serverCacheDurationInMinutes", DefaultValue = 15)]
    [IntegerValidator(ExcludeRange = false, MaxValue = 1440, MinValue = 1)]
    public int ServerCacheDurationInMinutes
    {
      get => (int) this["serverCacheDurationInMinutes"];
      set => this["serverCacheDurationInMinutes"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a flag for whether to disable storage text content of
    /// processed asset in browser cache
    /// </summary>
    [ConfigurationProperty("disableClientCache", DefaultValue = false)]
    public bool DisableClientCache
    {
      get => (bool) this["disableClientCache"];
      set => this["disableClientCache"] = (object) value;
    }
  }
}
