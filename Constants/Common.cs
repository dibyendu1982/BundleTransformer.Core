// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Constants.Common
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.Constants
{
  /// <summary>Common constants</summary>
  public static class Common
  {
    /// <summary>
    /// Relative path to directory that contains temporary files
    /// </summary>
    public static readonly string TempFilesDirectoryPath = "~/App_Data/BundleTransformer/Temp/";
    /// <summary>
    /// Pattern of cache item key, which stores text content of the processed asset
    /// </summary>
    public static readonly string ProcessedAssetContentCacheItemKeyPattern = "BT:ProcessedAssetContent_{0}";
    /// <summary>
    /// Name of QueryString parameter that contains the virtual path of the bundle
    /// </summary>
    public static readonly string BundleVirtualPathQueryStringParameterName = "bundleVirtualPath";
  }
}
