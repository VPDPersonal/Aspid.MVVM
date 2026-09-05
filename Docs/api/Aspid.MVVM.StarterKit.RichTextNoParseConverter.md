---
title: "Class RichTextNoParseConverter"
sidebar_label: "RichTextNoParseConverter"
description: "Class RichTextNoParseConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RichTextNoParseConverter {#Aspid_MVVM_StarterKit_RichTextNoParseConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Stops rich-text markup in a string from being interpreted.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/Rich Text", Name = "No Parse", Tooltip = "Stops rich-text markup in a string from being interpreted")]
public sealed class RichTextNoParseConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RichTextNoParseConverter](Aspid.MVVM.StarterKit.RichTextNoParseConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

TextMeshPro reads markup out of any text it is given, including text a player typed;
<code>&lt;noparse&gt;</code> renders the characters instead.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_RichTextNoParseConverter_Convert_System_String_}

Wraps the specified string so its markup is shown rather than obeyed.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The untrusted string.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The wrapped string; a blank string, spaces included, comes back unwrapped.

