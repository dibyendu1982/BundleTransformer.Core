// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.AssetContextBase
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Configuration;
using BundleTransformer.Core.Minifiers;
using BundleTransformer.Core.PostProcessors;
using BundleTransformer.Core.Resources;
using BundleTransformer.Core.Translators;
using BundleTransformer.Core.Utilities;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Optimization;

namespace BundleTransformer.Core.Assets
{
  public abstract class AssetContextBase : IAssetContext
  {
    /// <summary>Configuration settings of processing assets</summary>
    protected AssetSettingsBase _assetConfig;
    /// <summary>Pool of translators</summary>
    protected readonly Dictionary<string, ITranslator> _translatorsPool;
    /// <summary>Synchronizer of translators pool</summary>
    protected readonly object _translatorsPoolSynchronizer;
    /// <summary>Pool of postprocessors</summary>
    protected readonly Dictionary<string, IPostProcessor> _postProcessorsPool;
    /// <summary>Synchronizer of postprocessors pool</summary>
    protected readonly object _postProcessorsPoolSynchronizer;
    /// <summary>Pool of minifiers</summary>
    protected readonly Dictionary<string, IMinifier> _minifiersPool;
    /// <summary>Synchronizer of minifiers pool</summary>
    protected readonly object _minifiersPoolSynchronizer;
    /// <summary>File extension mappings</summary>
    protected readonly FileExtensionMappingCollection _fileExtensionMappings;

    /// <summary>Gets a output code type</summary>
    protected abstract string OutputCodeType { get; }

    /// <summary>Gets a file extension mappings</summary>
    public FileExtensionMappingCollection FileExtensionMappings => this._fileExtensionMappings;

    /// <summary>Constructs a instance of script context</summary>
    /// <param name="assetConfig">Configuration settings of processing assets</param>
    protected AssetContextBase(AssetSettingsBase assetConfig)
    {
      this._assetConfig = assetConfig;
      this._translatorsPool = new Dictionary<string, ITranslator>();
      this._translatorsPoolSynchronizer = new object();
      this._postProcessorsPool = new Dictionary<string, IPostProcessor>();
      this._postProcessorsPoolSynchronizer = new object();
      this._minifiersPool = new Dictionary<string, IMinifier>();
      this._minifiersPoolSynchronizer = new object();
      this._fileExtensionMappings = this.GetFileExtensionMappings();
    }

    /// <summary>Gets a file extension mappings</summary>
    /// <returns></returns>
    private FileExtensionMappingCollection GetFileExtensionMappings()
    {
      FileExtensionMappingCollection mappingCollection = new FileExtensionMappingCollection();
      foreach (FileExtensionRegistration fileExtension in (ConfigurationElementCollection) this._assetConfig.FileExtensions)
        mappingCollection.Add(fileExtension.FileExtension, fileExtension.AssetTypeCode);
      return mappingCollection;
    }

    /// <summary>Gets a instance of default transform</summary>
    /// <returns>Instance of transformer</returns>
    public abstract IBundleTransform GetDefaultTransformInstance();

    /// <summary>Gets a instance of translator</summary>
    /// <param name="translatorName">Translator name</param>
    /// <returns>Instance of translator</returns>
    public ITranslator GetTranslatorInstance(string translatorName)
    {
      ITranslator translator;
      lock (this._translatorsPoolSynchronizer)
      {
        if (this._translatorsPool.ContainsKey(translatorName))
        {
          translator = this._translatorsPool[translatorName];
        }
        else
        {
          if (translatorName == "NullTranslator")
            translator = (ITranslator) new NullTranslator();
          else
            translator = Utils.CreateInstanceByFullTypeName<ITranslator>((this._assetConfig.Translators[translatorName] ?? throw new TranslatorNotFoundException(string.Format(Strings.Configuration_TranslatorNotRegistered, (object) this.OutputCodeType, (object) translatorName))).Type);
          this._translatorsPool.Add(translatorName, translator);
        }
      }
      return translator;
    }

