// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Orderers.NullOrderer
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Collections.Generic;
using System.Web.Optimization;

namespace BundleTransformer.Core.Orderers
{
  /// <summary>
  /// Orderer that responsible for sorting of the files in declarative order
  /// </summary>
  public sealed class NullOrderer : IBundleOrderer
  {
    /// <summary>Sorts a files in declarative order</summary>
    /// <param name="context">Object BundleContext</param>
    /// <param name="files">List of files</param>
    /// <returns>Sorted list of files</returns>
    public IEnumerable<BundleFile> OrderFiles(
      BundleContext context,
      IEnumerable<BundleFile> files)
    {
      return files;
    }
  }
}
