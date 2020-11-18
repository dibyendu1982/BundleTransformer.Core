// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.HttpHandlers.AssetHandlerBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Assets;
using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Constants;
using BundleTransformer.Core.FileSystem;
using BundleTransformer.Core.Helpers;
using BundleTransformer.Core.PostProcessors;
using BundleTransformer.Core.Transformers;
using BundleTransformer.Core.Translators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Optimization;

namespace BundleTransformer.Core.HttpHandlers
{
  /// <summary>
  /// Base class of the debugging HTTP handler that responsible for text output
  /// of processed asset
  /// </summary>
  public abstract class AssetHandlerBase : IHttpHandler
  {
    /// <summary>HTTP context</summary>
    protected HttpContextBase _context;
    /// <summary>Server cache</summary>
    protected readonly Cache _cache;
    /// <summary>Synchronizer of requests to server cache</summary>
    private static readonly object _cacheSynchronizer = new object();
    /// <summary>Virtual file system wrapper</summary>
    private readonly IVirtualFileSystemWrapper _virtualFileSystemWrapper;
    /// <summary>Configuration settings of the debugging HTTP handler</summary>
    private readonly AssetHandlerSettings _assetHandlerConfig;
    /// <summary>Static file handler</summary>
    private static readonly Lazy<IHttpHandler> _staticFileHandler = new Lazy<IHttpHandler>(new Func<IHttpHandler>(AssetHandlerBase.CreateStaticFileHandlerInstance));

    /// <summary>Gets a asset content type</summary>
    protected abstract string ContentType { get; }

    /// <summary>Gets a value indicating whether asset is static</summary>
    protected abstract bool IsStaticAsset { get; }

    /// <summary>
    /// Gets a value indicating whether another request can use the instance of HTTP handler
    /// </summary>
    public bool IsReusable => true;

    /// <summary>Constructs a instance of the debugging HTTP handler</summary>
    /// <param name="cache">Server cache</param>
    /// <param name="virtualFileSystemWrapper">Virtual file system wrapper</param>
    /// <param name="assetHandlerConfig">Configuration settings of the debugging HTTP handler</param>
    protected AssetHandlerBase(
      Cache cache,
      IVirtualFileSystemWrapper virtualFileSystemWrapper,
      AssetHandlerSettings assetHandlerConfig)
    {
      this._cache = cache;
      this._virtualFileSystemWrapper = virtualFileSystemWrapper;
      this._assetHandlerConfig = assetHandlerConfig;
    }

    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    public void ProcessRequest(HttpContextBase context)
    {
      this._context = context;
      HttpRequestBase request = context.Request;
      HttpResponseBase response = context.Response;
      Uri url = request.Url;
      string str = !(url == (Uri) null) ? url.LocalPath : throw new HttpException(500, BundleTransformer.Core.Resources.Strings.Common_ValueIsNull);
      if (string.IsNullOrWhiteSpace(str))
        throw new HttpException(500, BundleTransformer.Core.Resources.Strings.Common_ValueIsEmpty);
      if (!this._virtualFileSystemWrapper.FileExists(str))
        throw new HttpException(404, string.Format(BundleTransformer.Core.Resources.Strings.Common_FileNotExist, (object) str));
      string bundleVirtualPath = request.QueryString[Common.BundleVirtualPathQueryStringParameterName];
      if (string.IsNullOrWhiteSpace(bundleVirtualPath) && this.IsStaticAsset)
      {
        AssetHandlerBase.ProcessStaticAssetRequest(context);
      }
      else
      {
        string processedAssetContent;
        try
        {
          processedAssetContent = this.GetProcessedAssetContent(str, bundleVirtualPath);
        }
        catch (HttpException ex)
        {
          throw;
        }
        catch (AssetTranslationException ex)
        {
          throw new HttpException(500, ex.Message, (Exception) ex);
        }
        catch (AssetPostProcessingException ex)
        {
          throw new HttpException(500, ex.Message, (Exception) ex);
        }
        catch (FileNotFoundException ex)
        {
          throw new HttpException(500, string.Format(BundleTransformer.Core.Resources.Strings.AssetHandler_DependencyNotFound, (object) ex.Message, (object) ex));
        }
        catch (Exception ex)
        {
          throw new HttpException(500, string.Format(BundleTransformer.Core.Resources.Strings.AssetHandler_UnknownError, (object) ex.Message, (object) ex));
        }
        HttpCachePolicyBase cache = response.Cache;
        if (this._assetHandlerConfig.DisableClientCache)
        {
          response.StatusCode = 200;
          response.StatusDescription = "OK";
          cache.SetCacheability(HttpCacheability.NoCache);
          cache.SetExpires(DateTime.UtcNow.AddYears(-1));
          cache.SetValidUntilExpires(false);
          cache.SetNoStore();
          cache.SetNoServerCaching();
          response.ContentType = this.ContentType;
          response.Write(processedAssetContent);
        }
        else
        {
          string assetEtag = AssetHandlerBase.GenerateAssetETag(processedAssetContent);
          int num = AssetHandlerBase.IsETagHeaderChanged(request, assetEtag) ? 1 : 0;
          if (num != 0)
          {
            response.StatusCode = 200;
            response.StatusDescription = "OK";
          }
          else
          {
            response.StatusCode = 304;
            response.StatusDescription = "Not Modified";
            response.AddHeader("Content-Length", "0");
          }
          response.AddHeader("X-Asset-Transformation-Powered-By", "Bundle Transformer");
          cache.SetCacheability(HttpCacheability.Public);
          cache.SetExpires(DateTime.UtcNow.AddYears(-1));
          cache.SetValidUntilExpires(true);
          cache.AppendCacheExtension("must-revalidate");
          cache.SetNoServerCaching();
          cache.VaryByHeaders["If-None-Match"] = true;
          cache.SetETag(assetEtag);
          if (num != 0)
          {
            response.ContentType = this.ContentType;
            response.Write(processedAssetContent);
          }
        }
        context.ApplicationInstance.CompleteRequest();
      }
    }

