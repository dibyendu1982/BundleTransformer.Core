// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Transformers.TransformerBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Minifiers;
using BundleTransformer.Core.PostProcessors;
using BundleTransformer.Core.Translators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Hosting;
using System.Web.Optimization;

namespace BundleTransformer.Core.Transformers
{
  /// <summary>
  /// Base class of transformer that responsible for processing assets
  /// </summary>
  public abstract class TransformerBase : ITransformer, IBundleTransform
  {
    /// <summary>
    /// List of patterns of files and directories that
    /// should be ignored when processing
    /// </summary>
    protected readonly string[] _ignorePatterns;
    /// <summary>List of translators</summary>
    protected ReadOnlyCollection<ITranslator> _translators;
    /// <summary>List of postprocessors</summary>
    protected ReadOnlyCollection<IPostProcessor> _postProcessors;
    /// <summary>Minifier</summary>
    protected IMinifier _minifier;

    /// <summary>Gets a asset content type</summary>
    protected abstract string ContentType { get; }

    /// <summary>
    /// Gets a list of translators (LESS, Sass, SCSS, CoffeeScript and TypeScript)
    /// </summary>
    public ReadOnlyCollection<ITranslator> Translators => this._translators;

    /// <summary>Gets a list of postprocessors</summary>
    public ReadOnlyCollection<IPostProcessor> PostProcessors => this._postProcessors;

    /// <summary>Gets a minifier</summary>
    public IMinifier Minifier => this._minifier;

    /// <summary>Gets or sets a flag for whether to enable tracing</summary>
    public bool EnableTracing { get; set; }

    /// <summary>
    /// Gets or sets a flag for whether to allow usage of pre-minified files
    /// </summary>
    public bool UsePreMinifiedFiles { get; set; }

    /// <summary>
    /// Gets or sets a flag for whether to allow combine files before minification
    /// </summary>
    public bool CombineFilesBeforeMinification { get; set; }

    /// <summary>Constructs a instance of transformer</summary>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    /// <param name="coreConfig">Configuration settings of core</param>
    protected TransformerBase(string[] ignorePatterns, CoreSettings coreConfig)
    {
      this._ignorePatterns = ignorePatterns;
      this.EnableTracing = coreConfig.EnableTracing;
    }

    /// <summary>Starts a processing of assets</summary>
    /// <param name="bundleContext">Object BundleContext</param>
    /// <param name="bundleResponse">Object BundleResponse</param>
    public void Process(BundleContext bundleContext, BundleResponse bundleResponse) => this.Process(bundleContext, bundleResponse, BundleTransformerContext.Current.IsDebugMode);

