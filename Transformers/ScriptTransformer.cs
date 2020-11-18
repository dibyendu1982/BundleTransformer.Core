// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Transformers.ScriptTransformer
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
using BundleTransformer.Core.Utilities;
using BundleTransformer.Core.Validators;
using System.Collections.Generic;
using System.Linq;

namespace BundleTransformer.Core.Transformers
{
  /// <summary>
  /// Transformer that responsible for processing of script assets
  /// </summary>
  public sealed class ScriptTransformer : TransformerBase
  {
    /// <summary>List of JS files with Microsoft-style extensions</summary>
    private readonly string[] _jsFilesWithMsStyleExtensions;

    /// <summary>Gets a asset content type</summary>
    protected override string ContentType => BundleTransformer.Core.Constants.ContentType.Js;

    /// <summary>Constructs a instance of script transformer</summary>
    public ScriptTransformer()
      : this((IMinifier) null, (IList<ITranslator>) null, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="minifier">Minifier</param>
    public ScriptTransformer(IMinifier minifier)
      : this(minifier, (IList<ITranslator>) null, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="translators">List of translators</param>
    public ScriptTransformer(IList<ITranslator> translators)
      : this((IMinifier) null, translators, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="postProcessors">List of postprocessors</param>
    public ScriptTransformer(IList<IPostProcessor> postProcessors)
      : this((IMinifier) null, (IList<ITranslator>) null, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    public ScriptTransformer(IMinifier minifier, IList<ITranslator> translators)
      : this(minifier, translators, (IList<IPostProcessor>) null, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="postProcessors">List of postprocessors</param>
    public ScriptTransformer(IMinifier minifier, IList<IPostProcessor> postProcessors)
      : this(minifier, (IList<ITranslator>) null, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    public ScriptTransformer(IList<ITranslator> translators, IList<IPostProcessor> postProcessors)
      : this((IMinifier) null, translators, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    public ScriptTransformer(
      IMinifier minifier,
      IList<ITranslator> translators,
      IList<IPostProcessor> postProcessors)
      : this(minifier, translators, postProcessors, new string[0])
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    public ScriptTransformer(string[] ignorePatterns)
      : this((IMinifier) null, (IList<ITranslator>) null, (IList<IPostProcessor>) null, ignorePatterns)
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    public ScriptTransformer(
      IMinifier minifier,
      IList<ITranslator> translators,
      IList<IPostProcessor> postProcessors,
      string[] ignorePatterns)
      : this(minifier, translators, postProcessors, ignorePatterns, BundleTransformerContext.Current.Configuration.GetCoreSettings())
    {
    }

    /// <summary>Constructs a instance of script transformer</summary>
    /// <param name="minifier">Minifier</param>
    /// <param name="translators">List of translators</param>
    /// <param name="postProcessors">List of postprocessors</param>
    /// <param name="ignorePatterns">List of patterns of files and directories that
    /// should be ignored when processing</param>
    /// <param name="coreConfig">Configuration settings of core</param>
    public ScriptTransformer(
      IMinifier minifier,
      IList<ITranslator> translators,
      IList<IPostProcessor> postProcessors,
      string[] ignorePatterns,
      CoreSettings coreConfig)
      : base(ignorePatterns, coreConfig)
    {
      ScriptSettings scripts1 = coreConfig.Scripts;
      this.UsePreMinifiedFiles = scripts1.UsePreMinifiedFiles;
      this.CombineFilesBeforeMinification = scripts1.CombineFilesBeforeMinification;
      this._jsFilesWithMsStyleExtensions = Utils.ConvertToStringCollection(coreConfig.JsFilesWithMicrosoftStyleExtensions.Replace(';', ','), ',', true, true);
      IAssetContext scripts2 = BundleTransformerContext.Current.Scripts;
      this._minifier = minifier ?? scripts2.GetDefaultMinifierInstance();
      this._translators = (translators ?? scripts2.GetDefaultTranslatorInstances()).ToList<ITranslator>().AsReadOnly();
      this._postProcessors = (postProcessors ?? scripts2.GetDefaultPostProcessorInstances()).ToList<IPostProcessor>().AsReadOnly();
    }

    /// <summary>
    /// Validates whether the specified assets are script asset
    /// </summary>
    /// <param name="assets">Set of script assets</param>
    protected override void ValidateAssetTypes(IList<IAsset> assets) => new ScriptAssetTypesValidator().Validate(assets);

    /// <summary>Removes a duplicate script assets</summary>
    /// <param name="assets">Set of script assets</param>
    /// <returns>Set of unique script assets</returns>
    protected override IList<IAsset> RemoveDuplicateAssets(IList<IAsset> assets) => new ScriptDuplicateAssetsFilter().Transform(assets);

    /// <summary>Removes a unnecessary script assets</summary>
    /// <param name="assets">Set of script assets</param>
    /// <returns>Set of necessary script assets</returns>
    protected override IList<IAsset> RemoveUnnecessaryAssets(IList<IAsset> assets) => new ScriptUnnecessaryAssetsFilter(this._ignorePatterns).Transform(assets);

    /// <summary>Replaces a file extensions of script assets</summary>
    /// <param name="assets">Set of script assets</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Set of script assets with a modified extension</returns>
    protected override IList<IAsset> ReplaceFileExtensions(
      IList<IAsset> assets,
      bool isDebugMode)
    {
      JsFileExtensionsFilter extensionsFilter = new JsFileExtensionsFilter(this._jsFilesWithMsStyleExtensions);
      extensionsFilter.IsDebugMode = isDebugMode;
      extensionsFilter.UsePreMinifiedFiles = this.UsePreMinifiedFiles;
      return extensionsFilter.Transform(assets);
    }

    /// <summary>Combines a code of script assets</summary>
    /// <param name="assets">Set of script assets</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// 
    ///             /// <returns>Combined asset</returns>
    protected override IAsset Combine(
      IList<IAsset> assets,
      string bundleVirtualPath,
      bool isDebugMode)
    {
      ScriptCombiner scriptCombiner = new ScriptCombiner();
      scriptCombiner.IsDebugMode = isDebugMode;
      scriptCombiner.EnableTracing = this.EnableTracing;
      return scriptCombiner.Combine(assets, bundleVirtualPath);
    }
  }
}
