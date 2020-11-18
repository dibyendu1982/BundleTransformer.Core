// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Configuration.IConfigurationContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.Configuration
{
  public interface IConfigurationContext
  {
    /// <summary>Gets a core configuration settings</summary>
    /// <returns>Configuration settings of core</returns>
    CoreSettings GetCoreSettings();
  }
}