    /// <summary>Starts a processing of assets</summary>
    /// <param name="bundleContext">Object BundleContext</param>
    /// <param name="bundleResponse">Object BundleResponse</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    public void Process(
      BundleContext bundleContext,
      BundleResponse bundleResponse,
      bool isDebugMode)
    {
      if (bundleContext == null)
        throw new ArgumentNullException(nameof (bundleContext), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (bundleContext)));
      if (bundleResponse == null)
        throw new ArgumentNullException(nameof (bundleResponse), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (bundleResponse)));
      if (bundleContext.EnableInstrumentation)
        return;
      List<IAsset> assetList = new List<IAsset>();
      foreach (BundleFile file in bundleResponse.Files)
        assetList.Add((IAsset) new Asset(file.VirtualFile.VirtualPath, file));
      if (assetList.Count <= 0)
        return;
      this.Transform((IList<IAsset>) assetList, bundleContext, bundleResponse, BundleTable.VirtualPathProvider, isDebugMode);
    }

    /// <summary>Transforms a assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <param name="bundleContext">Object BundleContext</param>
    /// <param name="bundleResponse">Object BundleResponse</param>
    /// <param name="virtualPathProvider">Virtual path provider</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    protected virtual void Transform(
      IList<IAsset> assets,
      BundleContext bundleContext,
      BundleResponse bundleResponse,
      VirtualPathProvider virtualPathProvider,
      bool isDebugMode)
    {
      this.ValidateAssetTypes(assets);
      assets = this.RemoveDuplicateAssets(assets);
      assets = this.RemoveUnnecessaryAssets(assets);
      assets = this.ReplaceFileExtensions(assets, isDebugMode);
      assets = this.Translate(assets, isDebugMode);
      assets = this.PostProcess(assets, isDebugMode);
      IAsset asset;
      if (this.CombineFilesBeforeMinification)
      {
        asset = this.Combine(assets, bundleContext.BundleVirtualPath, isDebugMode);
        if (!isDebugMode)
          asset = this.Minify(asset);
      }
      else
      {
        if (!isDebugMode)
          assets = this.Minify(assets);
        asset = this.Combine(assets, bundleContext.BundleVirtualPath, isDebugMode);
      }
      this.ConfigureBundleResponse(asset, bundleResponse, virtualPathProvider);
    }

    /// <summary>Validates a assets for compliance with a valid types</summary>
    /// <param name="assets">Set of assets</param>
    protected abstract void ValidateAssetTypes(IList<IAsset> assets);

    /// <summary>Removes a duplicate assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of unique assets</returns>
    protected abstract IList<IAsset> RemoveDuplicateAssets(IList<IAsset> assets);

    /// <summary>Removes a unnecessary assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of necessary assets</returns>
    protected abstract IList<IAsset> RemoveUnnecessaryAssets(IList<IAsset> assets);

    /// <summary>Translates a code of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Set of assets with translated code</returns>
    protected virtual IList<IAsset> Translate(IList<IAsset> assets, bool isDebugMode)
    {
      IList<IAsset> assets1 = assets;
      foreach (ITranslator translator in this._translators)
      {
        translator.IsDebugMode = isDebugMode;
        assets1 = translator.Translate(assets1);
      }
      return assets1;
    }

    /// <summary>Replaces a file extensions of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Set of assets with a modified extension</returns>
    protected abstract IList<IAsset> ReplaceFileExtensions(
      IList<IAsset> assets,
      bool isDebugMode);

    /// <summary>Process a text content of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Set of assets with processed code</returns>
    protected virtual IList<IAsset> PostProcess(IList<IAsset> assets, bool isDebugMode)
    {
      IList<IAsset> assets1 = assets;
      foreach (IPostProcessor postProcessor in this._postProcessors)
      {
        if (!isDebugMode || postProcessor.UseInDebugMode)
          assets1 = postProcessor.PostProcess(assets1);
      }
      return assets1;
    }

    /// <summary>Minify a text content of asset</summary>
    /// <param name="asset">Asset</param>
    /// <returns>Asset with minified code</returns>
    protected virtual IAsset Minify(IAsset asset) => this._minifier.Minify(asset);

    /// <summary>Minify a text content of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of assets with minified code</returns>
    protected virtual IList<IAsset> Minify(IList<IAsset> assets) => this._minifier.Minify(assets);

    /// <summary>Combines a code of assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Combined asset</returns>
    protected abstract IAsset Combine(
      IList<IAsset> assets,
      string bundleVirtualPath,
      bool isDebugMode);

    /// <summary>Configures a bundle bundleResponse</summary>
    /// <param name="combinedAsset">Combined asset</param>
    /// <param name="bundleResponse">Object BundleResponse</param>
    /// <param name="virtualPathProvider">Virtual path provider</param>
    protected virtual void ConfigureBundleResponse(
      IAsset combinedAsset,
      BundleResponse bundleResponse,
      VirtualPathProvider virtualPathProvider)
    {
      List<BundleFile> list = combinedAsset.VirtualPathDependencies.Select<string, BundleFile>((Func<string, BundleFile>) (assetVirtualPath => new BundleFile(assetVirtualPath, virtualPathProvider.GetFile(assetVirtualPath)))).ToList<BundleFile>();
      bundleResponse.Content = combinedAsset.Content;
      bundleResponse.Files = (IEnumerable<BundleFile>) list;
      bundleResponse.ContentType = this.ContentType;
    }
  }
}
