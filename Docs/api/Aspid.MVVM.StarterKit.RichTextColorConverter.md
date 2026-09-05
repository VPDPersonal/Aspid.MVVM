---
title: "Class RichTextColorConverter"
sidebar_label: "RichTextColorConverter"
description: "Class RichTextColorConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RichTextColorConverter {#Aspid_MVVM_StarterKit_RichTextColorConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Wraps a string in a rich-text color tag.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/Rich Text", Name = "Color", Tooltip = "Wraps a string in a rich-text color tag")]
public sealed class RichTextColorConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RichTextColorConverter](Aspid.MVVM.StarterKit.RichTextColorConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### RichTextColorConverter\(\) {#Aspid_MVVM_StarterKit_RichTextColorConverter__ctor}

```csharp
public RichTextColorConverter()
```

#### Remarks

Default: coloring white.

### RichTextColorConverter\(Color, bool\) {#Aspid_MVVM_StarterKit_RichTextColorConverter__ctor_UnityEngine_Color_System_Boolean_}

```csharp
public RichTextColorConverter(Color color, bool includeAlpha = false)
```

#### Parameters

`color` Color

The color the text is tagged with.

`includeAlpha` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, includes the alpha channel.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_RichTextColorConverter_Convert_System_String_}

Wraps the specified string in a color tag.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to color.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The tagged string; a blank string, spaces included, is left untagged.

