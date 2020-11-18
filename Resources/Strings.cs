// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Resources.Strings
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace BundleTransformer.Core.Resources
{
  /// <summary>
  ///   A strongly-typed resource class, for looking up localized strings, etc.
  /// </summary>
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  public class Strings
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Strings()
    {
    }

    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
      get
      {
        if (Strings.resourceMan == null)
          Strings.resourceMan = new ResourceManager("BundleTransformer.Core.Resources.Strings", typeof (Strings).Assembly);
        return Strings.resourceMan;
      }
    }

    /// <summary>
    ///   Overrides the current thread's CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get => Strings.resourceCulture;
      set => Strings.resourceCulture = value;
    }

    /// <summary>
    ///   Looks up a localized string similar to Could not find the '{0}' file in the '{1}' bundle..
    /// </summary>
    public static string AssetHandler_BundleFileNotFound => Strings.ResourceManager.GetString(nameof (AssetHandler_BundleFileNotFound), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Bundle '{0}' not exist..
    /// </summary>
    public static string AssetHandler_BundleNotFound => Strings.ResourceManager.GetString(nameof (AssetHandler_BundleNotFound), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During the output text content of processed asset is not found one of its dependencies.
    /// See more details:
    /// {0}.
    ///  </summary>
    public static string AssetHandler_DependencyNotFound => Strings.ResourceManager.GetString(nameof (AssetHandler_DependencyNotFound), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Could not find the transformer in the '{0}' bundle..
    /// </summary>
    public static string AssetHandler_TransformerNotFound => Strings.ResourceManager.GetString(nameof (AssetHandler_TransformerNotFound), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Could not find the translator of `{0}` type for the '{1}' asset..
    /// </summary>
    public static string AssetHandler_TranslatorNotFound => Strings.ResourceManager.GetString(nameof (AssetHandler_TranslatorNotFound), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During the output text content of processed asset an unknown error has occurred.
    /// See more details:
    /// {0}.
    ///  </summary>
    public static string AssetHandler_UnknownError => Strings.ResourceManager.GetString(nameof (AssetHandler_UnknownError), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Pure wildcard ignore patterns `*` and `*.*` are not supported..
    /// </summary>
    public static string Assets_InvalidIgnorePattern => Strings.ResourceManager.GetString(nameof (Assets_InvalidIgnorePattern), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to These assets are not scripts: {0}..
    /// </summary>
    public static string Assets_ScriptAssetsContainAssetsWithInvalidTypes => Strings.ResourceManager.GetString(nameof (Assets_ScriptAssetsContainAssetsWithInvalidTypes), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to These assets are not style sheets: {0}..
    /// </summary>
    public static string Assets_StyleAssetsContainAssetsWithInvalidTypes => Strings.ResourceManager.GetString(nameof (Assets_StyleAssetsContainAssetsWithInvalidTypes), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to The parameter `{0}` must be a non-empty string..
    /// </summary>
    public static string Common_ArgumentIsEmpty => Strings.ResourceManager.GetString(nameof (Common_ArgumentIsEmpty), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to The parameter `{0}` must be a non-nullable..
    /// </summary>
    public static string Common_ArgumentIsNull => Strings.ResourceManager.GetString(nameof (Common_ArgumentIsNull), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to You do not specified a name of assembly..
    /// </summary>
    public static string Common_AssemblyNameIsEmpty => Strings.ResourceManager.GetString(nameof (Common_AssemblyNameIsEmpty), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Can not convert value '{0}' of enumeration type `{1}` to value of enumeration type `{2}`..
    /// </summary>
    public static string Common_EnumValueConversionFailed => Strings.ResourceManager.GetString(nameof (Common_EnumValueConversionFailed), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Can't find string code that corresponding to the value '{0}' of enumeration type `{1}`..
    /// </summary>
    public static string Common_EnumValueToCodeConversionFailed => Strings.ResourceManager.GetString(nameof (Common_EnumValueToCodeConversionFailed), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to File '{0}' not exist..
    /// </summary>
    public static string Common_FileNotExist => Strings.ResourceManager.GetString(nameof (Common_FileNotExist), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to During instantiate an object of type `{0}` from assembly `{1}` error occurred..
    /// </summary>
    public static string Common_InstanceCreationFailed => Strings.ResourceManager.GetString(nameof (Common_InstanceCreationFailed), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Can't find a value of enumeration type `{0}` that corresponding to the severity level {1}..
    /// </summary>
    public static string Common_SeverityLevelToEnumValueConversionFailed => Strings.ResourceManager.GetString(nameof (Common_SeverityLevelToEnumValueConversionFailed), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to You do not specified a type name..
    /// </summary>
    public static string Common_TypeNameIsEmpty => Strings.ResourceManager.GetString(nameof (Common_TypeNameIsEmpty), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Unknown error..
    /// </summary>
    public static string Common_UnknownError => Strings.ResourceManager.GetString(nameof (Common_UnknownError), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Failed to determine MIME type of the file '{0}'..
    /// </summary>
    public static string Common_UnknownMimeType => Strings.ResourceManager.GetString(nameof (Common_UnknownMimeType), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Value cannot be empty..
    /// </summary>
    public static string Common_ValueIsEmpty => Strings.ResourceManager.GetString(nameof (Common_ValueIsEmpty), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Value cannot be null..
    /// </summary>
    public static string Common_ValueIsNull => Strings.ResourceManager.GetString(nameof (Common_ValueIsNull), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Default {0} minifier not specified..
    /// </summary>
    public static string Configuration_DefaultMinifierNotSpecified => Strings.ResourceManager.GetString(nameof (Configuration_DefaultMinifierNotSpecified), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to In the `name` attribute of `/configuration/bundleTransformer/{0}/jsEngine` configuration element not specified a name of JS engine.
    /// 
    /// If you have not installed JS engine, then for correct working of this module is recommended to install one of the following NuGet packages: {1}
    /// 
    /// After package is installed and JS engine is registered (https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/Registration-of-JS-engines), need set a name of JS engine (for example, `{2}`) to the `name` attribute of `/configur [rest of string was truncated]";.
    ///  </summary>
    public static string Configuration_JsEngineNotSpecified => Strings.ResourceManager.GetString(nameof (Configuration_JsEngineNotSpecified), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to {0} minifier with name `{1}` is not registered in configuration file..
    /// </summary>
    public static string Configuration_MinifierNotRegistered => Strings.ResourceManager.GetString(nameof (Configuration_MinifierNotRegistered), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to {0} postprocessor with name `{1}` is not registered in configuration file..
    /// </summary>
    public static string Configuration_PostProcessorNotRegistered => Strings.ResourceManager.GetString(nameof (Configuration_PostProcessorNotRegistered), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Translator, which compiles the code on intermediate language to {0}, and has the name `{1}` is not registered in  configuration file..
    /// </summary>
    public static string Configuration_TranslatorNotRegistered => Strings.ResourceManager.GetString(nameof (Configuration_TranslatorNotRegistered), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Column number.
    /// </summary>
    public static string ErrorDetails_ColumnNumber => Strings.ResourceManager.GetString(nameof (ErrorDetails_ColumnNumber), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to End column.</summary>
    public static string ErrorDetails_EndColumn => Strings.ResourceManager.GetString(nameof (ErrorDetails_EndColumn), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to End line.</summary>
    public static string ErrorDetails_EndLine => Strings.ResourceManager.GetString(nameof (ErrorDetails_EndLine), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Error code.</summary>
    public static string ErrorDetails_ErrorCode => Strings.ResourceManager.GetString(nameof (ErrorDetails_ErrorCode), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Error type.</summary>
    public static string ErrorDetails_ErrorType => Strings.ResourceManager.GetString(nameof (ErrorDetails_ErrorType), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to File.</summary>
    public static string ErrorDetails_File => Strings.ResourceManager.GetString(nameof (ErrorDetails_File), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Help keyword.
    /// </summary>
    public static string ErrorDetails_HelpKeyword => Strings.ResourceManager.GetString(nameof (ErrorDetails_HelpKeyword), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Line number.</summary>
    public static string ErrorDetails_LineNumber => Strings.ResourceManager.GetString(nameof (ErrorDetails_LineNumber), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Line source.</summary>
    public static string ErrorDetails_LineSource => Strings.ResourceManager.GetString(nameof (ErrorDetails_LineSource), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Message.</summary>
    public static string ErrorDetails_Message => Strings.ResourceManager.GetString(nameof (ErrorDetails_Message), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Name.</summary>
    public static string ErrorDetails_Name => Strings.ResourceManager.GetString(nameof (ErrorDetails_Name), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Script stack trace.
    /// </summary>
    public static string ErrorDetails_ScriptStackTrace => Strings.ResourceManager.GetString(nameof (ErrorDetails_ScriptStackTrace), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Severity.</summary>
    public static string ErrorDetails_Severity => Strings.ResourceManager.GetString(nameof (ErrorDetails_Severity), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Source error.
    /// </summary>
    public static string ErrorDetails_SourceError => Strings.ResourceManager.GetString(nameof (ErrorDetails_SourceError), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Source name.</summary>
    public static string ErrorDetails_SourceName => Strings.ResourceManager.GetString(nameof (ErrorDetails_SourceName), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Start column.
    /// </summary>
    public static string ErrorDetails_StartColumn => Strings.ResourceManager.GetString(nameof (ErrorDetails_StartColumn), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Start line.</summary>
    public static string ErrorDetails_StartLine => Strings.ResourceManager.GetString(nameof (ErrorDetails_StartLine), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to Subcategory.</summary>
    public static string ErrorDetails_Subcategory => Strings.ResourceManager.GetString(nameof (ErrorDetails_Subcategory), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to error.</summary>
    public static string ErrorType_Error => Strings.ResourceManager.GetString(nameof (ErrorType_Error), Strings.resourceCulture);

    /// <summary>Looks up a localized string similar to warning.</summary>
    public static string ErrorType_Warning => Strings.ResourceManager.GetString(nameof (ErrorType_Warning), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to File extension '{0}' has already been added to mapping collection..
    /// </summary>
    public static string FileExtensionMapping_DuplicateFileExtension => Strings.ResourceManager.GetString(nameof (FileExtensionMapping_DuplicateFileExtension), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During minification of {0} code, readed from the file '{1}', by {2} error has occurred.
    /// See more details:
    /// 
    /// {3}.
    ///  </summary>
    public static string Minifiers_MinificationFailed => Strings.ResourceManager.GetString(nameof (Minifiers_MinificationFailed), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During minification of {0} code, readed from the file '{1}', by {2} syntax error has occurred.
    /// See more details:
    /// 
    /// {3}.
    ///  </summary>
    public static string Minifiers_MinificationSyntaxError => Strings.ResourceManager.GetString(nameof (Minifiers_MinificationSyntaxError), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During postprocessing of {0} code, readed from the file '{1}', by {2} error has occurred.
    /// See more details:
    /// 
    /// {3}.
    ///  </summary>
    public static string PostProcessors_PostprocessingFailed => Strings.ResourceManager.GetString(nameof (PostProcessors_PostprocessingFailed), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During postprocessing of {0} code, readed from the file '{1}', by {2} syntax error has occurred.
    /// See more details:
    /// 
    /// {3}.
    ///  </summary>
    public static string PostProcessors_PostprocessingSyntaxError => Strings.ResourceManager.GetString(nameof (PostProcessors_PostprocessingSyntaxError), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to Resource with name '{0}' is null..
    /// </summary>
    public static string Resources_ResourceIsNull => Strings.ResourceManager.GetString(nameof (Resources_ResourceIsNull), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During translation of {0} code, readed from the file '{2}', to {1} code error has occurred.
    /// See more details:
    /// 
    /// {3}.
    ///  </summary>
    public static string Translators_TranslationFailed => Strings.ResourceManager.GetString(nameof (Translators_TranslationFailed), Strings.resourceCulture);

    /// <summary>
    ///    Looks up a localized string similar to During translation of {0} code, readed from the file '{2}', to {1} code syntax error has occurred.
    /// See more details:
    /// 
    /// {3}.
    ///  </summary>
    public static string Translators_TranslationSyntaxError => Strings.ResourceManager.GetString(nameof (Translators_TranslationSyntaxError), Strings.resourceCulture);

    /// <summary>
    ///   Looks up a localized string similar to The URL '{0}' is not valid. Only application relative URLs (~/url) are allowed..
    /// </summary>
    public static string UrlMappings_OnlyAppRelativeUrlAllowed => Strings.ResourceManager.GetString(nameof (UrlMappings_OnlyAppRelativeUrlAllowed), Strings.resourceCulture);
  }
}
