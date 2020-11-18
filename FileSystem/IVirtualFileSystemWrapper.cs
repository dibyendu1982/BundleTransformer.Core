﻿// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.FileSystem.IVirtualFileSystemWrapper
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;
using System.IO;
using System.Text;
using System.Web.Caching;

namespace BundleTransformer.Core.FileSystem
{
  /// <summary>Defines interface of virtual file system wrapper</summary>
  public interface IVirtualFileSystemWrapper
  {
    /// <summary>
    /// Gets a value that indicates whether a file exists in the virtual file system
    /// </summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>Result of checking (true – exist; false – not exist)</returns>
    bool FileExists(string virtualPath);

    /// <summary>Gets a text content of the specified file</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>Text content</returns>
    string GetFileTextContent(string virtualPath);

    /// <summary>Gets a binary content of the specified file</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>Binary content</returns>
    byte[] GetFileBinaryContent(string virtualPath);

    /// <summary>Gets a file stream</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>File stream</returns>
    Stream GetFileStream(string virtualPath);

    /// <summary>
    /// Converts a virtual path to an application absolute path
    /// </summary>
    /// <param name="virtualPath">The virtual path to convert to an application-relative path</param>
    /// <returns>The absolute path representation of the specified virtual path</returns>
    string ToAbsolutePath(string virtualPath);

    /// <summary>
    /// Returns a cache key to use for the specified virtual path
    /// </summary>
    /// <param name="virtualPath">The path to the virtual resource</param>
    /// <returns>A cache key for the specified virtual resource</returns>
    string GetCacheKey(string virtualPath);

    /// <summary>
    /// Creates a cache dependency based on the specified virtual paths
    /// </summary>
    /// <param name="virtualPath">The path to the primary virtual resource</param>
    /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource</param>
    /// <param name="utcStart">The UTC time at which the virtual resources were read</param>
    /// <returns>A System.Web.Caching.CacheDependency object for the specified virtual resources</returns>
    CacheDependency GetCacheDependency(
      string virtualPath,
      string[] virtualPathDependencies,
      DateTime utcStart);

    /// <summary>Detect if a file is text and detect the encoding</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <param name="sampleSize">Number of characters to use for testing</param>
    /// <param name="encoding">Detected encoding</param>
    /// <returns>Result of check (true - is text; false - is binary)</returns>
    bool IsTextFile(string virtualPath, int sampleSize, out Encoding encoding);
  }
}
