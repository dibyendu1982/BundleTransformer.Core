// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.DependencyCollection
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace BundleTransformer.Core.Assets
{
  /// <summary>List of dependencies</summary>
  public sealed class DependencyCollection : List<Dependency>
  {
    /// <summary>
    /// Determines whether the list of dependencies contains the specified URL
    /// </summary>
    /// <param name="url">URL of dependency</param>
    /// <returns>Result of checking (true – contains; false – not contains)</returns>
    public bool ContainsUrl(string url)
    {
      if (this.Count == 0)
        return false;
      string urlInUpperCase = url.ToUpperInvariant();
      return this.Count<Dependency>((Func<Dependency, bool>) (d => d.Url.ToUpperInvariant() == urlInUpperCase)) > 0;
    }

    /// <summary>Gets a dependency by URL</summary>
    /// <param name="url">URL of dependency</param>
    /// <returns>Dependency</returns>
    public Dependency GetByUrl(string url)
    {
      if (this.Count == 0)
        return (Dependency) null;
      string urlInUpperCase = url.ToUpperInvariant();
      return this.SingleOrDefault<Dependency>((Func<Dependency, bool>) (d => d.Url.ToUpperInvariant() == urlInUpperCase));
    }
  }
}
