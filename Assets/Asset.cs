// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.Asset
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Constants;
using BundleTransformer.Core.FileSystem;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Optimization;

namespace BundleTransformer.Core.Assets
{
  /// <summary>Asset</summary>
  public sealed class Asset : IAsset
  {
    /// <summary>
    /// Regular expression to determine whether asset is
    /// minified version of CSS file with *.min.css extension
    /// </summary>
    private static readonly Regex _cssFileWithMinExtensionRegex = new Regex("\\.min\\.css$", RegexOptions.IgnoreCase);
    /// <summary>
    /// Regular expression to determine whether asset is
    /// debug version of JS file with *.debug.js extension
    /// </summary>
    private static readonly Regex _jsFileWithDebugExtensionRegex = new Regex("\\.debug\\.js$", RegexOptions.IgnoreCase);
    /// <summary>
    /// Regular expression to determine whether asset is
    /// minified version of JS file with *.min.js extension
    /// </summary>
    private static readonly Regex _jsFileWithMinExtensionRegex = new Regex("\\.min\\.js$", RegexOptions.IgnoreCase);
    /// <summary>Virtual file system wrapper</summary>
    private readonly IVirtualFileSystemWrapper _virtualFileSystemWrapper;
    /// <summary>Style file extension mappings</summary>
    private readonly FileExtensionMappingCollection _styleFileExtensionMappings;
    /// <summary>Script file extension mappings</summary>
    private readonly FileExtensionMappingCollection _scriptFileExtensionMappings;
    /// <summary>Virtual path to asset file</summary>
    private string _virtualPath;
    /// <summary>Asset type code</summary>
    private string _assetTypeCode;
    /// <summary>Flag indicating what asset is a stylesheet</summary>
    private bool _isStylesheet;
    /// <summary>Flag indicating what asset is a script</summary>
    private bool _isScript;
    /// <summary>Text content of asset</summary>
    private string _content;
    /// <summary>Included virtual path</summary>
    private readonly string _includedVirtualPath;
    /// <summary>List of asset transformations</summary>
    private readonly IList<IItemTransform> _transforms;

    /// <summary>Gets or sets a virtual path to asset file</summary>
    public string VirtualPath
    {
      get => this._virtualPath;
      set
      {
        string filePath = value;
        string str = BundleTransformer.Core.Constants.AssetTypeCode.Unknown;
        bool flag1 = false;
        bool flag2 = false;
        if (!string.IsNullOrWhiteSpace(filePath))
        {
          str = this._styleFileExtensionMappings.GetAssetTypeCodeByFilePath(filePath);
          if (str != BundleTransformer.Core.Constants.AssetTypeCode.Unknown)
          {
            flag1 = true;
          }
          else
          {
            str = this._scriptFileExtensionMappings.GetAssetTypeCodeByFilePath(filePath);
            if (str != BundleTransformer.Core.Constants.AssetTypeCode.Unknown)
              flag2 = true;
          }
        }
        this._virtualPath = filePath;
        this._assetTypeCode = str;
        this._isStylesheet = flag1;
        this._isScript = flag2;
      }
    }

    /// <summary>Gets a URL of asset file</summary>
    public string Url => this._virtualFileSystemWrapper.ToAbsolutePath(this.VirtualPath);

    /// <summary>
    /// Gets or sets a list of virtual paths to other files required by the primary asset
    /// </summary>
    public IList<string> VirtualPathDependencies { get; set; }

    /// <summary>Gets or sets a list of original assets</summary>
    public IList<IAsset> OriginalAssets { get; set; }

    /// <summary>Gets a asset type code</summary>
    public string AssetTypeCode => this._assetTypeCode;

