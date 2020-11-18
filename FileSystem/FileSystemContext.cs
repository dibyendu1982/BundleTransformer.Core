// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.FileSystem.FileSystemContext
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;

namespace BundleTransformer.Core.FileSystem
{
  /// <summary>Defines interface of file system context</summary>
  public sealed class FileSystemContext : IFileSystemContext
  {
    /// <summary>Virtual file system wrapper</summary>
    private readonly Lazy<VirtualFileSystemWrapper> _virtualFileSystemWrapper = new Lazy<VirtualFileSystemWrapper>();
    /// <summary>Common relative path resolver</summary>
    private readonly Lazy<CommonRelativePathResolver> _commonRelativePathResolver = new Lazy<CommonRelativePathResolver>();

    /// <summary>Gets a instance of the virtual file system wrapper</summary>
    /// <returns>Virtual file system wrapper</returns>
    public IVirtualFileSystemWrapper GetVirtualFileSystemWrapper() => (IVirtualFileSystemWrapper) this._virtualFileSystemWrapper.Value;

    /// <summary>Gets a instance of the common relative path resolver</summary>
    /// <returns>Common relative path resolver</returns>
    public IRelativePathResolver GetCommonRelativePathResolver() => (IRelativePathResolver) this._commonRelativePathResolver.Value;
  }
}
