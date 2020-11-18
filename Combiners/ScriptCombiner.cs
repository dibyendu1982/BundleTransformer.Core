// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Combiners.ScriptCombiner
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using AdvancedStringBuilder;
using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace BundleTransformer.Core.Combiners
{
  /// <summary>Script asset combiner</summary>
  public sealed class ScriptCombiner : CombinerBase
  {
    protected override string GenerateCombinedAssetVirtualPath(string bundleVirtualPath)
    {
      string str = bundleVirtualPath.TrimEnd();
      string javaScript = FileExtension.JavaScript;
      if (!str.EndsWith(javaScript, StringComparison.OrdinalIgnoreCase))
        str += javaScript;
      return str;
    }

    protected override string CombineAssetContent(IList<IAsset> assets)
    {
      StringBuilderPool shared = StringBuilderPool.Shared;
      StringBuilder stringBuilder = shared.Rent();
      int count = assets.Count;
      int num = count - 1;
      for (int index = 0; index < count; ++index)
      {
        IAsset asset = assets[index];
        string str = asset.Content.TrimEnd();
        if (this.EnableTracing)
          stringBuilder.AppendFormatLine("//#region URL: {0}", (object) asset.Url);
        stringBuilder.Append(str);
        if (!str.EndsWith(";"))
          stringBuilder.Append(";");
        if (this.EnableTracing)
        {
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("//#endregion");
        }
        if (index != num)
          stringBuilder.AppendLine();
      }
      string str1 = stringBuilder.ToString();
      shared.Return(stringBuilder);
      return str1;
    }
  }
}