    /// <summary>Gets a instance of postprocessor</summary>
    /// <param name="postProcessorName">Postprocessor name</param>
    /// <returns>Instance of postprocessor</returns>
    public IPostProcessor GetPostProcessorInstance(string postProcessorName)
    {
      IPostProcessor postProcessor1;
      lock (this._postProcessorsPoolSynchronizer)
      {
        if (this._postProcessorsPool.ContainsKey(postProcessorName))
        {
          postProcessor1 = this._postProcessorsPool[postProcessorName];
        }
        else
        {
          if (postProcessorName == "NullPostProcessor")
          {
            postProcessor1 = (IPostProcessor) new NullPostProcessor();
          }
          else
          {
            PostProcessorRegistration postProcessor2 = this._assetConfig.PostProcessors[postProcessorName];
            postProcessor1 = postProcessor2 != null ? Utils.CreateInstanceByFullTypeName<IPostProcessor>(postProcessor2.Type) : throw new PostProcessorNotFoundException(string.Format(Strings.Configuration_PostProcessorNotRegistered, (object) this.OutputCodeType, (object) postProcessorName));
            postProcessor1.UseInDebugMode = postProcessor2.UseInDebugMode;
          }
          this._postProcessorsPool.Add(postProcessorName, postProcessor1);
        }
      }
      return postProcessor1;
    }

    /// <summary>Gets a instance of minifier</summary>
    /// <param name="minifierName">Minifier name</param>
    /// <returns>Instance of minifier</returns>
    public IMinifier GetMinifierInstance(string minifierName)
    {
      IMinifier minifier;
      lock (this._minifiersPoolSynchronizer)
      {
        if (this._minifiersPool.ContainsKey(minifierName))
        {
          minifier = this._minifiersPool[minifierName];
        }
        else
        {
          if (minifierName == "NullMinifier")
            minifier = (IMinifier) new NullMinifier();
          else
            minifier = Utils.CreateInstanceByFullTypeName<IMinifier>((this._assetConfig.Minifiers[minifierName] ?? throw new MinifierNotFoundException(string.Format(Strings.Configuration_MinifierNotRegistered, (object) this.OutputCodeType, (object) minifierName))).Type);
          this._minifiersPool.Add(minifierName, minifier);
        }
      }
      return minifier;
    }

    /// <summary>Gets a list of default translator instances</summary>
    /// <returns>List of default translator instances</returns>
    public IList<ITranslator> GetDefaultTranslatorInstances()
    {
      List<ITranslator> translatorList = new List<ITranslator>();
      foreach (TranslatorRegistration translator in (ConfigurationElementCollection) this._assetConfig.Translators)
      {
        if (translator.Enabled)
        {
          ITranslator translatorInstance = this.GetTranslatorInstance(translator.Name);
          translatorList.Add(translatorInstance);
        }
      }
      return (IList<ITranslator>) translatorList;
    }

    /// <summary>Gets a list of default postprocessor instances</summary>
    /// <returns>List of default postprocessor instances</returns>
    public IList<IPostProcessor> GetDefaultPostProcessorInstances()
    {
      List<IPostProcessor> postProcessorList = new List<IPostProcessor>();
      foreach (string postProcessorName in Utils.ConvertToStringCollection(this._assetConfig.DefaultPostProcessors, ',', true, true))
      {
        IPostProcessor processorInstance = this.GetPostProcessorInstance(postProcessorName);
        postProcessorList.Add(processorInstance);
      }
      return (IList<IPostProcessor>) postProcessorList;
    }

    /// <summary>Gets a instance of default minifier</summary>
    /// <returns>Instance of default minifier</returns>
    public IMinifier GetDefaultMinifierInstance()
    {
      string defaultMinifier = this._assetConfig.DefaultMinifier;
      return !string.IsNullOrWhiteSpace(defaultMinifier) ? this.GetMinifierInstance(defaultMinifier) : throw new ConfigurationErrorsException(string.Format(Strings.Configuration_DefaultMinifierNotSpecified, (object) this.OutputCodeType));
    }
  }
}
