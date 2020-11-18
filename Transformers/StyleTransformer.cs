// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Transformers.StyleTransformer
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Combiners;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Filters;
using BundleTransformer.Core.Minifiers;
using BundleTransformer.Core.PostProcessors;
using BundleTransformer.Core.Translators;
using BundleTransformer.Core.Validators;
using System.Collections.Generic;
using System.Linq;

namespace BundleTransformer.Core.Transformers
{
  /// <summary>
  /// Transformer that responsible for processing of style assets
  /// </summary>
  public sealed class StyleTransformer : TransformerBase
  {
    /// <summary>Gets a asset content type</summary>
    protected override string ContentType => BundleTransformer.Core.Constants.ContentType.Css;

    /// <summary>Constructs a instance of style transformer</summary>
    public StyleTransformer()
      : this((IMinifier) null, (IList<ITranslator>) null, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="minifier">Minifier</param>
    public StyleTransformer(IMinifier minifier)
      : this(minifier, (IList<ITranslator>) null, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="translators">List of translators</param>
    public StyleTransformer(IList<ITranslator> translators)
      : this((IMinifier) null, translators, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="postProcessors">List of postprocessors</param>
    public StyleTransformer(IList<IPostProcessor> postProcessors)
      : this((IMinifier) null, (IList<ITranslator>) null, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    public StyleTransformer(IMinifier minifier, IList<ITranslator> translators)
      : this(minifier, translators, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="postProcessors">List of postprocessors</param>
    public StyleTransformer(IMinifier minifier, IList<IPostProcessor> postProcessors)
      : this(minifier, (IList<ITranslator>) null, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    public StyleTransformer(IList<ITranslator> translators, IList<IPostProcessor> postProcessors)
      : this((IMinifier) null, translators, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    public StyleTransformer(
      IMinifier minifier,
      IList<ITranslator> translators,
      IList<IPostProcessor> postProcessors)
      : this(minifier, translators, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    public StyleTransformer(string[] ignorePatterns)
      : this((IMinifier) null, (IList<ITranslator>) null, (IList<IPostProcessor>) null, ignorePatterns)
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    public StyleTransformer(
      IMinifier minifier,
      IList<ITranslator> translators,
      IList<IPostProcessor> postProcessors,
      string[] ignorePatterns)
      : this(minifier, translators, postProcessors, ignorePatterns, BundleTransformerContext.Current.Configuration.GetCoreSettings())
    {
    }

    /// <summary>Constructs a instance of style transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    /// <param name="coreConfig">Configuration settings of core</param>
    public StyleTransformer(
      IMinifier minifier,
      IList<ITranslator> translators,
      IList<IPostProcessor> postProcessors,
      string[] ignorePatterns,
      CoreSettings coreConfig)
      : base(ignorePatterns, coreConfig)
    {
      StyleSettings styles1 = coreConfig.Styles;
      this.UsePreMinifiedFiles = styles1.UsePreMinifiedFiles;
      this.CombineFilesBeforeMinification = styles1.CombineFilesBeforeMinification;
      IAssetContext styles2 = BundleTransformerContext.Current.Styles;
      this._minifier = minifier ?? styles2.GetDefaultMinifierInstance();
      this._translators = (translators ?? styles2.GetDefaultTranslatorInstances()).ToList<ITranslator>().AsReadOnly();
      this._postProcessors = (postProcessors ?? styles2.GetDefaultPostProcessorInstances()).ToList<IPostProcessor>().AsReadOnly();
    }

    /// <summary>
    /// Validates whether the specified assets are style asset
    /// </summary>
    /// <param name="assets">Set of style assets</param>
    protected override void ValidateAssetTypes(IList<IAsset> assets) => new StyleAssetTypesValidator().Validate(assets);

    /// <summary>Removes a duplicate style assets</summary>
    /// <param name="assets">Set of style assets</param>
    /// <returns>Set of unique style assets</returns>
    protected override IList<IAsset> RemoveDuplicateAssets(IList<IAsset> assets) => new StyleDuplicateAssetsFilter().Transform(assets);

    /// <summary>Removes a unnecessary style assets</summary>
    /// <param name="assets">Set of style assets</param>
    /// <returns>Set of necessary style assets</returns>
    protected override IList<IAsset> RemoveUnnecessaryAssets(IList<IAsset> assets) => new StyleUnnecessaryAssetsFilter(this._ignorePatterns).Transform(assets);

    /// <summary>Replaces a file extensions of style assets</summary>
    /// <param name="assets">Set of style assets</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Set of style assets with a modified extension</returns>
    protected override IList<IAsset> ReplaceFileExtensions(
      IList<IAsset> assets,
      bool isDebugMode)
    {
      CssFileExtensionsFilter extensionsFilter = new CssFileExtensionsFilter();
      extensionsFilter.IsDebugMode = isDebugMode;
      extensionsFilter.UsePreMinifiedFiles = this.UsePreMinifiedFiles;
      return extensionsFilter.Transform(assets);
    }

    /// <summary>Combines a code of style assets</summary>
    /// <param name="assets">Set of style assets</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    protected override IAsset Combine(
      IList<IAsset> assets,
      string bundleVirtualPath,
      bool isDebugMode)
    {
      StyleCombiner styleCombiner = new StyleCombiner();
      styleCombiner.IsDebugMode = isDebugMode;
      styleCombiner.EnableTracing = this.EnableTracing;
      return styleCombiner.Combine(assets, bundleVirtualPath);
    }
  }
}
