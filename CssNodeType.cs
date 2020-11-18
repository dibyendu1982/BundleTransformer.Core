// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.CssNodeType
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core
{
  /// <summary>CSS node types</summary>
  public enum CssNodeType
  {
    /// <summary>Unknown node</summary>
    Unknown,
    /// <summary>
    /// <code>@charset</code> rule
    /// </summary>
    CharsetRule,
    /// <summary>
    /// <code>@import</code> rule
    /// </summary>
    ImportRule,
    /// <summary>
    /// <code>url</code> rule
    /// </summary>
    UrlRule,
    /// <summary>Multiline comment</summary>
    MultilineComment,
  }
}
