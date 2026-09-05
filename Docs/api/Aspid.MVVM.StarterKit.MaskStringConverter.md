---
title: "Class MaskStringConverter"
sidebar_label: "MaskStringConverter"
description: "Class MaskStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MaskStringConverter {#Aspid_MVVM_StarterKit_MaskStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Hides the middle of a string, keeping a few characters at each end.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Mask", Tooltip = "Hides the middle of a string, keeping a few characters at each end")]
public sealed class MaskStringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MaskStringConverter](Aspid.MVVM.StarterKit.MaskStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### MaskStringConverter\(\) {#Aspid_MVVM_StarterKit_MaskStringConverter__ctor}

```csharp
public MaskStringConverter()
```

#### Remarks

Default: showing two characters at each end.

### MaskStringConverter\(int, int, char\) {#Aspid_MVVM_StarterKit_MaskStringConverter__ctor_System_Int32_System_Int32_System_Char_}

```csharp
public MaskStringConverter(int visibleHead, int visibleTail, char maskChar = '•')
```

#### Parameters

`visibleHead` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many characters to leave visible at the start.

`visibleTail` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many characters to leave visible at the end.

`maskChar` [char](https://learn.microsoft.com/dotnet/api/system.char)

The character the hidden part is written with.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when a visible count is negative.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_MaskStringConverter_Convert_System_String_}

Masks the middle of the specified string.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to mask.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The masked string. A string too short to keep both ends is masked completely; a blank one comes back unmasked.

#### Remarks

A visible count landing inside a surrogate pair hides the whole character.

