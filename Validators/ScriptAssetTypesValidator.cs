// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Validators.ScriptAssetTypesValidator
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BundleTransformer.Core.Validators
{
  /// <summary>
  /// Validator that checks whether the specified assets are script
  /// </summary>
  public sealed class ScriptAssetTypesValidator : IValidator
  {
    /// <summary>
    /// Validates whether the specified assets are script assets
    /// </summary>
    /// <param name="assets">Set of assets</param>
    public void Validate(IList<IAsset> assets)
    {
      if (assets == null)
        throw new ArgumentNullException(nameof (assets), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (assets)));
      if (assets.Count == 0)
        return;
      IList<IAsset> list = (IList<IAsset>) assets.Where<IAsset>((Func<IAsset, bool>) (a => !a.IsScript)).ToList<IAsset>();
      if (list.Count > 0)
      {
        string[] array = list.Select<IAsset, string>((Func<IAsset, string>) (a => a.VirtualPath)).ToArray<string>();
        throw new InvalidAssetTypesException(string.Format(BundleTransformer.Core.Resources.Strings.Assets_ScriptAssetsContainAssetsWithInvalidTypes, (object) string.Join(", ", array)), array);
      }
    }
  }
}
