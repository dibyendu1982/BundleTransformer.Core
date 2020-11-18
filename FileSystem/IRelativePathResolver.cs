// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.FileSystem.IRelativePathResolver
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.FileSystem
{
  /// <summary>Defines interface of relative path resolver</summary>
  public interface IRelativePathResolver
  {
    /// <summary>Transforms relative path to absolute</summary>
    /// <param name="basePath">The base path</param>
    /// <param name="relativePath">The relative path</param>
    string ResolveRelativePath(string basePath, string relativePath);
  }
}
