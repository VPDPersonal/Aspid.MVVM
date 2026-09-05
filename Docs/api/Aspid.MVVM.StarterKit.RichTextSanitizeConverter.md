---
title: "Class RichTextSanitizeConverter"
sidebar_label: "RichTextSanitizeConverter"
description: "Class RichTextSanitizeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RichTextSanitizeConverter {#Aspid_MVVM_StarterKit_RichTextSanitizeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Takes rich-text markup out of a string, or shows it as text instead of obeying it.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/Rich Text", Name = "Sanitize", Tooltip = "Takes rich-text markup out of a string, or shows it as text instead of obeying it")]
public sealed class RichTextSanitizeConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RichTextSanitizeConverter](Aspid.MVVM.StarterKit.RichTextSanitizeConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

[`RichTextSanitize.Escape`](Aspid.MVVM.StarterKit.RichTextSanitize.md) emits TextMeshPro's <code>&lt;noparse&gt;</code>; legacy uGUI <code>Text</code> needs [`RichTextSanitize.Strip`](Aspid.MVVM.StarterKit.RichTextSanitize.md).

## Constructors

### RichTextSanitizeConverter\(\) {#Aspid_MVVM_StarterKit_RichTextSanitizeConverter__ctor}

```csharp
public RichTextSanitizeConverter()
```

#### Remarks

Default: stripping every tag.

### RichTextSanitizeConverter\(RichTextSanitize, string\[\]?, bool\) {#Aspid_MVVM_StarterKit_RichTextSanitizeConverter__ctor_Aspid_MVVM_StarterKit_RichTextSanitize_System_String___System_Boolean_}

```csharp
public RichTextSanitizeConverter(RichTextSanitize mode, string[]? allowedTags = null, bool keepStrayBrackets = true)
```

#### Parameters

`mode` [RichTextSanitize](Aspid.MVVM.StarterKit.RichTextSanitize.md)

Whether markup is removed or shown as text.

`allowedTags` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Tag names allowed through, without brackets. Closing tags match; <code>color</code> covers <code>&lt;#RRGGBB&gt;</code>.

`keepStrayBrackets` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, a bracket that does not open a tag is left as text.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_RichTextSanitizeConverter_Convert_System_String_}

Removes or escapes the markup in the specified string, leaving the allowed tags untouched.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to sanitize.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The sanitized string. An undeclared mode reports an error and strips.

