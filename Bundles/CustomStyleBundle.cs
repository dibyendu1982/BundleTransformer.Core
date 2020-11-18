// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Bundles.CustomStyleBundle
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Builders;
using System.Web.Optimization;

namespace BundleTransformer.Core.Bundles
{
  /// <summary>
  /// Bundle that uses StyleTransformer as transformation by default
  /// and NullBuilder as builder by default
  /// </summary>
  public sealed class CustomStyleBundle : Bundle
  {
    /// <summary>Constructs a instance of custom style bundle</summary>
    /// <param name="virtualPath">Virtual path of bundle</param>
    public CustomStyleBundle(string virtualPath)
      : this(virtualPath, (string) null)
    {
    }

    /// <summary>Constructs a instance of custom style bundle</summary>
    /// <param name="virtualPath">Virtual path of bundle</param>
    /// <param name="cdnPath">Path of bundle on CDN</param>
    public CustomStyleBundle(string virtualPath, string cdnPath)
      : base(virtualPath, cdnPath, BundleTransformerContext.Current.Styles.GetDefaultTransformInstance())
      => this.Builder = (IBundleBuilder) new NullBuilder();
  }
}
