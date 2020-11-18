// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Builders.NullBuilder
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Collections.Generic;
using System.Web.Optimization;

namespace BundleTransformer.Core.Builders
{
  /// <summary>
  /// Builder that responsible for prevention of early applying of
  /// the item transformations and combining of code
  /// </summary>
  public sealed class NullBuilder : IBundleBuilder
  {
    /// <summary>
    /// Prevents a early applying of the item transformations and combining of code
    /// </summary>
    /// <param name="bundle">Bundle</param>
    /// <param name="context">Object BundleContext</param>
    /// <param name="files">List of files</param>
    /// <returns>Empty string</returns>
    public string BuildBundleContent(
      Bundle bundle,
      BundleContext context,
      IEnumerable<BundleFile> files)
    {
      return string.Empty;
    }
  }
}
