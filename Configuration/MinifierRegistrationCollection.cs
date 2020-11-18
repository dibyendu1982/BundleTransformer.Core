// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.MinifierRegistrationCollection
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Collection of registered minifiers</summary>
  [ConfigurationCollection(typeof (MinifierRegistration))]
  public sealed class MinifierRegistrationCollection : ConfigurationElementCollection
  {
    /// <summary>Creates a new minifier registration</summary>
    /// <returns>Minifier registration</returns>
    protected override ConfigurationElement CreateNewElement() => (ConfigurationElement) new MinifierRegistration();

    /// <summary>Gets a key of the specified minifier registration</summary>
    /// <param name="element">Minifier registration</param>
    /// <returns>Key</returns>
    protected override object GetElementKey(ConfigurationElement element) => (object) ((AssetProcessorRegistrationBase) element).Name;

    /// <summary>Gets a minifier registration by minifier name</summary>
    /// <param name="name">Minifier name</param>
    /// <returns>Minifier registration</returns>
    public MinifierRegistration this[string name] => (MinifierRegistration) this.BaseGet((object) name);
  }
}
