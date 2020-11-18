// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Utilities.SourceCodeNavigator
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using AdvancedStringBuilder;
using System;
using System.Globalization;
using System.Text;

namespace BundleTransformer.Core.Utilities
{
  public static class SourceCodeNavigator
  {
    private const byte DEFAULT_TAB_SIZE = 4;
    private const int DEFAULT_MAX_FRAGMENT_LENGTH = 95;
    /// <summary>Array of characters used to find the next line break</summary>
    private static readonly char[] _nextLineBreakChars = new char[2]
    {
      '\r',
      '\n'
    };
    /// <summary>
    /// Array of characters used to find the previous line break
    /// </summary>
    private static readonly char[] _previousLineBreakChars = new char[2]
    {
      '\n',
      '\r'
    };

    /// <summary>Finds a next line break</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="startPosition">Position in the input string that defines the leftmost
    /// position to be searched</param>
    /// <param name="lineBreakPosition">Position of line break</param>
    /// <param name="lineBreakLength">Length of line break</param>
    private static void FindNextLineBreak(
      string sourceCode,
      int startPosition,
      out int lineBreakPosition,
      out int lineBreakLength)
    {
      int length = sourceCode.Length - startPosition;
      SourceCodeNavigator.FindNextLineBreak(sourceCode, startPosition, length, out lineBreakPosition, out lineBreakLength);
    }

    /// <summary>Finds a next line break</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="startPosition">Position in the input string that defines the leftmost
    /// position to be searched</param>
    /// <param name="length">Number of characters in the substring to include in the search</param>
    /// <param name="lineBreakPosition">Position of line break</param>
    /// <param name="lineBreakLength">Length of line break</param>
    private static void FindNextLineBreak(
      string sourceCode,
      int startPosition,
      int length,
      out int lineBreakPosition,
      out int lineBreakLength)
    {
      lineBreakPosition = sourceCode.IndexOfAny(SourceCodeNavigator._nextLineBreakChars, startPosition, length);
      if (lineBreakPosition != -1)
      {
        lineBreakLength = 1;
        if (sourceCode[lineBreakPosition] != '\r')
          return;
        int index = lineBreakPosition + 1;
        char result;
        if (!sourceCode.TryGetChar(index, out result) || result != '\n')
          return;
        lineBreakLength = 2;
      }
      else
        lineBreakLength = 0;
    }

    /// <summary>Finds a previous line break</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="startPosition">Position in the input string that defines the leftmost
    /// position to be searched</param>
    /// <param name="lineBreakPosition">Position of line break</param>
    /// <param name="lineBreakLength">Length of line break</param>
    private static void FindPreviousLineBreak(
      string sourceCode,
      int startPosition,
      out int lineBreakPosition,
      out int lineBreakLength)
    {
      lineBreakPosition = sourceCode.LastIndexOfAny(SourceCodeNavigator._previousLineBreakChars, startPosition);
      if (lineBreakPosition != -1)
      {
        lineBreakLength = 1;
        if (sourceCode[lineBreakPosition] != '\n')
          return;
        int index = lineBreakPosition - 1;
        char result;
        if (!sourceCode.TryGetChar(index, out result) || result != '\r')
          return;
        lineBreakPosition = index;
        lineBreakLength = 2;
      }
      else
        lineBreakLength = 0;
    }

    /// <summary>Calculates a line break count</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="lineBreakCount">Number of line breaks</param>
    /// <param name="charRemainderCount">Number of characters left</param>
    public static void CalculateLineBreakCount(
      string sourceCode,
      out int lineBreakCount,
      out int charRemainderCount)
    {
      SourceCodeNavigator.CalculateLineBreakCount(sourceCode, 0, out lineBreakCount, out charRemainderCount);
    }

