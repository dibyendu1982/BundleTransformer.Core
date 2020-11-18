// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Resolvers.CustomBundleResolver
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Constants;
using BundleTransformer.Core.Resources;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace BundleTransformer.Core.Resolvers
{
  /// <summary>
  /// Custom bundle resolver, that required in order to the debugging HTTP handler can use
  /// transformations of the corresponding bundle
  /// </summary>
  public sealed class CustomBundleResolver : IBundleResolver
  {
    /// <summary>Collection of bundles</summary>
    private readonly BundleCollection _bundles;
    /// <summary>HTTP context</summary>
    private HttpContextBase _httpContext;

    /// <summary>Gets or sets a HTTP context</summary>
    internal HttpContextBase Context
    {
      get => this._httpContext ?? (HttpContextBase) new HttpContextWrapper(HttpContext.Current);
      set => this._httpContext = value;
    }

    /// <summary>
    /// Constructs a instance of the <see cref="T:System.Web.Optimization.CustomBundleResolver" /> class
    /// </summary>
    public CustomBundleResolver()
      : this(BundleTable.Bundles)
    {
    }

    /// <summary>
    /// Constructs a instance of the <see cref="T:System.Web.Optimization.CustomBundleResolver" /> class
    /// with the specified bundle
    /// </summary>
    /// <param name="bundles">Collection of bundles</param>
    public CustomBundleResolver(BundleCollection bundles)
      : this(bundles, (HttpContextBase) null)
    {
    }

    /// <summary>
    /// Constructs a instance of the <see cref="T:System.Web.Optimization.CustomBundleResolver" /> class
    /// with the specified bundle and context
    /// </summary>
    /// <param name="bundles">Collection of bundles</param>
    /// <param name="context">HTTP context</param>
    public CustomBundleResolver(BundleCollection bundles, HttpContextBase context)
    {
      this._bundles = bundles;
      this.Context = context;
    }

    /// <summary>Determines if the virtual path is to a bundle</summary>
    /// <param name="virtualPath">Virtual path of bundle</param>
    /// <returns>Result of check (true - is virtual path; false - is not virtual path)</returns>
    public bool IsBundleVirtualPath(string virtualPath) => CustomBundleResolver.ValidateVirtualPath(virtualPath, nameof (virtualPath)) == null && this._bundles.GetBundleFor(virtualPath) != null;

    /// <summary>
    /// Gets a enumeration of actual file paths to the contents of bundle
    /// </summary>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <returns>Actual file paths to the contents of bundle</returns>
    public IEnumerable<string> GetBundleContents(string bundleVirtualPath)
    {
      if (CustomBundleResolver.ValidateVirtualPath(bundleVirtualPath, nameof (bundleVirtualPath)) != null)
        return (IEnumerable<string>) null;
      Bundle bundleFor = this._bundles.GetBundleFor(bundleVirtualPath);
      if (bundleFor == null)
        return (IEnumerable<string>) null;
      List<string> stringList = new List<string>();
      BundleContext context = new BundleContext(this.Context, this._bundles, bundleVirtualPath);
      IEnumerable<BundleFile> files = CustomBundleResolver.GetBundleResponse(bundleFor, context).Files;
      string str1 = this.Context.Server.UrlEncode(bundleVirtualPath);
      foreach (BundleFile bundleFile in files)
      {
        string includedVirtualPath;
        string str2 = ((includedVirtualPath = bundleFile.IncludedVirtualPath).IndexOf("?", StringComparison.Ordinal) == -1 ? includedVirtualPath + "?" : includedVirtualPath + "&") + Common.BundleVirtualPathQueryStringParameterName + "=" + str1;
        stringList.Add(str2);
      }
      return (IEnumerable<string>) stringList;
    }

    /// <summary>
    /// Gets a versioned url for bundle or returns the virtual path unchanged if it does not point to a bundle
    /// </summary>
    /// <param name="virtualPath">Virtual file path</param>
    /// <returns>Versioned url for bundle</returns>
    public string GetBundleUrl(string virtualPath) => CustomBundleResolver.ValidateVirtualPath(virtualPath, nameof (virtualPath)) != null ? (string) null : this._bundles.ResolveBundleUrl(virtualPath);

    private static BundleResponse GetBundleResponse(
      Bundle bundle,
      BundleContext context)
    {
      BundleResponse response = bundle.CacheLookup(context);
      if (response == null || context.EnableInstrumentation)
      {
        response = bundle.GenerateBundleResponse(context);
        bundle.UpdateCache(context, response);
      }
      return response;
    }

    private static Exception ValidateVirtualPath(string virtualPath, string argumentName)
    {
      if (string.IsNullOrWhiteSpace(virtualPath))
        return (Exception) new ArgumentException(string.Format(Strings.Common_ArgumentIsEmpty, (object) argumentName), argumentName);
      return !virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase) ? (Exception) new ArgumentException(string.Format(Strings.UrlMappings_OnlyAppRelativeUrlAllowed, (object) virtualPath), argumentName) : (Exception) null;
    }
  }
}
