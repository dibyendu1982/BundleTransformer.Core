// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Filters.CssFileExtensionsFilter
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Constants;
using BundleTransformer.Core.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BundleTransformer.Core.Filters
{
  /// <summary>
  /// Filter that responsible for choosing appropriate version
  /// of CSS file, depending on current mode of
  /// web application (debug mode - debug versions of CSS asset files;
  /// release mode - minified versions)
  /// </summary>
  public sealed class CssFileExtensionsFilter : FileExtensionsFilterBase
  {
    /// <summary>Extensions of CSS files for debug mode</summary>
    private static readonly string[] _debugCssExtensions = new string[2]
    {
      ".css",
      ".min.css"
    };
    /// <summary>Extensions of CSS files for release mode</summary>
    private static readonly string[] _releaseCssExtensions = new string[2]
    {
      ".min.css",
      ".css"
    };

    /// <summary>Constructs a instance of CSS file extensions filter</summary>
    public CssFileExtensionsFilter()
      : this(BundleTransformerContext.Current.FileSystem.GetVirtualFileSystemWrapper())
    {
    }

    /// <summary>Constructs a instance of CSS file extensions filter</summary>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    public CssFileExtensionsFilter(IVirtualFileSystemWrapper virtualFileSystemWrapper)
      : base(virtualFileSystemWrapper)
    {
    }

    /// <summary>
    /// Chooses a appropriate versions of CSS files, depending on
    /// current mode of web application
    /// </summary>
    /// <param name="assets">Set of CSS assets</param>
    /// <returns>Set of CSS assets adapted for current mode of web application</returns>
    public override IList<IAsset> Transform(IList<IAsset> assets)
    {
      if (assets == null)
        throw new ArgumentNullException(nameof (assets), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (assets)));
      if (assets.Count == 0)
        return assets;
      foreach (IAsset asset in assets.Where<IAsset>((Func<IAsset, bool>) (a => a.AssetTypeCode == AssetTypeCode.Css && !a.Minified)))
      {
        bool isMinified;
        string appropriateAssetFilePath = this.GetAppropriateAssetFilePath(asset.VirtualPath, out isMinified);
        asset.VirtualPath = appropriateAssetFilePath;
        asset.Minified = isMinified;
      }
      return assets;
    }

    /// <summary>
    /// Gets a version of CSS file virtual path, most appropriate for
    /// current mode of web application
    /// </summary>
    /// <param name="assetVirtualPath">CSS asset virtual file path</param>
    /// <param name="isMinified">Flag indicating what appropriate
    /// virtual file path version of CSS asset is minified</param>
    /// <returns>Virtual path to CSS file, corresponding current mode
    /// of web application</returns>
    protected override string GetAppropriateAssetFilePath(
      string assetVirtualPath,
      out bool isMinified)
    {
      string assetVirtualPath1 = assetVirtualPath.Trim();
      isMinified = false;
      if (assetVirtualPath1.Length > 0)
      {
        string[] extensions = this.UsageOfPreMinifiedFilesEnabled ? CssFileExtensionsFilter._releaseCssExtensions : CssFileExtensionsFilter._debugCssExtensions;
        assetVirtualPath1 = this.ProbeAssetFilePath(Asset.RemoveAdditionalCssFileExtension(assetVirtualPath1), extensions);
        isMinified = Asset.IsCssFileWithMinExtension(assetVirtualPath1);
      }
      return assetVirtualPath1;
    }
  }
}
