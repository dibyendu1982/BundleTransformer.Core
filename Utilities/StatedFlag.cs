// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Utilities.StatedFlag
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.Utilities
{
  public struct StatedFlag
  {
    private bool _isSet;

    public bool IsSet() => this._isSet;

    public bool Set()
    {
      if (this._isSet)
        return false;
      this._isSet = true;
      return true;
    }
  }
}
