// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.ConfigurationContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;
using System.Configuration;

namespace BundleTransformer.Core.Configuration
{
  /// <summary>Defines interface of configuration context</summary>
  public sealed class ConfigurationContext : IConfigurationContext
  {
    /// <summary>Configuration settings of core</summary>
    private readonly Lazy<CoreSettings> _coreConfig = new Lazy<CoreSettings>((Func<CoreSettings>) (() => (CoreSettings) ConfigurationManager.GetSection("bundleTransformer/core")));

    /// <summary>Gets a core configuration settings</summary>
    /// <returns>Configuration settings of core</returns>
    public CoreSettings GetCoreSettings() => this._coreConfig.Value;
  }
}
