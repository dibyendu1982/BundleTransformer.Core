// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Assets.FileExtensionMappingCollection
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BundleTransformer.Core.Assets
{
  /// <summary>Collection of file extension mappings</summary>
  public sealed class FileExtensionMappingCollection : IEnumerable<FileExtensionMapping>, IEnumerable
  {
    /// <summary>Internal collection of file extension mappings</summary>
    private readonly Dictionary<string, string> _entries = new Dictionary<string, string>();

    /// <summary>
    /// Gets or sets a asset type code associated with the specified file extension
    /// </summary>
    /// <param name="fileExtension">File extension</param>
    /// <returns>Asset type code</returns>
    public string this[string fileExtension]
    {
      get => !string.IsNullOrWhiteSpace(fileExtension) ? this._entries[FileExtensionMappingCollection.ProcessFileExtension(fileExtension)] : throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (fileExtension)), nameof (fileExtension));
      set
      {
        if (string.IsNullOrWhiteSpace(fileExtension))
          throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (fileExtension)), nameof (fileExtension));
        if (string.IsNullOrWhiteSpace(value))
          throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (value)), nameof (value));
        string key = FileExtensionMappingCollection.ProcessFileExtension(fileExtension);
        string str = FileExtensionMappingCollection.ProcessAssetTypeCode(value);
        if (this._entries.ContainsKey(key))
          throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.FileExtensionMapping_DuplicateFileExtension, (object) key), nameof (fileExtension));
        this._entries[key] = str;
      }
    }

    /// <summary>Gets a collection containing the file extensions</summary>
    public ICollection<string> FileExtensions => (ICollection<string>) this._entries.Keys;

    /// <summary>Gets a collection containing the asset type codes</summary>
    public ICollection<string> AssetTypeCodes => (ICollection<string>) this._entries.Values;

    /// <summary>Gets a number of mappings contained in the collection</summary>
    public int Count => this._entries.Count;

    public IEnumerator<FileExtensionMapping> GetEnumerator()
    {
      foreach (KeyValuePair<string, string> entry in this._entries)
        yield return new FileExtensionMapping(entry.Key, entry.Value);
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>
    /// Determines whether a collections contains the specified file extension
    /// </summary>
    /// <param name="fileExtension">File extension</param>
    /// <returns>Result of check (true - contains; false - not contains)</returns>
    public bool ContainsFileExtension(string fileExtension) => !string.IsNullOrWhiteSpace(fileExtension) ? this.InnerContainsFileExtension(fileExtension) : throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (fileExtension)), nameof (fileExtension));

    /// <summary>
    /// Determines whether a collections contains the specified asset type code
    /// </summary>
    /// <param name="assetTypeCode">Asset type code</param>
    /// <returns>Result of check (true - contains; false - not contains)</returns>
    public bool ContainsAssetTypeCode(string assetTypeCode) => !string.IsNullOrWhiteSpace(assetTypeCode) ? this._entries.ContainsValue(assetTypeCode) : throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (assetTypeCode)), nameof (assetTypeCode));

    /// <summary>
    /// Determines whether a collections contains the specified file extension mapping
    /// </summary>
    /// <param name="mapping">File extension mapping</param>
    /// <returns>Result of check (true - contains; false - not contains)</returns>
    public bool Contains(FileExtensionMapping mapping)
    {
      if (mapping == null)
        throw new ArgumentNullException(nameof (mapping), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (mapping)));
      return !string.IsNullOrWhiteSpace(mapping.FileExtension) ? this.InnerContainsFileExtension(mapping.FileExtension) : throw new EmptyValueException(BundleTransformer.Core.Resources.Strings.Common_ValueIsEmpty);
    }

    private bool InnerContainsFileExtension(string fileExtension) => this._entries.ContainsKey(FileExtensionMappingCollection.ProcessFileExtension(fileExtension));

    /// <summary>Gets a asset type code by file path</summary>
    /// <param name="filePath">File path</param>
    /// <returns>Asset type code</returns>
    public string GetAssetTypeCodeByFilePath(string filePath)
    {
      string str1 = !string.IsNullOrWhiteSpace(filePath) ? filePath.TrimEnd().ToLowerInvariant() : throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (filePath)), nameof (filePath));
      int num = str1.Count<char>((Func<char, bool>) (c => c == '.'));
      if (num == 0)
        return AssetTypeCode.Unknown;
      string str2 = AssetTypeCode.Unknown;
      if (num == 1)
      {
        string extension = Path.GetExtension(str1);
        if (this._entries.ContainsKey(extension))
          str2 = this._entries[extension];
      }
      else
      {
        string str3 = string.Empty;
        foreach (string key in (IEnumerable<string>) this._entries.Keys)
        {
          if (str1.EndsWith(key))
          {
            str3 = key;
            break;
          }
        }
        if (str3.Length > 0 && this._entries.ContainsKey(str3))
          str2 = this[str3];
      }
      return str2;
    }

    /// <summary>
    /// Adds a specified file extension and asset type code to the collection
    /// </summary>
    /// <param name="fileExtension">File extension</param>
    /// <param name="assetTypeCode">Asset type code</param>
    public void Add(string fileExtension, string assetTypeCode)
    {
      if (string.IsNullOrWhiteSpace(fileExtension))
        throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (fileExtension)), nameof (fileExtension));
      if (string.IsNullOrWhiteSpace(assetTypeCode))
        throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (assetTypeCode)), nameof (assetTypeCode));
      this.InnerAdd(fileExtension, assetTypeCode);
    }

    /// <summary>
    /// Adds a specified file extension mapping to the collection
    /// </summary>
    /// <param name="mapping">File extension mapping</param>
    public void Add(FileExtensionMapping mapping)
    {
      if (mapping == null)
        throw new ArgumentNullException(nameof (mapping), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (mapping)));
      if (string.IsNullOrWhiteSpace(mapping.FileExtension))
        throw new EmptyValueException(BundleTransformer.Core.Resources.Strings.Common_ValueIsEmpty);
      if (string.IsNullOrWhiteSpace(mapping.AssetTypeCode))
        throw new EmptyValueException(BundleTransformer.Core.Resources.Strings.Common_ValueIsEmpty);
      this.InnerAdd(mapping.FileExtension, mapping.AssetTypeCode);
    }

    private void InnerAdd(string fileExtension, string assetTypeCode)
    {
      string key = FileExtensionMappingCollection.ProcessFileExtension(fileExtension);
      string str = FileExtensionMappingCollection.ProcessAssetTypeCode(assetTypeCode);
      if (this._entries.ContainsKey(key))
        throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.FileExtensionMapping_DuplicateFileExtension, (object) key), nameof (fileExtension));
      this._entries.Add(key, str);
    }

    /// <summary>
    /// Removes a mapping with the specified file extension from the collection
    /// </summary>
    /// <param name="fileExtension">File extension</param>
    /// <returns>Result of operation (true - mapping is successfully found and removed;
    /// false - otherwise)</returns>
    public bool Remove(string fileExtension) => !string.IsNullOrWhiteSpace(fileExtension) ? this.InnerRemove(fileExtension) : throw new ArgumentException(string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsEmpty, (object) nameof (fileExtension)), nameof (fileExtension));

    /// <summary>Removes a mapping from the collection</summary>
    /// <param name="mapping">File extension mapping</param>
    /// <returns>Result of operation (true - mapping is successfully found and removed;
    /// false - otherwise)</returns>
    public bool Remove(FileExtensionMapping mapping)
    {
      if (mapping == null)
        throw new ArgumentNullException(nameof (mapping), string.Format(BundleTransformer.Core.Resources.Strings.Common_ArgumentIsNull, (object) nameof (mapping)));
      return !string.IsNullOrWhiteSpace(mapping.FileExtension) ? this.InnerRemove(mapping.FileExtension) : throw new EmptyValueException(BundleTransformer.Core.Resources.Strings.Common_ValueIsEmpty);
    }

    private bool InnerRemove(string fileExtension) => this._entries.Remove(FileExtensionMappingCollection.ProcessFileExtension(fileExtension));

    /// <summary>
    /// Removes all file extension mappings from the collection
    /// </summary>
    public void Clear() => this._entries.Clear();

    /// <summary>Process a file extension</summary>
    /// <param name="fileExtension">File extension</param>
    /// <returns>Processed file extension</returns>
    private static string ProcessFileExtension(string fileExtension) => fileExtension.Trim().ToLowerInvariant();

    /// <summary>Process a asset type code</summary>
    /// <param name="assetTypeCode">Asset type code</param>
    /// <returns>Processed asset type code</returns>
    private static string ProcessAssetTypeCode(string assetTypeCode) => assetTypeCode.Trim();
  }
}