    /// <summary>
    /// Gets or sets a flag indicating what text content of asset was obtained by
    /// combining the contents of other assets
    /// </summary>
    public bool Combined { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating what text content of asset is minified
    /// </summary>
    public bool Minified { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating what all relative paths in
    /// text content of asset is transformed to absolute
    /// </summary>
    public bool RelativePathsResolved { get; set; }

    /// <summary>Gets or sets a text content of asset</summary>
    public string Content
    {
      get
      {
        if (this._content == null)
          this._content = this.ApplyTransformsToContent(this._virtualFileSystemWrapper.GetFileTextContent(this.VirtualPath));
        return this._content;
      }
      set => this._content = value;
    }

    /// <summary>Gets a flag indicating what asset is a stylesheet</summary>
    public bool IsStylesheet => this._isStylesheet;

    /// <summary>Gets a flag indicating what asset is a script</summary>
    public bool IsScript => this._isScript;

    /// <summary>Constructs a instance of Asset</summary>
    /// <param name="virtualPath">Virtual path to asset file</param>
    public Asset(string virtualPath)
      : this(virtualPath, (BundleFile) null, BundleTransformerContext.Current.FileSystem.GetVirtualFileSystemWrapper())
    {
    }

    /// <summary>Constructs a instance of Asset</summary>
    /// <param name="virtualPath">Virtual path to asset file</param>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    public Asset(string virtualPath, IVirtualFileSystemWrapper virtualFileSystemWrapper)
      : this(virtualPath, (BundleFile) null, virtualFileSystemWrapper)
    {
    }

    /// <summary>Constructs a instance of Asset</summary>
    /// <param name="virtualPath">Virtual path to asset file</param>
    /// <param name="bundleFile">Bundle file</param>
    public Asset(string virtualPath, BundleFile bundleFile)
      : this(virtualPath, bundleFile, BundleTransformerContext.Current.FileSystem.GetVirtualFileSystemWrapper())
    {
    }

    /// <summary>Constructs a instance of Asset</summary>
    /// <param name="virtualPath">Virtual path to asset file</param>
    /// <param name="bundleFile">Bundle file</param>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    public Asset(
      string virtualPath,
      BundleFile bundleFile,
      IVirtualFileSystemWrapper virtualFileSystemWrapper)
      : this(virtualPath, bundleFile, virtualFileSystemWrapper, BundleTransformerContext.Current.Styles.FileExtensionMappings, BundleTransformerContext.Current.Scripts.FileExtensionMappings)
    {
    }

    /// <summary>Constructs a instance of Asset</summary>
    /// <param name="virtualPath">Virtual path to asset file</param>
    /// <param name="bundleFile">Bundle file</param>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    /// <param name="styleFileExtensionMappings">Style file extension mappings</param>
    /// <param name="scriptFileExtensionMappings">Script file extension mappings</param>
    public Asset(
      string virtualPath,
      BundleFile bundleFile,
      IVirtualFileSystemWrapper virtualFileSystemWrapper,
      FileExtensionMappingCollection styleFileExtensionMappings,
      FileExtensionMappingCollection scriptFileExtensionMappings)
    {
      this._virtualFileSystemWrapper = virtualFileSystemWrapper;
      this._styleFileExtensionMappings = styleFileExtensionMappings;
      this._scriptFileExtensionMappings = scriptFileExtensionMappings;
      if (bundleFile != null)
      {
        this._includedVirtualPath = bundleFile.IncludedVirtualPath;
        this._transforms = bundleFile.Transforms;
      }
      else
      {
        this._includedVirtualPath = string.Empty;
        this._transforms = (IList<IItemTransform>) new List<IItemTransform>();
      }
      this._assetTypeCode = BundleTransformer.Core.Constants.AssetTypeCode.Unknown;
      this._isStylesheet = false;
      this._isScript = false;
      this._content = (string) null;
      this.VirtualPath = virtualPath;
      this.VirtualPathDependencies = (IList<string>) new List<string>();
      this.OriginalAssets = (IList<IAsset>) new List<IAsset>();
      this.Combined = false;
      this.Minified = false;
      this.RelativePathsResolved = false;
    }

    /// <summary>Applies a transformations to asset content</summary>
    /// <param name="content">Asset content</param>
    /// <returns>Processed asset content </returns>
    private string ApplyTransformsToContent(string content)
    {
      string input = content;
      if (this._transforms.Count > 0)
      {
        foreach (IItemTransform transform in (IEnumerable<IItemTransform>) this._transforms)
          input = transform.Process(this._includedVirtualPath, input);
      }
      return input;
    }

    /// <summary>
    /// Checks a whether an asset is minified version of CSS file
    /// with *.min.css extension
    /// </summary>
    /// <param name="assetVirtualPath">CSS asset virtual file path</param>
    /// <returns>Checking result (true - with *.min.css extension;
    /// false - without *.min.css extension)</returns>
    public static bool IsCssFileWithMinExtension(string assetVirtualPath) => Asset._cssFileWithMinExtensionRegex.IsMatch(assetVirtualPath);

    /// <summary>
    /// Checks a whether an asset is debug version of JS file
    /// with *.debug.js extension
    /// </summary>
    /// <param name="assetVirtualPath">JS asset virtual file path</param>
    /// <returns>Checking result (true - with *.debug.js extension;
    /// false - without *.debug.js extension)</returns>
    public static bool IsJsFileWithDebugExtension(string assetVirtualPath) => Asset._jsFileWithDebugExtensionRegex.IsMatch(assetVirtualPath);

    /// <summary>
    /// Checks a whether an asset is minified version of JS file with *.min.js extension
    /// </summary>
    /// <param name="assetVirtualPath">JS asset virtual file path</param>
    /// <returns>Checking result (true - with *.min.js extension;
    /// false - without *.min.js extension)</returns>
    public static bool IsJsFileWithMinExtension(string assetVirtualPath) => Asset._jsFileWithMinExtensionRegex.IsMatch(assetVirtualPath);

    /// <summary>
    /// Removes a additional file extension from path of the specified CSS file
    /// </summary>
    /// <param name="assetVirtualPath">CSS asset virtual file path</param>
    /// <returns>CSS asset virtual file path without additional file extension</returns>
    public static string RemoveAdditionalCssFileExtension(string assetVirtualPath) => Asset._cssFileWithMinExtensionRegex.Replace(assetVirtualPath, FileExtension.Css);

    /// <summary>
    /// Removes a additional file extension from path of the specified JS file
    /// </summary>
    /// <param name="assetVirtualPath">JS asset virtual file path</param>
    /// <returns>JS asset virtual file path without additional file extension</returns>
    public static string RemoveAdditionalJsFileExtension(string assetVirtualPath)
    {
      string input = Asset._jsFileWithDebugExtensionRegex.Replace(assetVirtualPath, FileExtension.JavaScript);
      return Asset._jsFileWithMinExtensionRegex.Replace(input, FileExtension.JavaScript);
    }
  }
}
