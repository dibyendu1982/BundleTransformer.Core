// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.AssetSettingsBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Configuration settings of processing assets</summary>
  public abstract class AssetSettingsBase : ConfigurationElement
  {
    /// <summary>Gets or sets a name of default minifier</summary>
    [ConfigurationProperty("defaultMinifier", DefaultValue = "NullMinifier")]
    public string DefaultMinifier
    {
      get => (string) this["defaultMinifier"];
      set => this["defaultMinifier"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a ordered comma-separated list of names of default postprocessors
    /// </summary>
    public abstract string DefaultPostProcessors { get; set; }

    /// <summary>
    /// Gets or sets a flag for whether to allow usage of pre-minified files
    /// </summary>
    [ConfigurationProperty("usePreMinifiedFiles", DefaultValue = true)]
    public bool UsePreMinifiedFiles
    {
      get => (bool) this["usePreMinifiedFiles"];
      set => this["usePreMinifiedFiles"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a flag for whether to allow combine files before minification
    /// </summary>
    [ConfigurationProperty("combineFilesBeforeMinification", DefaultValue = false)]
    public bool CombineFilesBeforeMinification
    {
      get => (bool) this["combineFilesBeforeMinification"];
      set => this["combineFilesBeforeMinification"] = (object) value;
    }

    /// <summary>Gets a list of registered translators</summary>
    [ConfigurationProperty("translators", IsRequired = true)]
    public TranslatorRegistrationCollection Translators => (TranslatorRegistrationCollection) this["translators"];

    /// <summary>Gets a list of registered postprocessors</summary>
    [ConfigurationProperty("postProcessors", IsRequired = true)]
    public PostProcessorRegistrationCollection PostProcessors => (PostProcessorRegistrationCollection) this["postProcessors"];

    /// <summary>Gets a list of registered minifiers</summary>
    [ConfigurationProperty("minifiers", IsRequired = true)]
    public MinifierRegistrationCollection Minifiers => (MinifierRegistrationCollection) this["minifiers"];

    /// <summary>Gets a list of registered file extensions</summary>
    [ConfigurationProperty("fileExtensions", IsRequired = true)]
    public FileExtensionRegistrationCollection FileExtensions => (FileExtensionRegistrationCollection) this["fileExtensions"];
  }
}