    /// <summary>Calculates a line break count</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="fragmentStartPosition">Start position of fragment</param>
    /// <param name="lineBreakCount">Number of line breaks</param>
    /// <param name="charRemainderCount">Number of characters left</param>
    public static void CalculateLineBreakCount(
      string sourceCode,
      int fragmentStartPosition,
      out int lineBreakCount,
      out int charRemainderCount)
    {
      int fragmentLength = sourceCode.Length - fragmentStartPosition;
      SourceCodeNavigator.CalculateLineBreakCount(sourceCode, fragmentStartPosition, fragmentLength, out lineBreakCount, out charRemainderCount);
    }

    /// <summary>Calculates a line break count</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="fragmentStartPosition">Start position of fragment</param>
    /// <param name="fragmentLength">Length of fragment</param>
    /// <param name="lineBreakCount">Number of line breaks</param>
    /// <param name="charRemainderCount">Number of characters left</param>
    public static void CalculateLineBreakCount(
      string sourceCode,
      int fragmentStartPosition,
      int fragmentLength,
      out int lineBreakCount,
      out int charRemainderCount)
    {
      int length1 = sourceCode.Length;
      lineBreakCount = 0;
      charRemainderCount = 0;
      if (string.IsNullOrWhiteSpace(sourceCode))
        return;
      if (fragmentStartPosition < 0)
        throw new ArgumentException("", nameof (fragmentStartPosition));
      if (fragmentLength > length1 - fragmentStartPosition)
        throw new ArgumentException("", nameof (fragmentLength));
      int num = fragmentStartPosition + fragmentLength - 1;
      int lineBreakPosition = int.MinValue;
      int lineBreakLength = 0;
      int startPosition;
      do
      {
        startPosition = lineBreakPosition == int.MinValue ? fragmentStartPosition : lineBreakPosition + lineBreakLength;
        int length2 = num - startPosition + 1;
        SourceCodeNavigator.FindNextLineBreak(sourceCode, startPosition, length2, out lineBreakPosition, out lineBreakLength);
        if (lineBreakPosition != -1)
          ++lineBreakCount;
      }
      while (lineBreakPosition != -1 && lineBreakPosition <= num);
      if (lineBreakCount > 0)
        charRemainderCount = num - startPosition + 1;
      else
        charRemainderCount = fragmentLength;
    }

    /// <summary>Calculates a node coordinates</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="nodePosition">Current node position</param>
    /// <returns>Node coordinates</returns>
    public static SourceCodeNodeCoordinates CalculateNodeCoordinates(
      string sourceCode,
      int nodePosition)
    {
      if (string.IsNullOrEmpty(sourceCode) || nodePosition >= sourceCode.Length)
        return SourceCodeNodeCoordinates.Empty;
      int fragmentLength = nodePosition + 1;
      int lineBreakCount;
      int charRemainderCount;
      SourceCodeNavigator.CalculateLineBreakCount(sourceCode, 0, fragmentLength, out lineBreakCount, out charRemainderCount);
      return new SourceCodeNodeCoordinates(lineBreakCount + 1, charRemainderCount + 1);
    }

    /// <summary>Gets a source fragment</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="nodePosition">Current node position</param>
    /// <param name="tabSize">Number of spaces in the tab</param>
    /// <param name="maxFragmentLength">Maximum length of the source fragment</param>
    /// <returns>Source fragment</returns>
    public static string GetSourceFragment(
      string sourceCode,
      int nodePosition,
      byte tabSize = 4,
      int maxFragmentLength = 95)
    {
      SourceCodeNodeCoordinates nodeCoordinates = SourceCodeNavigator.CalculateNodeCoordinates(sourceCode, nodePosition);
      return SourceCodeNavigator.GetSourceFragment(sourceCode, nodeCoordinates, tabSize);
    }

