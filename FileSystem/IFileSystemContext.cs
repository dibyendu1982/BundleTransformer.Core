// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.FileSystem.IFileSystemContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.FileSystem
{
  public interface IFileSystemContext
  {
    /// <summary>Gets a instance of the virtual file system wrapper</summary>
    /// <returns>Virtual file system wrapper</returns>
    IVirtualFileSystemWrapper GetVirtualFileSystemWrapper();

    /// <summary>Gets a instance of the common relative path resolver</summary>
    /// <returns>Common relative path resolver</returns>
    IRelativePathResolver GetCommonRelativePathResolver();
  }
}
