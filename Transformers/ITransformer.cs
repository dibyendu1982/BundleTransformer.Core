// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Transformers.ITransformer
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Minifiers;
using BundleTransformer.Core.PostProcessors;
using BundleTransformer.Core.Translators;
using System.Collections.ObjectModel;

namespace BundleTransformer.Core.Transformers
{
  /// <summary>
  /// Defines interface of transformer that responsible for processing assets
  /// </summary>
  public interface ITransformer
  {
    /// <summary>Gets a list of translators</summary>
    ReadOnlyCollection<ITranslator> Translators { get; }

    /// <summary>Gets a list of postprocessors</summary>
    ReadOnlyCollection<IPostProcessor> PostProcessors { get; }

    /// <summary>Gets a minifier</summary>
    IMinifier Minifier { get; }
  }
}