    /// <summary>Gets a source fragment</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="nodeCoordinates">Node coordinates</param>
    /// <param name="tabSize">Number of spaces in the tab</param>
    /// <param name="maxFragmentLength">Maximum length of the source fragment</param>
    /// <returns>Source fragment</returns>
    public static string GetSourceFragment(
      string sourceCode,
      SourceCodeNodeCoordinates nodeCoordinates,
      byte tabSize = 4,
      int maxFragmentLength = 95)
    {
      string empty = string.Empty;
      int lineNumber1 = nodeCoordinates.LineNumber;
      int columnNumber = nodeCoordinates.ColumnNumber;
      if (!string.IsNullOrEmpty(sourceCode))
      {
        int lineNumber2 = lineNumber1 - 1;
        int lineNumber3 = lineNumber1;
        int lineNumber4 = lineNumber1 + 1;
        string line1 = string.Empty;
        string line2 = string.Empty;
        string line3 = string.Empty;
        int num1 = 0;
        int lineBreakPosition = int.MinValue;
        int lineBreakLength = 0;
        do
        {
          int num2 = lineBreakPosition == int.MinValue ? 0 : lineBreakPosition + lineBreakLength;
          SourceCodeNavigator.FindNextLineBreak(sourceCode, num2, out lineBreakPosition, out lineBreakLength);
          string str = lineBreakPosition == -1 ? sourceCode.Substring(num2) : sourceCode.Substring(num2, lineBreakPosition - num2);
          ++num1;
          if (num1 == lineNumber2)
            line1 = str;
          else if (num1 == lineNumber3)
            line2 = str;
          else if (num1 == lineNumber4)
            line3 = str;
        }
        while (lineBreakPosition != -1 && num1 <= lineNumber4);
        int length = lineNumber4.ToString((IFormatProvider) CultureInfo.InvariantCulture).Length;
        if (lineNumber3 == num1)
          length = lineNumber3.ToString((IFormatProvider) CultureInfo.InvariantCulture).Length;
        int fragmentStartPosition;
        int fragmentLength;
        SourceCodeNavigator.CalculateCutPositions(line2, columnNumber, maxFragmentLength, out fragmentStartPosition, out fragmentLength);
        StringBuilderPool shared = StringBuilderPool.Shared;
        StringBuilder builder = shared.Rent();
        if (line2.Length > 0)
        {
          if (line1.Length > 0)
            builder.AppendLine(SourceCodeNavigator.FormatSourceCodeLine(line1, new SourceCodeNodeCoordinates(lineNumber2, 0), length, fragmentStartPosition, fragmentLength, tabSize));
          builder.AppendLine(SourceCodeNavigator.FormatSourceCodeLine(line2, new SourceCodeNodeCoordinates(lineNumber3, columnNumber), length, fragmentStartPosition, fragmentLength, tabSize));
          if (line3.Length > 0)
            builder.AppendLine(SourceCodeNavigator.FormatSourceCodeLine(line3, new SourceCodeNodeCoordinates(lineNumber4, 0), length, fragmentStartPosition, fragmentLength, tabSize));
        }
        empty = builder.ToString();
        shared.Return(builder);
      }
      return empty;
    }

    /// <summary>Calculates a cut positions</summary>
    /// <param name="line">Line content</param>
    /// <param name="columnNumber">Column number</param>
    /// <param name="maxFragmentLength">Maximum length of the source fragment</param>
    /// <param name="fragmentStartPosition">Start position of source fragment</param>
    /// <param name="fragmentLength">Length of source fragment</param>
    private static void CalculateCutPositions(
      string line,
      int columnNumber,
      int maxFragmentLength,
      out int fragmentStartPosition,
      out int fragmentLength)
    {
      int length = line.Length;
      int num1 = (int) Math.Floor((double) maxFragmentLength / 2.0);
      fragmentStartPosition = 0;
      fragmentLength = maxFragmentLength;
      int num2 = maxFragmentLength;
      if (length <= num2)
        return;
      fragmentStartPosition = columnNumber - num1 - 1;
      if (fragmentStartPosition < 0)
        fragmentStartPosition = 0;
      fragmentLength = maxFragmentLength - 2;
    }

