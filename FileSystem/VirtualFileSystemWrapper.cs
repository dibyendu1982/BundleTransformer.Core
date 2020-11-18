// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.FileSystem.VirtualFileSystemWrapper
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using AdvancedStringBuilder;
using BundleTransformer.Core.Resources;
using BundleTransformer.Core.Utilities;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Optimization;

namespace BundleTransformer.Core.FileSystem
{
  /// <summary>Virtual file system wrapper</summary>
  public sealed class VirtualFileSystemWrapper : IVirtualFileSystemWrapper
  {
    /// <summary>
    /// Gets a value that indicates whether a file exists in the virtual file system
    /// </summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>Result of checking (true – exist; false – not exist)</returns>
    public bool FileExists(string virtualPath) => BundleTable.VirtualPathProvider.FileExists(virtualPath);

    /// <summary>Gets a text content of the specified file</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>Text content</returns>
    public string GetFileTextContent(string virtualPath)
    {
      StringBuilderPool shared = StringBuilderPool.Shared;
      StringBuilder builder = shared.Rent();
      try
      {
        using (StreamReader streamReader = new StreamReader(BundleTable.VirtualPathProvider.GetFile(virtualPath).Open()))
        {
          while (streamReader.Peek() >= 0)
            builder.AppendLine(streamReader.ReadLine());
        }
        return builder.ToString();
      }
      catch (FileNotFoundException ex)
      {
        throw new FileNotFoundException(string.Format(Strings.Common_FileNotExist, (object) virtualPath), virtualPath, (Exception) ex);
      }
      finally
      {
        shared.Return(builder);
      }
    }

    /// <summary>Gets a binary content of the specified file</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>Binary content</returns>
    public byte[] GetFileBinaryContent(string virtualPath)
    {
      byte[] buffer;
      try
      {
        using (Stream stream = BundleTable.VirtualPathProvider.GetFile(virtualPath).Open())
        {
          buffer = new byte[stream.Length];
          stream.Read(buffer, 0, (int) stream.Length);
        }
      }
      catch (FileNotFoundException ex)
      {
        throw new FileNotFoundException(string.Format(Strings.Common_FileNotExist, (object) virtualPath), virtualPath, (Exception) ex);
      }
      return buffer;
    }

    /// <summary>Gets a file stream</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <returns>File stream</returns>
    public Stream GetFileStream(string virtualPath)
    {
      try
      {
        return BundleTable.VirtualPathProvider.GetFile(virtualPath).Open();
      }
      catch (FileNotFoundException ex)
      {
        throw new FileNotFoundException(string.Format(Strings.Common_FileNotExist, (object) virtualPath), virtualPath, (Exception) ex);
      }
    }

    /// <summary>
    /// Converts a virtual path to an application absolute path
    /// </summary>
    /// <param name="virtualPath">The virtual path to convert to an application-relative path</param>
    /// <returns>The absolute path representation of the specified virtual path</returns>
    public string ToAbsolutePath(string virtualPath) => VirtualPathUtility.ToAbsolute(virtualPath);

    /// <summary>
    /// Returns a cache key to use for the specified virtual path
    /// </summary>
    /// <param name="virtualPath">The path to the virtual resource</param>
    /// <returns>A cache key for the specified virtual resource</returns>
    public string GetCacheKey(string virtualPath) => BundleTable.VirtualPathProvider.GetCacheKey(virtualPath);

    /// <summary>
    /// Creates a cache dependency based on the specified virtual paths
    /// </summary>
    /// <param name="virtualPath">The path to the primary virtual resource</param>
    /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource</param>
    /// <param name="utcStart">The UTC time at which the virtual resources were read</param>
    /// <returns>A System.Web.Caching.CacheDependency object for the specified virtual resources</returns>
    public CacheDependency GetCacheDependency(
      string virtualPath,
      string[] virtualPathDependencies,
      DateTime utcStart)
    {
      return BundleTable.VirtualPathProvider.GetCacheDependency(virtualPath, (IEnumerable) virtualPathDependencies, utcStart);
    }

    /// <summary>Detect if a file is text and detect the encoding</summary>
    /// <param name="virtualPath">The path to the virtual file</param>
    /// <param name="sampleSize">Number of characters to use for testing</param>
    /// <param name="encoding">Detected encoding</param>
    /// <returns>Result of check (true - is text; false - is binary)</returns>
    public bool IsTextFile(string virtualPath, int sampleSize, out Encoding encoding)
    {
      using (Stream fileStream = this.GetFileStream(virtualPath))
        return Utils.IsTextStream(fileStream, sampleSize, out encoding);
    }
  }
}
