// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Filters.JsFileExtensionsFilter
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Constants;
using BundleTransformer.Core.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BundleTransformer.Core.Filters
{
  /// <summary>
  /// Filter that responsible for choosing appropriate version
  /// of JS file, depending on current mode of
  /// web application (debug mode - debug versions of JS asset files;
  /// release mode - minified versions)
  /// </summary>
  public sealed class JsFileExtensionsFilter : FileExtensionsFilterBase
  {
    /// <summary>Version number placeholder</summary>
    private const string VERSION_NUMBER_PLACEHOLDER = "$version$";
    /// <summary>
    /// List of regular expressions of JS files with Microsoft-style extensions
    /// </summary>
    private readonly List<Regex> _jsFilesWithMsStyleExtensionsRegExps;
    /// <summary>
    /// Extensions of JS files for debug mode (standard style)
    /// </summary>
    private static readonly string[] _debugJsExtensionsForStandardStyle = new string[2]
    {
      ".js",
      ".min.js"
    };
    /// <summary>
    /// Extensions of JS files for release mode (standard style)
    /// </summary>
    private static readonly string[] _releaseJsExtensionsForStandardStyle = new string[2]
    {
      ".min.js",
      ".js"
    };
    /// <summary>
    /// Extensions of JS files for debug mode (Microsoft style)
    /// </summary>
    private static readonly string[] _debugJsExtensionsForMicrosoftStyle = new string[2]
    {
      ".debug.js",
      ".js"
    };
    /// <summary>
    /// Extensions of JS files for release mode (Microsoft style)
    /// </summary>
    private static readonly string[] _releaseJsExtensionsForMicrosoftStyle = new string[2]
    {
      ".js",
      ".debug.js"
    };

    /// <summary>Constructs a instance of JS file extensions filter</summary>
    /// <param name="jsFilesWithMsStyleExtensions">JS files with Microsoft-style extensions list</param>
    public JsFileExtensionsFilter(string[] jsFilesWithMsStyleExtensions)
      : this(jsFilesWithMsStyleExtensions, BundleTransformerContext.Current.FileSystem.GetVirtualFileSystemWrapper())
    {
    }

    /// <summary>Constructs a instance of JS file extensions filter</summary>
    /// <param name="jsFilesWithMsStyleExtensions">JS files with Microsoft-style extensions list</param>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    public JsFileExtensionsFilter(
      string[] jsFilesWithMsStyleExtensions,
      IVirtualFileSystemWrapper virtualFileSystemWrapper)
      : base(virtualFileSystemWrapper)
    {
      List<Regex> regexList = new List<Regex>();
      if (jsFilesWithMsStyleExtensions.Length != 0)
      {
        string oldValue = "$version$".Replace("$", "\\$");
        foreach (string msStyleExtension in jsFilesWithMsStyleExtensions)
        {
          if (!string.IsNullOrWhiteSpace(msStyleExtension))
          {
            string str = Regex.Escape(msStyleExtension.Trim());
            if (str.IndexOf(oldValue, StringComparison.OrdinalIgnoreCase) != -1)
              str = str.Replace(oldValue, "(?:\\d+\\.)*\\d+(?:(?:alpha|beta|rc)\\d{0,1})?");
            string pattern = "^" + str + "$";
            regexList.Add(new Regex(pattern, RegexOptions.IgnoreCase));
          }
        }
      }
      this._jsFilesWithMsStyleExtensionsRegExps = regexList;
    }

    /// <summary>
    /// Chooses a appropriate versions of JS files, depending on
    /// current mode of web application
    /// </summary>
    /// <param name="assets">Set of JS assets</param>
    /// <returns>Set of JS assets adapted for current mode of web application</returns>
    public override IList<IAsset> Transform(IList<IAsset> assets)
    {
      if (assets == null)
        throw new ArgumentNullException(nameof (assets), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (assets)));
      if (assets.Count == 0)
        return assets;
      foreach (IAsset asset in assets.Where<IAsset>((Func<IAsset, bool>) (a => a.AssetTypeCode == AssetTypeCode.JavaScript && !a.Minified)))
      {
        bool isMinified;
        string appropriateAssetFilePath = this.GetAppropriateAssetFilePath(asset.VirtualPath, out isMinified);
        asset.VirtualPath = appropriateAssetFilePath;
        asset.Minified = isMinified;
      }
      return assets;
    }

    /// <summary>
    /// Gets a version of JS file virtual path, most appropriate for
    /// current mode of web application
    /// </summary>
    /// <param name="assetVirtualPath">JS asset file virtual path</param>
    /// <param name="isMinified">Flag indicating what appropriate
    /// virtual file path version of JS asset is minified</param>
    /// <returns>Virtual path to JS file, corresponding current mode
    /// of web application</returns>
    protected override string GetAppropriateAssetFilePath(
      string assetVirtualPath,
      out bool isMinified)
    {
      string assetVirtualPath1 = assetVirtualPath.Trim();
      isMinified = false;
      if (assetVirtualPath1.Length > 0)
      {
        string assetVirtualPath2 = Asset.RemoveAdditionalJsFileExtension(assetVirtualPath1);
        if (this.IsJsFileWithMicrosoftStyleExtension(assetVirtualPath2))
        {
          string[] extensions = this.UsageOfPreMinifiedFilesEnabled ? JsFileExtensionsFilter._releaseJsExtensionsForMicrosoftStyle : JsFileExtensionsFilter._debugJsExtensionsForMicrosoftStyle;
          assetVirtualPath1 = this.ProbeAssetFilePath(assetVirtualPath2, extensions);
          isMinified = !Asset.IsJsFileWithDebugExtension(assetVirtualPath1);
        }
        else
        {
          string[] extensions = this.UsageOfPreMinifiedFilesEnabled ? JsFileExtensionsFilter._releaseJsExtensionsForStandardStyle : JsFileExtensionsFilter._debugJsExtensionsForStandardStyle;
          assetVirtualPath1 = this.ProbeAssetFilePath(assetVirtualPath2, extensions);
          isMinified = Asset.IsJsFileWithMinExtension(assetVirtualPath1);
        }
      }
      return assetVirtualPath1;
    }

    /// <summary>
    /// Checks a existance of specified JS file in list of
    /// JS files that have extensions in Microsoft-style
    /// </summary>
    /// <param name="assetVirtualPath">JS asset virtual file path</param>
    /// <returns>Checking result (true – exist; false – not exist)</returns>
    private bool IsJsFileWithMicrosoftStyleExtension(string assetVirtualPath)
    {
      string input = Asset.RemoveAdditionalJsFileExtension(Path.GetFileName(assetVirtualPath));
      bool flag = false;
      foreach (Regex extensionsRegExp in this._jsFilesWithMsStyleExtensionsRegExps)
      {
        if (extensionsRegExp.IsMatch(input))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }
  }
}