    /// <summary>Process a request of static asset</summary>
    /// <param name="context">HTTP context</param>
    private static void ProcessStaticAssetRequest(HttpContextBase context) => AssetHandlerBase._staticFileHandler.Value.ProcessRequest(context.ApplicationInstance.Context);

    /// <summary>Creates a instance of static file handler</summary>
    /// <returns>Instance of static file handler</returns>
    private static IHttpHandler CreateStaticFileHandlerInstance()
    {
      Assembly assembly = typeof (HttpApplication).Assembly;
      return (IHttpHandler) Activator.CreateInstance(assembly.GetType("System.Web.StaticFileHandler", true), true) ?? throw new HttpException(500, string.Format(BundleTransformer.Core.Resources.Strings.Common_InstanceCreationFailed, (object) "System.Web.StaticFileHandler", (object) assembly.FullName));
    }

    /// <summary>Gets a cache key</summary>
    /// <param name="assetVirtualPath">Virtual path of asset</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <returns>Cache key for specified asset</returns>
    protected virtual string GetCacheKey(string assetVirtualPath, string bundleVirtualPath)
    {
      string str1 = !string.IsNullOrWhiteSpace(assetVirtualPath) ? UrlHelpers.ProcessBackSlashes(assetVirtualPath) : throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (assetVirtualPath)), nameof (assetVirtualPath));
      string str2 = string.Format(Common.ProcessedAssetContentCacheItemKeyPattern, (object) str1.ToLowerInvariant());
      if (!string.IsNullOrWhiteSpace(bundleVirtualPath))
      {
        string str3 = UrlHelpers.RemoveLastSlash(UrlHelpers.ProcessBackSlashes(bundleVirtualPath));
        str2 = str2 + "_" + str3.ToLowerInvariant();
      }
      return str2;
    }

    /// <summary>Gets a processed asset content</summary>
    /// <param name="assetVirtualPath">Virtual path of asset</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <returns>Text content of asset</returns>
    private string GetProcessedAssetContent(string assetVirtualPath, string bundleVirtualPath)
    {
      if (string.IsNullOrWhiteSpace(assetVirtualPath))
        throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (assetVirtualPath)), nameof (assetVirtualPath));
      string str;
      if (this._assetHandlerConfig.DisableServerCache)
      {
        str = this.ProcessAsset(assetVirtualPath, bundleVirtualPath).Content;
      }
      else
      {
        lock (AssetHandlerBase._cacheSynchronizer)
        {
          string cacheKey = this.GetCacheKey(assetVirtualPath, bundleVirtualPath);
          object obj = this._cache.Get(cacheKey);
          if (obj != null)
          {
            str = (string) obj;
          }
          else
          {
            IAsset asset = this.ProcessAsset(assetVirtualPath, bundleVirtualPath);
            str = asset.Content;
            DateTime utcNow = DateTime.UtcNow;
            DateTime absoluteExpiration = DateTime.Now.AddMinutes((double) this._assetHandlerConfig.ServerCacheDurationInMinutes);
            TimeSpan slidingExpiration = Cache.NoSlidingExpiration;
            List<string> stringList = new List<string>()
            {
              assetVirtualPath
            };
            stringList.AddRange((IEnumerable<string>) asset.VirtualPathDependencies);
            CacheDependency cacheDependency = this._virtualFileSystemWrapper.GetCacheDependency(assetVirtualPath, stringList.ToArray(), utcNow);
            this._cache.Insert(cacheKey, (object) str, cacheDependency, absoluteExpiration, slidingExpiration, CacheItemPriority.Low, (CacheItemRemovedCallback) null);
          }
        }
      }
      return str;
    }

    /// <summary>
    /// Generates a value for HTTP header "ETag" based on
    /// information about processed asset
    /// </summary>
    /// <param name="assetContent">Text content of asset</param>
    /// <returns>ETag value</returns>
    private static string GenerateAssetETag(string assetContent)
    {
      string str;
      using (SHA256 hashAlgorithm = AssetHandlerBase.CreateHashAlgorithm())
        str = HttpServerUtility.UrlTokenEncode(hashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(assetContent)));
      return string.Format("\"{0}\"", (object) str);
    }

    /// <summary>Creates a hash algorithm</summary>
    /// <returns>Hash algorithm</returns>
    private static SHA256 CreateHashAlgorithm() => Type.GetType("Mono.Runtime") != (Type) null ? (SHA256) new SHA256Managed() : (SHA256) new SHA256CryptoServiceProvider();

    /// <summary>
    /// Checks a actuality of data in browser cache using
    /// HTTP header "ETag"
    /// </summary>
    /// <param name="request">HttpRequest object</param>
    /// <param name="eTag">ETag value</param>
    /// <returns>Result of checking (true – data has changed; false – has not changed)</returns>
    private static bool IsETagHeaderChanged(HttpRequestBase request, string eTag)
    {
      bool flag = true;
      string header = request.Headers["If-None-Match"];
      if (!string.IsNullOrWhiteSpace(header))
        flag = header != eTag;
      return flag;
    }

    /// <summary>Process a asset</summary>
    /// <param name="assetVirtualPath">Virtual path of asset</param>
    /// <param name="bundleVirtualPath">Virtual path of bundle</param>
    /// <returns>Processed asset</returns>
    private IAsset ProcessAsset(string assetVirtualPath, string bundleVirtualPath)
    {
      BundleFile bundleFile = (BundleFile) null;
      ITransformer transformer = (ITransformer) null;
      if (!string.IsNullOrWhiteSpace(bundleVirtualPath))
      {
        Bundle bundleByVirtualPath = this.GetBundleByVirtualPath(bundleVirtualPath);
        bundleFile = bundleByVirtualPath != null ? this.GetBundleFileByVirtualPath(bundleByVirtualPath, assetVirtualPath) : throw new HttpException(500, string.Format(BundleTransformer.Core.Resources.Strings.AssetHandler_BundleNotFound, (object) bundleVirtualPath));
        if (bundleFile == null)
          throw new HttpException(500, string.Format(BundleTransformer.Core.Resources.Strings.AssetHandler_BundleFileNotFound, (object) assetVirtualPath, (object) bundleVirtualPath));
        transformer = this.GetTransformer(bundleByVirtualPath);
        if (transformer == null)
          throw new HttpException(500, string.Format(BundleTransformer.Core.Resources.Strings.AssetHandler_TransformerNotFound, (object) bundleVirtualPath));
      }
      IAsset asset = (IAsset) new Asset(assetVirtualPath, bundleFile);
      if (!this.IsStaticAsset)
        asset = this.TranslateAsset(asset, transformer, BundleTransformerContext.Current.IsDebugMode);
      if (transformer != null)
        asset = this.PostProcessAsset(asset, transformer);
      return asset;
    }

    /// <summary>Gets a bundle by virtual path</summary>
    /// <param name="virtualPath">Virtual path</param>
    /// <returns>Bundle</returns>
    protected virtual Bundle GetBundleByVirtualPath(string virtualPath) => BundleTable.Bundles.GetBundleFor(virtualPath);

    /// <summary>Gets a bundle file by virtual path</summary>
    /// <param name="bundle">Bundle</param>
    /// <param name="virtualPath">Virtual path</param>
    /// <returns>Bundle</returns>
    protected virtual BundleFile GetBundleFileByVirtualPath(
      Bundle bundle,
      string virtualPath)
    {
      BundleFile bundleFile = (BundleFile) null;
      string b = this.RemoveAdditionalFileExtension(UrlHelpers.ProcessBackSlashes(this._virtualFileSystemWrapper.ToAbsolutePath(virtualPath)));
      BundleContext context = new BundleContext(this._context, BundleTable.Bundles, bundle.Path);
      foreach (BundleFile enumerateFile in bundle.EnumerateFiles(context))
      {
        if (string.Equals(this.RemoveAdditionalFileExtension(UrlHelpers.ProcessBackSlashes(this._virtualFileSystemWrapper.ToAbsolutePath(enumerateFile.VirtualFile.VirtualPath))), b, StringComparison.OrdinalIgnoreCase))
        {
          bundleFile = enumerateFile;
          break;
        }
      }
      return bundleFile;
    }

    /// <summary>
    /// Removes a additional file extension from path of specified asset
    /// </summary>
    /// <param name="assetPath">Path of asset</param>
    /// <returns>Path of asset without additional file extension</returns>
    protected virtual string RemoveAdditionalFileExtension(string assetPath) => assetPath;

    /// <summary>Gets a transformer from bundle</summary>
    /// <param name="bundle">Bundle</param>
    /// <returns>Transformer</returns>
    protected abstract ITransformer GetTransformer(Bundle bundle);

    /// <summary>
    /// Translates a code of asset written on intermediate language
    /// </summary>
    /// <param name="asset">Asset</param>
    /// <param name="transformer">Transformer</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Translated asset</returns>
    protected virtual IAsset TranslateAsset(
      IAsset asset,
      ITransformer transformer,
      bool isDebugMode)
    {
      return asset;
    }

    /// <summary>Helper method to facilitate a translation of asset</summary>
    /// <typeparam name="T">Type of translator</typeparam>
    /// <param name="translatorName">Name of translator</param>
    /// <param name="asset">Asset</param>
    /// <param name="transformer">Transformer</param>
    /// <param name="isDebugMode">Flag that web application is in debug mode</param>
    /// <returns>Translated asset</returns>
    protected IAsset InnerTranslateAsset<T>(
      string translatorName,
      IAsset asset,
      ITransformer transformer,
      bool isDebugMode)
      where T : class, ITranslator
    {
      IAsset asset1 = asset;
      T obj;
      if (transformer != null)
      {
        obj = this.GetTranslatorByType<T>(transformer);
        if ((object) obj == null)
          throw new HttpException(500, string.Format(BundleTransformer.Core.Resources.Strings.AssetHandler_TranslatorNotFound, (object) typeof (T).FullName, (object) asset.Url));
      }
      else
        obj = this.GetTranslatorByName<T>(translatorName);
      if ((object) obj != null)
      {
        asset1 = obj.Translate(asset1);
        obj.IsDebugMode = isDebugMode;
      }
      return asset1;
    }

    /// <summary>Gets a translator by name</summary>
    /// <typeparam name="T">Type of translator</typeparam>
    /// <param name="translatorName">Name of translator</param>
    /// <returns>Translator</returns>
    protected abstract T GetTranslatorByName<T>(string translatorName) where T : class, ITranslator;

    /// <summary>Gets a translator by type from transformer</summary>
    /// <typeparam name="T">Type of translator</typeparam>
    /// <param name="transformer">Transformer</param>
    /// <returns>Translator</returns>
    protected T GetTranslatorByType<T>(ITransformer transformer) where T : class, ITranslator => (T) transformer.Translators.FirstOrDefault<ITranslator>((Func<ITranslator, bool>) (t => t is T));

    /// <summary>Postprocess a text content of asset</summary>
    /// <param name="asset">Asset</param>
    /// <param name="transformer">Transformer</param>
    /// <returns>Postprocessed asset</returns>
    protected virtual IAsset PostProcessAsset(IAsset asset, ITransformer transformer)
    {
      foreach (IPostProcessor postProcessor in (IEnumerable<IPostProcessor>) transformer.PostProcessors.Where<IPostProcessor>((Func<IPostProcessor, bool>) (p => p.UseInDebugMode)).ToList<IPostProcessor>())
        postProcessor.PostProcess(asset);
      return asset;
    }
  }
}
