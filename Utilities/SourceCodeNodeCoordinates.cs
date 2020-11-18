// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Utilities.SourceCodeNodeCoordinates
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.Utilities
{
  /// <summary>Source code node coordinates</summary>
  public struct SourceCodeNodeCoordinates
  {
    /// <summary>Line number</summary>
    private int _lineNumber;
    /// <summary>Column number</summary>
    private int _columnNumber;
    /// <summary>
    /// Represents a node coordinates that has line number and column number values set to zero.
    /// </summary>
    public static readonly SourceCodeNodeCoordinates Empty = new SourceCodeNodeCoordinates(0, 0);

    /// <summary>Gets or sets a line number</summary>
    public int LineNumber
    {
      get => this._lineNumber;
      set => this._lineNumber = value;
    }

    /// <summary>Gets or sets a column number</summary>
    public int ColumnNumber
    {
      get => this._columnNumber;
      set => this._columnNumber = value;
    }

    /// <summary>
    /// Gets a value indicating whether this node coordinates is empty
    /// </summary>
    public bool IsEmpty => this.LineNumber == 0 && this.ColumnNumber == 0;

    /// <summary>
    /// Constructs an instance of source code node coordinates
    /// </summary>
    /// <param name="lineNumber">Line number</param>
    /// <param name="columnNumber">Column number</param>
    public SourceCodeNodeCoordinates(int lineNumber, int columnNumber)
    {
      this._lineNumber = lineNumber;
      this._columnNumber = columnNumber;
    }
  }
}
