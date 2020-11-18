// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.CssNodeMatch
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Text.RegularExpressions;

namespace BundleTransformer.Core
{
  /// <summary>CSS node match</summary>
  public sealed class CssNodeMatch : AssetNodeMatchBase
  {
    /// <summary>Gets a type of CSS node</summary>
    public CssNodeType NodeType { get; private set; }

    /// <summary>Constructs a instance of CSS node match</summary>
    /// <param name="position">Position in the original string where
    /// the first character of the captured substring was found</param>
    /// <param name="length">Length of the captured substring</param>
    /// <param name="nodeType">Type of CSS node</param>
    /// <param name="match">Single regular expression match</param>
    public CssNodeMatch(int position, int length, CssNodeType nodeType, Match match)
      : base(position, length, match)
      => this.NodeType = nodeType;
  }
}
