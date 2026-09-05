---
title: "Class RichTextSizeConverter"
sidebar_label: "RichTextSizeConverter"
description: "Class RichTextSizeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RichTextSizeConverter {#Aspid_MVVM_StarterKit_RichTextSizeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Wraps a string in a rich-text size tag.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/Rich Text", Name = "Size", Tooltip = "Wraps a string in a rich-text size tag")]
public sealed class RichTextSizeConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RichTextSizeConverter](Aspid.MVVM.StarterKit.RichTextSizeConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### RichTextSizeConverter\(\) {#Aspid_MVVM_StarterKit_RichTextSizeConverter__ctor}

```csharp
public RichTextSizeConverter()
```

#### Remarks

Default: at full size.

### RichTextSizeConverter\(float, bool\) {#Aspid_MVVM_StarterKit_RichTextSizeConverter__ctor_System_Single_System_Boolean_}

```csharp
public RichTextSizeConverter(float size, bool isPercent = true)
```

#### Parameters

`size` [float](https://learn.microsoft.com/dotnet/api/system.single)

The size applied to the text.

`isPercent` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, treats the size as a percentage.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">size</code> is not above zero.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_RichTextSizeConverter_Convert_System_String_}

Wraps the specified string in a size tag.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to resize.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The tagged string, or the string untagged when it is blank, spaces included, or the
configured size is not above zero.

