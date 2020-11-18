// Decompiled with JetBrains decompiler
// Type: BundleTransformer.Core.Utilities.Utils
// Assembly: BundleTransformer.Core, Version=1.10.0.0, Culture=neutral, PublicKeyToken=973c344c93aac60d
// MVID: EF37069B-667D-4804-94D3-E51E56666BCA
// Assembly location: C:\Users\dstiwary\source\repos\MvcSass\SassExperiment\bin\app.publish\bin\BundleTransformer.Core.dll

using BundleTransformer.Core.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BundleTransformer.Core.Utilities
{
  public static class Utils
  {
    /// <summary>Converts a string value to string collection</summary>
    /// <param name="value">String value</param>
    /// <param name="separator">Separator</param>
    /// <param name="trimItemValues">Allow trim of item values</param>
    /// <param name="removeEmptyItems">Allow removal of empty items from collection</param>
    /// <returns>String collection</returns>
    public static string[] ConvertToStringCollection(
      string value,
      char separator,
      bool trimItemValues = false,
      bool removeEmptyItems = false)
    {
      return Utils.ConvertToStringCollection(value, new char[1]
      {
        separator
      }, (trimItemValues ? 1 : 0) != 0, (removeEmptyItems ? 1 : 0) != 0);
    }

    /// <summary>Converts a string value to string collection</summary>
    /// <param name="value">String value</param>
    /// <param name="separator">Separator</param>
    /// <param name="trimItemValues">Allow trim of item values</param>
    /// <param name="removeEmptyItems">Allow removal of empty items from collection</param>
    /// <returns>String collection</returns>
    public static string[] ConvertToStringCollection(
      string value,
      char[] separator,
      bool trimItemValues = false,
      bool removeEmptyItems = false)
    {
      List<string> stringList = new List<string>();
      if (!string.IsNullOrWhiteSpace(value))
      {
        string[] strArray = value.Split(separator);
        int length = strArray.Length;
        for (int index = 0; index < length; ++index)
        {
          string str = strArray[index];
          if (trimItemValues)
            str = str.Trim();
          if (str.Length > 0 || !removeEmptyItems)
            stringList.Add(str);
        }
      }
      return stringList.ToArray();
    }

    /// <summary>Creates a instance by specified full type name</summary>
    /// <param name="fullTypeName">Full type name</param>
    /// <typeparam name="T">Target type</typeparam>
    /// <returns>Instance of type</returns>
    internal static T CreateInstanceByFullTypeName<T>(string fullTypeName) where T : class
    {
      int length = !string.IsNullOrWhiteSpace(fullTypeName) ? fullTypeName.IndexOf(',') : throw new ArgumentException(string.Format(Strings.Common_ArgumentIsEmpty, (object) nameof (fullTypeName)), nameof (fullTypeName));
      string typeName;
      string assemblyString;
      Assembly assembly;
      if (length != -1)
      {
        typeName = fullTypeName.Substring(0, length).Trim();
        if (string.IsNullOrEmpty(typeName))
          throw new EmptyValueException(Strings.Common_TypeNameIsEmpty);
        assemblyString = fullTypeName.Substring(length + 1, fullTypeName.Length - (length + 1)).Trim();
        assembly = !string.IsNullOrEmpty(assemblyString) ? Assembly.Load(assemblyString) : throw new EmptyValueException(Strings.Common_AssemblyNameIsEmpty);
      }
      else
      {
        typeName = fullTypeName;
        assembly = typeof (Utils).Assembly;
        assemblyString = assembly.FullName;
      }
      return (T) (assembly.CreateInstance(typeName) ?? throw new NullReferenceException(string.Format(Strings.Common_InstanceCreationFailed, (object) typeName, (object) assemblyString)));
    }

    /// <summary>
    /// Converts a value of source enumeration type to value of destination enumeration type
    /// </summary>
    /// <typeparam name="TSource">Source enumeration type</typeparam>
    /// <typeparam name="TDest">Destination enumeration type</typeparam>
    /// <param name="value">Value of source enumeration type</param>
    /// <returns>Value of destination enumeration type</returns>
    public static TDest GetEnumFromOtherEnum<TSource, TDest>(TSource value)
    {
      string b = value.ToString();
      foreach (TDest dest in (TDest[]) Enum.GetValues(typeof (TDest)))
      {
        if (string.Equals(dest.ToString(), b, StringComparison.OrdinalIgnoreCase))
          return dest;
      }
      throw new InvalidCastException(string.Format(Strings.Common_EnumValueConversionFailed, (object) b, (object) typeof (TSource), (object) typeof (TDest)));
    }

    /// <summary>Gets a content of the embedded resource as string</summary>
    /// <param name="resourceName">The case-sensitive resource name without the namespace of the specified type</param>
    /// <param name="type">The type, that determines the assembly and whose namespace is used to scope
    /// the resource name</param>
    /// <returns>Сontent of the embedded resource as string</returns>
    public static string GetResourceAsString(string resourceName, Type type)
    {
      if (resourceName == null)
        throw new ArgumentNullException(nameof (resourceName), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (resourceName)));
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (type)));
      if (string.IsNullOrWhiteSpace(resourceName))
        throw new ArgumentException(string.Format(Strings.Common_ArgumentIsEmpty, (object) nameof (resourceName)), nameof (resourceName));
      Assembly assembly = type.Assembly;
      string str = type.Namespace;
      return Utils.InnerGetResourceAsString(str != null ? str + "." + resourceName : resourceName, assembly);
    }

    /// <summary>Gets a content of the embedded resource as string</summary>
    /// <param name="resourceName">The case-sensitive resource name</param>
    /// <param name="assembly">The assembly, which contains the embedded resource</param>
    /// <returns>Сontent of the embedded resource as string</returns>
    public static string GetResourceAsString(string resourceName, Assembly assembly)
    {
      if (resourceName == null)
        throw new ArgumentNullException(nameof (resourceName), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (resourceName)));
      if (assembly == (Assembly) null)
        throw new ArgumentNullException(nameof (assembly), string.Format(Strings.Common_ArgumentIsNull, (object) nameof (assembly)));
      return !string.IsNullOrWhiteSpace(resourceName) ? Utils.InnerGetResourceAsString(resourceName, assembly) : throw new ArgumentException(string.Format(Strings.Common_ArgumentIsEmpty, (object) nameof (resourceName)), nameof (resourceName));
    }

    private static string InnerGetResourceAsString(string resourceName, Assembly assembly)
    {
      using (Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName))
      {
        if (manifestResourceStream == null)
          throw new NullReferenceException(string.Format(Strings.Resources_ResourceIsNull, (object) resourceName));
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
          return streamReader.ReadToEnd();
      }
    }

    /// <summary>Detect if a stream is text and detect the encoding</summary>
    /// <param name="stream">Stream</param>
    /// <param name="sampleSize">Number of characters to use for testing</param>
    /// <param name="encoding">Detected encoding</param>
    /// <returns>Result of check (true - is text; false - is binary)</returns>
    public static bool IsTextStream(Stream stream, int sampleSize, out Encoding encoding)
    {
      byte[] buffer1 = new byte[sampleSize];
      char[] buffer2 = new char[sampleSize];
      bool flag = true;
      int num = stream.Read(buffer1, 0, buffer1.Length);
      stream.Seek(0L, SeekOrigin.Begin);
      encoding = buffer1[0] != (byte) 239 || buffer1[1] != (byte) 187 || buffer1[2] != (byte) 191 ? (buffer1[0] != (byte) 254 || buffer1[1] != byte.MaxValue ? (buffer1[0] != (byte) 0 || buffer1[1] != (byte) 0 || (buffer1[2] != (byte) 254 || buffer1[3] != byte.MaxValue) ? (buffer1[0] != (byte) 43 || buffer1[1] != (byte) 47 || buffer1[2] != (byte) 118 ? Encoding.Default : Encoding.UTF7) : Encoding.UTF32) : Encoding.Unicode) : Encoding.UTF8;
      using (StreamReader streamReader = new StreamReader(stream))
        streamReader.Read(buffer2, 0, buffer2.Length);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream, encoding))
        {
          streamWriter.Write(buffer2);
          streamWriter.Flush();
          byte[] buffer3 = memoryStream.GetBuffer();
          for (int index = 0; index < num & flag; ++index)
            flag = (int) buffer1[index] == (int) buffer3[index];
        }
      }
      return flag;
    }

    /// <summary>Gets a stream from the string value</summary>
    /// <param name="value">String value</param>
    /// <returns>Stream</returns>
    public static Stream GetStreamFromString(string value)
    {
      MemoryStream memoryStream = new MemoryStream();
      StreamWriter streamWriter = new StreamWriter((Stream) memoryStream);
      streamWriter.Write(value);
      streamWriter.Flush();
      memoryStream.Position = 0L;
      return (Stream) memoryStream;
    }
  }
}
