// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.FileExtensionRegistrationCollection
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Collection of registered file extensions</summary>
  [ConfigurationCollection(typeof (FileExtensionRegistration))]
  public sealed class FileExtensionRegistrationCollection : ConfigurationElementCollection
  {
    /// <summary>Creates a new file extension registration</summary>
    /// <returns>File extension registration</returns>
    protected override ConfigurationElement CreateNewElement() => (ConfigurationElement) new FileExtensionRegistration();

    /// <summary>
    /// Gets a key of the specified file extension registration
    /// </summary>
    /// <param name="element">File extension registration</param>
    /// <returns>Key</returns>
    protected override object GetElementKey(ConfigurationElement element) => (object) ((FileExtensionRegistration) element).FileExtension;

    /// <summary>Gets a file extension registration by file extension</summary>
    /// <param name="name">File extension name</param>
    /// <returns>File extension registration</returns>
    public FileExtensionRegistration this[string name] => (FileExtensionRegistration) this.BaseGet((object) name);
  }
}
