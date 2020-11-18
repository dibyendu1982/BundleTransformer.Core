// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Validators.IValidator
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System.Collections.Generic;

namespace BundleTransformer.Core.Validators
{
  /// <summary>Defines interface of asset validator</summary>
  internal interface IValidator
  {
    /// <summary>Validates a assets</summary>
    /// <param name="assets">Set of assets</param>
    void Validate(IList<IAsset> assets);
  }
}
