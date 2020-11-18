﻿// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Translators.AssetTranslationException
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System;

namespace BundleTransformer.Core.Translators
{
  /// <summary>
  /// The exception that is thrown when a translation of asset code, written
  /// on intermediate language, is failed
  /// </summary>
  public sealed class AssetTranslationException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <code>BundleTransformer.Core.Translators.AssetTranslationException</code> class
    /// with a specified error message
    /// </summary>
    /// <param name="message">The message that describes the error</param>
    public AssetTranslationException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <code>BundleTransformer.Core.Translators.AssetTranslationException</code> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception</param>
    /// <param name="innerException">The exception that is the cause of the current exception</param>
    public AssetTranslationException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
