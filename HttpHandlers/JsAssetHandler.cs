// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.HttpHandlers.JsAssetHandler
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.FileSystem;
using System.Web;
using System.Web.Caching;

namespace BundleTransformer.Core.HttpHandlers
{
  /// <summary>
  /// Debugging HTTP handler that responsible for text output
  /// of translated JS asset
  /// </summary>
  public sealed class JsAssetHandler : ScriptAssetHandlerBase
  {
    /// <summary>Gets a value indicating whether asset is static</summary>
    protected override bool IsStaticAsset => true;

    /// <summary>
    /// Constructs a instance of the debugging JS HTTP handler
    /// </summary>
    public JsAssetHandler()
      : this(HttpContext.Current.Cache, BundleTransformerContext.Current.FileSystem.GetVirtualFileSystemWrapper(), BundleTransformerContext.Current.Configuration.GetCoreSettings().AssetHandler)
    {
    }

    /// <summary>
    /// Constructs a instance of the debugging JS HTTP handler
    /// </summary>
    /// <param name="cache">Server cache</param>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    /// <param name="assetHandlerConfig">Configuration settings of the debugging HTTP handler</param>
    public JsAssetHandler(
      Cache cache,
      IVirtualFileSystemWrapper virtualFileSystemWrapper,
      AssetHandlerSettings assetHandlerConfig)
      : base(cache, virtualFileSystemWrapper, assetHandlerConfig)
    {
    }

    /// <summary>
    /// Removes a additional file extension from path of specified JS asset
    /// </summary>
    /// <param name="assetPath">Path of JS asset</param>
    /// <returns>Path of JS asset without additional file extension</returns>
    protected override string RemoveAdditionalFileExtension(string assetPath) => Asset.RemoveAdditionalJsFileExtension(assetPath);
  }
}
