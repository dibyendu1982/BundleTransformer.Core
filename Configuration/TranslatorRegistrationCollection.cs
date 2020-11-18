// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.TranslatorRegistrationCollection
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Collection of registered translators</summary>
  [ConfigurationCollection(typeof (TranslatorRegistration))]
  public sealed class TranslatorRegistrationCollection : ConfigurationElementCollection
  {
    /// <summary>Creates a new translator registration</summary>
    /// <returns>Translator registration</returns>
    protected override ConfigurationElement CreateNewElement() => (ConfigurationElement) new TranslatorRegistration();

    /// <summary>Gets a key of the specified translator registration</summary>
    /// <param name="element">Translator registration</param>
    /// <returns>Key</returns>
    protected override object GetElementKey(ConfigurationElement element) => (object) ((AssetProcessorRegistrationBase) element).Name;

    /// <summary>Gets a translator registration by translator name</summary>
    /// <param name="name">Translator name</param>
    /// <returns>Translator registration</returns>
    public TranslatorRegistration this[string name] => (TranslatorRegistration) this.BaseGet((object) name);
  }
}
