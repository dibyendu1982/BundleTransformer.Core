﻿// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Translators.NullTranslator
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Resources;
using System;
using System.Collections.Generic;

namespace BundleTransformer.Core.Translators
{
  /// <summary>Null translator (used as a placeholder)</summary>
  public sealed class NullTranslator : ITranslator
  {
    /// <summary>
    /// Gets or sets a flag that web application is in debug mode
    /// </summary>
    public bool IsDebugMode { get; set; }

    /// <summary>Do not performs operations with asset</summary>
    /// <param name="asset">Asset</param>
    /// <returns>Asset</returns>
    public IAsset Translate(IAsset asset) => asset != null ? asset : throw new ArgumentNullException(nameof (asset), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (asset)));

    /// <summary>Do not performs operations with assets</summary>
    /// <param name="assets">Set of assets</param>
    /// <returns>Set of assets</returns>
    public IList<IAsset> Translate(IList<IAsset> assets) => assets != null ? assets : throw new ArgumentNullException(nameof (assets), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (assets)));
  }
}
