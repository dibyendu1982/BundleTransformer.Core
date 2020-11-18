// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.AssetNodeMatchBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Text.RegularExpressions;

namespace BundleTransformer.Core
{
  /// <summary>Asset node match</summary>
  public abstract class AssetNodeMatchBase
  {
    /// <summary>
    /// Gets a position in the original string where
    /// the first character of the captured substring was found
    /// </summary>
    public int Position { get; private set; }

    /// <summary>Gets a length of the captured substring</summary>
    public int Length { get; private set; }

    /// <summary>Gets a single regular expression match</summary>
    public Match Match { get; private set; }

    /// <summary>Constructs a instance of asset node match</summary>
    /// <param name="position">Position in the original string where
    /// the first character of the captured substring was found</param>
    /// <param name="length">Length of the captured substring</param>
    /// <param name="match">Single regular expression match</param>
    protected AssetNodeMatchBase(int position, int length, Match match)
    {
      this.Position = position;
      this.Length = length;
      this.Match = match;
    }
  }
}
