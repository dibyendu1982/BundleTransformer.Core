// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Utilities.CharExtensions
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Runtime.CompilerServices;

namespace BundleTransformer.Core.Utilities
{
  /// <summary>Extensions for Char</summary>
  public static class CharExtensions
  {
    [MethodImpl((MethodImplOptions) 256)]
    public static bool IsWhitespace(this char source)
    {
      if (source == ' ')
        return true;
      return source >= '\t' && source <= '\r';
    }

    [MethodImpl((MethodImplOptions) 256)]
    public static bool IsNumeric(this char source) => source >= '0' && source <= '9';

    [MethodImpl((MethodImplOptions) 256)]
    public static bool IsAlphaLower(this char source) => source >= 'a' && source <= 'z';

    [MethodImpl((MethodImplOptions) 256)]
    public static bool IsAlphaUpper(this char source) => source >= 'A' && source <= 'Z';

    [MethodImpl((MethodImplOptions) 256)]
    public static bool IsAlpha(this char source) => source.IsAlphaLower() || source.IsAlphaUpper();

    [MethodImpl((MethodImplOptions) 256)]
    public static bool IsAlphaNumeric(this char source) => source.IsAlpha() || source.IsNumeric();
  }
}
