// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.FileExtensionMapping
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.Assets
{
  /// <summary>File extension mapping</summary>
  public sealed class FileExtensionMapping
  {
    /// <summary>Gets a file extension</summary>
    public string FileExtension { get; private set; }

    /// <summary>Gets a asset type code</summary>
    public string AssetTypeCode { get; private set; }

    /// <summary>Constructs a instance of file extension mapping</summary>
    /// <param name="fileExtension">File extension</param>
    /// <param name="assetTypeCode">Asset type code</param>
    public FileExtensionMapping(string fileExtension, string assetTypeCode)
    {
      this.FileExtension = fileExtension;
      this.AssetTypeCode = assetTypeCode;
    }
  }
}