    /// <summary>Formats a line of source code</summary>
    /// <param name="line">Line content</param>
    /// <param name="nodeCoordinates">Node coordinates</param>
    /// <param name="lineNumberSize">Number of symbols in the line number caption</param>
    /// <param name="fragmentStartPosition">Start position of source fragment</param>
    /// <param name="fragmentLength">Length of source fragment</param>
    /// <param name="tabSize">Number of spaces in the tab</param>
    /// <returns>Formatted line</returns>
    private static string FormatSourceCodeLine(
      string line,
      SourceCodeNodeCoordinates nodeCoordinates,
      int lineNumberSize,
      int fragmentStartPosition = 0,
      int fragmentLength = 0,
      byte tabSize = 4)
    {
      int lineNumber = nodeCoordinates.LineNumber;
      int columnNumber = nodeCoordinates.ColumnNumber;
      int length = line.Length;
      string source;
      if (fragmentStartPosition == 0 && fragmentLength == length)
        source = line;
      else if (fragmentStartPosition >= length)
      {
        source = string.Empty;
      }
      else
      {
        int num = fragmentStartPosition + fragmentLength - 1;
        bool flag1 = fragmentStartPosition > 0;
        bool flag2 = num <= length;
        if (num + 1 == length)
          flag2 = false;
        if (num >= length)
        {
          flag2 = false;
          fragmentLength = length - 1 - fragmentStartPosition + 1;
        }
        source = line.Substring(fragmentStartPosition, fragmentLength);
        if (flag1)
          source = "…" + source;
        if (flag2)
          source += "…";
      }
      string str = string.Format("Line {0}: {1}", (object) lineNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture).PadLeft(lineNumberSize), (object) source.TabsToSpaces((int) tabSize));
      if (columnNumber > 0)
      {
        int num = columnNumber - fragmentStartPosition;
        if (fragmentStartPosition > 0)
          ++num;
        str = str + Environment.NewLine + string.Empty.PadRight((num < source.Length ? source.Substring(0, num - 1) : source).TabsToSpaces((int) tabSize).Length + lineNumberSize + 7).Replace(" ", "-") + "^";
      }
      return str;
    }

    /// <summary>Calculates a absolute node coordinates</summary>
    /// <param name="baseNodeCoordinates">Base node coordinates</param>
    /// <param name="relativeNodeCoordinates">Relative node coordinates</param>
    /// <returns>Absolute node coordinates</returns>
    public static SourceCodeNodeCoordinates CalculateAbsoluteNodeCoordinates(
      SourceCodeNodeCoordinates baseNodeCoordinates,
      SourceCodeNodeCoordinates relativeNodeCoordinates)
    {
      int lineNumber1 = relativeNodeCoordinates.LineNumber;
      int columnNumber1 = relativeNodeCoordinates.ColumnNumber;
      int lineNumber2;
      int columnNumber2;
      if (!baseNodeCoordinates.IsEmpty)
      {
        int lineNumber3 = baseNodeCoordinates.LineNumber;
        int columnNumber3 = baseNodeCoordinates.ColumnNumber;
        lineNumber2 = lineNumber3;
        columnNumber2 = columnNumber3;
        if (lineNumber1 > 0)
        {
          if (lineNumber1 == 1)
          {
            if (columnNumber1 > 0)
              columnNumber2 = columnNumber3 + columnNumber1 - 1;
          }
          else
          {
            lineNumber2 = lineNumber3 + lineNumber1 - 1;
            columnNumber2 = columnNumber1;
          }
        }
      }
      else
      {
        lineNumber2 = lineNumber1;
        columnNumber2 = columnNumber1;
      }
      return new SourceCodeNodeCoordinates(lineNumber2, columnNumber2);
    }

