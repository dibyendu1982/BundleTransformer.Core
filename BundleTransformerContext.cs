// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.BundleTransformerContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.FileSystem;
using System;
using System.Web.Optimization;

namespace BundleTransformer.Core
{
  /// <summary>Bundle transformer context</summary>
  public sealed class BundleTransformerContext : IBundleTransformerContext
  {
    /// <summary>Instance of default bundle transformer context</summary>
    private static readonly Lazy<BundleTransformerContext> _default = new Lazy<BundleTransformerContext>((Func<BundleTransformerContext>) (() => new BundleTransformerContext()));
    /// <summary>Instance of current bundle transformer context</summary>
    private static IBundleTransformerContext _current;

    /// <summary>Gets a instance of bundle transformer context</summary>
    public static IBundleTransformerContext Current
    {
      get => BundleTransformerContext._current ?? (IBundleTransformerContext) BundleTransformerContext._default.Value;
      set => BundleTransformerContext._current = value;
    }

    /// <summary>Gets a configuration context</summary>
    public IConfigurationContext Configuration { get; private set; }

    /// <summary>Gets a file system context</summary>
    public IFileSystemContext FileSystem { get; private set; }

    /// <summary>Gets a style context</summary>
    public IAssetContext Styles { get; private set; }

    /// <summary>Gets a script context</summary>
    public IAssetContext Scripts { get; private set; }

    /// <summary>Gets a flag that web application is in debug mode</summary>
    public bool IsDebugMode => !BundleTable.EnableOptimizations;

    /// <summary>
    /// Private constructor for implementation Singleton pattern
    /// </summary>
    private BundleTransformerContext()
    {
      ConfigurationContext configurationContext = new ConfigurationContext();
      CoreSettings coreSettings = configurationContext.GetCoreSettings();
      this.Configuration = (IConfigurationContext) configurationContext;
      this.FileSystem = (IFileSystemContext) new FileSystemContext();
      this.Styles = (IAssetContext) new StyleContext(coreSettings.Styles);
      this.Scripts = (IAssetContext) new ScriptContext(coreSettings.Scripts);
    }
  }
}
