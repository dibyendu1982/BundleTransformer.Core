// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Utilities.InterlockedStatedFlag
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.Threading;

namespace BundleTransformer.Core.Utilities
{
  public struct InterlockedStatedFlag
  {
    private int _counter;

    public bool IsSet() => (uint) this._counter > 0U;

    public bool Set() => Interlocked.Exchange(ref this._counter, 1) == 0;
  }
}
