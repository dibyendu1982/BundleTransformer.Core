﻿// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Minifiers.MinifierNotFoundException
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;

namespace BundleTransformer.Core.Minifiers
{
  /// <summary>
  /// The exception that is thrown when a minifier is not found
  /// </summary>
  public sealed class MinifierNotFoundException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <code>BundleTransformer.Core.Minifiers.MinifierNotFoundException</code> class
    /// with a specified error message
    /// </summary>
    /// <param name="message">The message that describes the error</param>
    public MinifierNotFoundException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <code>BundleTransformer.Core.Minifiers.MinifierNotFoundException</code> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception</param>
    /// <param name="innerException">The exception that is the cause of the current exception</param>
    public MinifierNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
