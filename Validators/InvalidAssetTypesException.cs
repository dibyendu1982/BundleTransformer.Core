// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Validators.InvalidAssetTypesException
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;

namespace BundleTransformer.Core.Validators
{
  /// <summary>
  /// The exception that is thrown when a assets are invalid types
  /// </summary>
  public sealed class InvalidAssetTypesException : Exception
  {
    /// <summary>
    /// Gets or sets a virtual paths of assets with invalid types
    /// </summary>
    public string[] InvalidAssetsVirtualPaths { get; set; }

    /// <summary>
    /// Initializes a new instance of the <code>BundleTransformer.Core.Validators.InvalidAssetTypesException</code> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception</param>
    /// <param name="invalidAssetsVirtualPaths">Virtual paths of assets with invalid types</param>
    public InvalidAssetTypesException(string message, string[] invalidAssetsVirtualPaths)
      : base(message)
      => this.InvalidAssetsVirtualPaths = invalidAssetsVirtualPaths;
  }
}
