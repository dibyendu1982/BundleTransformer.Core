// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Transformers.Tracer
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using AdvancedStringBuilder;
using System.Collections.Generic;
using System.Text;
using System.Web.Optimization;

namespace BundleTransformer.Core.Transformers
{
  /// <summary>
  /// Transformer that responsible to output trace information
  /// </summary>
  public sealed class Tracer : IBundleTransform
  {
    /// <summary>Displays trace information</summary>
    /// <param name="context">Object BundleContext</param>
    /// <param name="response">Object BundleResponse</param>
    public void Process(BundleContext context, BundleResponse response)
    {
      StringBuilderPool shared = StringBuilderPool.Shared;
      StringBuilder stringBuilder = shared.Rent();
      stringBuilder.AppendLine("*************************************************************************************");
      stringBuilder.AppendLine("* BUNDLE RESPONSE                                                                   *");
      stringBuilder.AppendLine("*************************************************************************************");
      foreach (BundleFile file in response.Files)
        stringBuilder.AppendLine("  " + file.IncludedVirtualPath);
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("*************************************************************************************");
      stringBuilder.AppendLine("* BUNDLE COLLECTION                                                                 *");
      stringBuilder.AppendLine("*************************************************************************************");
      foreach (Bundle bundle in (IEnumerable<Bundle>) context.BundleCollection)
      {
        stringBuilder.AppendFormatLine("-= {0} =-", (object) bundle.Path);
        foreach (BundleFile enumerateFile in bundle.EnumerateFiles(context))
          stringBuilder.AppendLine("  " + enumerateFile.IncludedVirtualPath);
        stringBuilder.AppendLine();
      }
      string str = stringBuilder.ToString();
      shared.Return(stringBuilder);
      response.ContentType = "text/plain";
      response.Content = str;
    }
  }
}