    /// <summary>Calculates a absolute node coordinates</summary>
    /// <param name="baseNodeCoordinates">Base node coordinates</param>
    /// <param name="additionalContent">Additional content</param>
    /// <returns>Absolute node coordinates</returns>
    public static SourceCodeNodeCoordinates CalculateAbsoluteNodeCoordinates(
      SourceCodeNodeCoordinates baseNodeCoordinates,
      string additionalContent)
    {
      int lineBreakCount = 0;
      int charRemainderCount = 0;
      if (!string.IsNullOrEmpty(additionalContent))
        SourceCodeNavigator.CalculateLineBreakCount(additionalContent, out lineBreakCount, out charRemainderCount);
      int lineNumber1;
      int columnNumber1;
      if (!baseNodeCoordinates.IsEmpty)
      {
        int lineNumber2 = baseNodeCoordinates.LineNumber;
        int columnNumber2 = baseNodeCoordinates.ColumnNumber;
        if (lineBreakCount > 0)
        {
          lineNumber1 = lineNumber2 + lineBreakCount;
          columnNumber1 = charRemainderCount + 1;
        }
        else
        {
          lineNumber1 = lineNumber2;
          columnNumber1 = columnNumber2 + charRemainderCount;
        }
      }
      else
      {
        lineNumber1 = lineBreakCount + 1;
        columnNumber1 = charRemainderCount + 1;
      }
      return new SourceCodeNodeCoordinates(lineNumber1, columnNumber1);
    }

    /// <summary>Calculates a absolute node coordinates</summary>
    /// <param name="baseNodeCoordinates">Base node coordinates</param>
    /// <param name="lineBreakCount">Number of line breaks</param>
    /// <param name="charRemainderCount">Number of characters left</param>
    /// <returns>Absolute node coordinates</returns>
    public static SourceCodeNodeCoordinates CalculateAbsoluteNodeCoordinates(
      SourceCodeNodeCoordinates baseNodeCoordinates,
      int lineBreakCount,
      int charRemainderCount)
    {
      int lineNumber1;
      int columnNumber1;
      if (!baseNodeCoordinates.IsEmpty)
      {
        int lineNumber2 = baseNodeCoordinates.LineNumber;
        int columnNumber2 = baseNodeCoordinates.ColumnNumber;
        if (lineBreakCount > 0)
        {
          lineNumber1 = lineNumber2 + lineBreakCount;
          columnNumber1 = charRemainderCount + 1;
        }
        else
        {
          lineNumber1 = lineNumber2;
          columnNumber1 = columnNumber2 + charRemainderCount;
        }
      }
      else
      {
        lineNumber1 = lineBreakCount + 1;
        columnNumber1 = charRemainderCount + 1;
      }
      return new SourceCodeNodeCoordinates(lineNumber1, columnNumber1);
    }

    /// <summary>Gets a current line content</summary>
    /// <param name="sourceCode">Source code</param>
    /// <param name="currentPosition">Current position</param>
    /// <param name="startLinePosition">Start position of line</param>
    /// <param name="endLinePosition">End position of line</param>
    /// <returns>Line content</returns>
    public static string GetCurrentLine(
      string sourceCode,
      int currentPosition,
      out int startLinePosition,
      out int endLinePosition)
    {
      startLinePosition = -1;
      endLinePosition = -1;
      if (string.IsNullOrEmpty(sourceCode))
        return string.Empty;
      int length1 = sourceCode.Length;
      if (currentPosition >= length1)
        throw new ArgumentException("", nameof (currentPosition));
      switch (sourceCode[currentPosition])
      {
        case '\n':
        case '\r':
          return string.Empty;
        default:
          int lineBreakPosition1;
          int lineBreakLength;
          SourceCodeNavigator.FindPreviousLineBreak(sourceCode, currentPosition, out lineBreakPosition1, out lineBreakLength);
          startLinePosition = lineBreakPosition1 == -1 ? 0 : lineBreakPosition1 + lineBreakLength;
          int lineBreakPosition2;
          SourceCodeNavigator.FindNextLineBreak(sourceCode, currentPosition, out lineBreakPosition2, out int _);
          endLinePosition = lineBreakPosition2 == -1 ? length1 - 1 : lineBreakPosition2 - 1;
          int length2 = endLinePosition - startLinePosition + 1;
          return sourceCode.Substring(startLinePosition, length2);
      }
    }
  }
}
