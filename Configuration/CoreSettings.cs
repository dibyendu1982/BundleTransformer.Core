// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.CoreSettings
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Configuration settings of core</summary>
  public sealed class CoreSettings : ConfigurationSection
  {
    /// <summary>Gets or sets a flag for whether to enable tracing</summary>
    [ConfigurationProperty("enableTracing", DefaultValue = false)]
    public bool EnableTracing
    {
      get => (bool) this["enableTracing"];
      set => this["enableTracing"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a list of JS files with Microsoft-style extensions
    /// </summary>
    [ConfigurationProperty("jsFilesWithMicrosoftStyleExtensions", DefaultValue = "MicrosoftAjax.js,MicrosoftMvcAjax.js,MicrosoftMvcValidation.js,knockout-$version$.js")]
    public string JsFilesWithMicrosoftStyleExtensions
    {
      get => (string) this["jsFilesWithMicrosoftStyleExtensions"];
      set => this["jsFilesWithMicrosoftStyleExtensions"] = (object) value;
    }

    /// <summary>
    /// Gets a configuration settings of processing style assets
    /// </summary>
    [ConfigurationProperty("css")]
    public StyleSettings Styles => this["css"] as StyleSettings;

    /// <summary>
    /// Gets a configuration settings of processing script assets
    /// </summary>
    [ConfigurationProperty("js")]
    public ScriptSettings Scripts => this["js"] as ScriptSettings;

    /// <summary>
    /// Gets a configuration settings of the debugging HTTP handler, that responsible
    /// for text output of processed asset
    /// </summary>
    [ConfigurationProperty("assetHandler")]
    public AssetHandlerSettings AssetHandler => this["assetHandler"] as AssetHandlerSettings;
  }
}
