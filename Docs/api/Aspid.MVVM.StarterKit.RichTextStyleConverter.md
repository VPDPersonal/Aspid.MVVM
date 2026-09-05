---
title: "Class RichTextStyleConverter"
sidebar_label: "RichTextStyleConverter"
description: "Class RichTextStyleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RichTextStyleConverter {#Aspid_MVVM_StarterKit_RichTextStyleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Wraps a string in rich-text style tags.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/Rich Text", Name = "Style", Tooltip = "Wraps a string in rich-text style tags")]
public sealed class RichTextStyleConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RichTextStyleConverter](Aspid.MVVM.StarterKit.RichTextStyleConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### RichTextStyleConverter\(\) {#Aspid_MVVM_StarterKit_RichTextStyleConverter__ctor}

```csharp
public RichTextStyleConverter()
```

#### Remarks

Default: no styling, which leaves the string untagged.

### RichTextStyleConverter\(bool, bool, bool, bool\) {#Aspid_MVVM_StarterKit_RichTextStyleConverter__ctor_System_Boolean_System_Boolean_System_Boolean_System_Boolean_}

```csharp
public RichTextStyleConverter(bool bold = false, bool italic = false, bool underline = false, bool strikethrough = false)
```

#### Parameters

`bold` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to wrap in <code>&lt;b&gt;</code>.

`italic` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to wrap in <code>&lt;i&gt;</code>.

`underline` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to wrap in <code>&lt;u&gt;</code>.

`strikethrough` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to wrap in <code>&lt;s&gt;</code>.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_RichTextStyleConverter_Convert_System_String_}

Wraps the specified string in the configured tags.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to style.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The tagged string; a blank string, spaces included, is left untagged.

