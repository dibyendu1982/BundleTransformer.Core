// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.FileSystem.CommonRelativePathResolver
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Helpers;
using BundleTransformer.Core.Resources;
using System;

namespace BundleTransformer.Core.FileSystem
{
  /// <summary>Common relative path resolver</summary>
  public class CommonRelativePathResolver : IRelativePathResolver
  {
    /// <summary>Virtual file system wrapper</summary>
    private readonly IVirtualFileSystemWrapper _virtualFileSystemWrapper;

    /// <summary>
    /// Constructs a instance of common relative path resolver
    /// </summary>
    public CommonRelativePathResolver()
      : this(BundleTransformerContext.Current.FileSystem.GetVirtualFileSystemWrapper())
    {
    }

    /// <summary>
    /// Constructs a instance of common relative path resolver
    /// </summary>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    public CommonRelativePathResolver(IVirtualFileSystemWrapper virtualFileSystemWrapper) => this._virtualFileSystemWrapper = virtualFileSystemWrapper;

    /// <summary>Transforms a relative path to absolute</summary>
    /// <param name="basePath">The base path</param>
    /// <param name="relativePath">The relative path</param>
    public string ResolveRelativePath(string basePath, string relativePath)
    {
      if (basePath == null)
        throw new ArgumentNullException(nameof (basePath), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (basePath)));
      if (relativePath == null)
        throw new ArgumentNullException(nameof (relativePath), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (relativePath)));
      string str1 = UrlHelpers.ProcessBackSlashes(basePath);
      string str2 = UrlHelpers.ProcessBackSlashes(relativePath);
      string absolutePath1;
      if (this.TryConvertToAbsolutePath(str2, out absolutePath1))
        return UrlHelpers.Normalize(absolutePath1);
      string absolutePath2;
      if (this.TryConvertToAbsolutePath(str1, out absolutePath2))
        str1 = absolutePath2;
      if (string.IsNullOrWhiteSpace(str1))
        return UrlHelpers.Normalize(str2);
      return string.IsNullOrWhiteSpace(str2) ? UrlHelpers.Normalize(str1) : UrlHelpers.Combine(UrlHelpers.GetDirectoryName(str1), str2);
    }

    /// <summary>
    /// Converts a relative path to an absolute path.
    /// A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="relativePath">The relative path</param>
    /// <param name="absolutePath">The absolute path</param>
    /// <returns>true if path was converted successfully; otherwise, false</returns>
    private bool TryConvertToAbsolutePath(string relativePath, out string absolutePath)
    {
      absolutePath = (string) null;
      if (relativePath.StartsWith("/") || UrlHelpers.StartsWithProtocol(relativePath))
      {
        absolutePath = relativePath;
        return true;
      }
      if (!relativePath.StartsWith("~/"))
        return false;
      absolutePath = this._virtualFileSystemWrapper.ToAbsolutePath(relativePath);
      return true;
    }
  }
}
