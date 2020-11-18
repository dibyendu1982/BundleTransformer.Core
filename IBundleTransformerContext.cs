// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.IBundleTransformerContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.FileSystem;

namespace BundleTransformer.Core
{
  /// <summary>Defines interface of bundle transformer context</summary>
  public interface IBundleTransformerContext
  {
    /// <summary>Gets a configuration context</summary>
    IConfigurationContext Configuration { get; }

    /// <summary>Gets a file system context</summary>
    IFileSystemContext FileSystem { get; }

    /// <summary>Gets a style context</summary>
    IAssetContext Styles { get; }

    /// <summary>Gets a script context</summary>
    IAssetContext Scripts { get; }

    /// <summary>Gets a flag that web application is in debug mode</summary>
    bool IsDebugMode { get; }
  }
}
