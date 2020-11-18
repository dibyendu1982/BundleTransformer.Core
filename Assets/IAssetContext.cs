// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.IAssetContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Minifiers;
using BundleTransformer.Core.PostProcessors;
using BundleTransformer.Core.Translators;
using System.Collections.Generic;
using System.Web.Optimization;

namespace BundleTransformer.Core.Assets
{
  /// <summary>Defines interface of asset context</summary>
  public interface IAssetContext
  {
    /// <summary>Gets a file extension mappings</summary>
    FileExtensionMappingCollection FileExtensionMappings { get; }

    /// <summary>Gets a instance of default transform</summary>
    /// <returns>Instance of transformer</returns>
    IBundleTransform GetDefaultTransformInstance();

    /// <summary>Gets a instance of translator</summary>
    /// <param name="translatorName">Translator name</param>
    /// <returns>Instance of translator</returns>
    ITranslator GetTranslatorInstance(string translatorName);

    /// <summary>Gets a instance of postprocessor</summary>
    /// <param name="postProcessorName">Postprocessor name</param>
    /// <returns>Instance of postprocessor</returns>
    IPostProcessor GetPostProcessorInstance(string postProcessorName);

    /// <summary>Gets a instance of minifier</summary>
    /// <param name="minifierName">Minifier name</param>
    /// <returns>Instance of minifier</returns>
    IMinifier GetMinifierInstance(string minifierName);

    /// <summary>Gets a list of default translator instances</summary>
    /// <returns>List of default translator instances</returns>
    IList<ITranslator> GetDefaultTranslatorInstances();

    /// <summary>Gets a list of default postprocessor instances</summary>
    /// <returns>List of default postprocessor instances</returns>
    IList<IPostProcessor> GetDefaultPostProcessorInstances();

    /// <summary>Gets a instance of default minifier</summary>
    /// <returns>Instance of default minifier</returns>
    IMinifier GetDefaultMinifierInstance();
  }
}
