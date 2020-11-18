// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.Dependency
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

namespace BundleTransformer.Core.Assets
{
  /// <summary>Asset dependency</summary>
  public sealed class Dependency
  {
    /// <summary>Gets a URL of dependency file</summary>
    public string Url { get; private set; }

    /// <summary>Gets or sets a text content of dependency file</summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating what this dependency file is observable
    /// </summary>
    public bool IsObservable { get; set; }

    /// <summary>Constructs a instance of the asset dependency</summary>
    public Dependency()
      : this(string.Empty)
    {
    }

    /// <summary>Constructs a instance of the asset dependency</summary>
    /// <param name="url">URL of dependency file</param>
    public Dependency(string url)
      : this(url, string.Empty)
    {
    }

    /// <summary>Constructs a instance of the asset dependency</summary>
    /// <param name="url">URL of dependency file</param>
    /// <param name="content">Text content of dependency file</param>
    public Dependency(string url, string content)
      : this(url, content, true)
    {
    }

    /// <summary>Constructs a instance of the asset dependency</summary>
    /// <param name="url">URL of dependency file</param>
    /// <param name="content">Text content of dependency file</param>
    /// <param name="isObservable">Flag indicating what this dependency file is observable</param>
    public Dependency(string url, string content, bool isObservable)
    {
      this.Url = url;
      this.Content = content;
      this.IsObservable = isObservable;
    }
  }
}
