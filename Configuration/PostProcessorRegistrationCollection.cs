// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.PostProcessorRegistrationCollection
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Collection of registered postprocessors</summary>
  [ConfigurationCollection(typeof (PostProcessorRegistration))]
  public sealed class PostProcessorRegistrationCollection : ConfigurationElementCollection
  {
    /// <summary>Creates a new postprocessor registration</summary>
    /// <returns>Postprocessor registration</returns>
    protected override ConfigurationElement CreateNewElement() => (ConfigurationElement) new PostProcessorRegistration();

    /// <summary>
    /// Gets a key of the specified postprocessor registration
    /// </summary>
    /// <param name="element">Postprocessor registration</param>
    /// <returns>Key</returns>
    protected override object GetElementKey(ConfigurationElement element) => (object) ((AssetProcessorRegistrationBase) element).Name;

    /// <summary>
    /// Gets a postprocessor registration by postprocessor name
    /// </summary>
    /// <param name="name">Postprocessor name</param>
    /// <returns>Postprocessor registration</returns>
    public PostProcessorRegistration this[string name] => (PostProcessorRegistration) this.BaseGet((object) name);
  }
}
