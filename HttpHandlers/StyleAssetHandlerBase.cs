// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.HttpHandlers.StyleAssetHandlerBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.FileSystem;
using BundleTransformer.Core.Transformers;
using System;
using System.Linq;
using System.Web.Caching;
using System.Web.Optimization;

namespace BundleTransformer.Core.HttpHandlers
{
  /// <summary>
  /// Base class of the debugging HTTP handler that responsible for text output
  /// of processed style asset
  /// </summary>
  public abstract class StyleAssetHandlerBase : AssetHandlerBase
  {
    /// <summary>Gets a asset content type</summary>
    protected override string ContentType => BundleTransformer.Core.Constants.ContentType.Css;

    /// <summary>
    /// Constructs a instance of the debugging style HTTP handler
    /// </summary>
    /// <param name="cache">Server cache</param>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    /// <param name="assetHandlerConfig">Configuration settings of the debugging HTTP handler</param>
    protected StyleAssetHandlerBase(
      Cache cache,
      IVirtualFileSystemWrapper virtualFileSystemWrapper,
      AssetHandlerSettings assetHandlerConfig)
      : base(cache, virtualFileSystemWrapper, assetHandlerConfig)
    {
    }

    /// <summary>Gets a CSS transformer from bundle</summary>
    /// <param name="bundle">Bundle</param>
    /// <returns>CSS transformer</returns>
    protected override ITransformer GetTransformer(Bundle bundle)
    {
      IBundleTransform bundleTransform = (IBundleTransform) null;
      if (bundle != null)
        bundleTransform = bundle.Transforms.FirstOrDefault<IBundleTransform>((Func<IBundleTransform, bool>) (t => t is StyleTransformer));
      return (ITransformer) bundleTransform;
    }

    /// <summary>Gets a translator by name</summary>
    /// <typeparam name="T">Type of translator</typeparam>
    /// <param name="translatorName">Name of translator</param>
    /// <returns>Translator</returns>
    protected override T GetTranslatorByName<T>(string translatorName) => (T) BundleTransformerContext.Current.Styles.GetTranslatorInstance(translatorName);
  }
}
