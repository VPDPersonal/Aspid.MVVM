---
title: "Class PadStringConverter"
sidebar_label: "PadStringConverter"
description: "Class PadStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PadStringConverter {#Aspid_MVVM_StarterKit_PadStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Pads a string to a fixed width.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Pad", Tooltip = "Pads a string to a fixed width")]
public sealed class PadStringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PadStringConverter](Aspid.MVVM.StarterKit.PadStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### PadStringConverter\(\) {#Aspid_MVVM_StarterKit_PadStringConverter__ctor}

```csharp
public PadStringConverter()
```

#### Remarks

Default: padding to eight characters.

### PadStringConverter\(int, char, bool\) {#Aspid_MVVM_StarterKit_PadStringConverter__ctor_System_Int32_System_Char_System_Boolean_}

```csharp
public PadStringConverter(int totalWidth, char padChar = ' ', bool padLeft = true)
```

#### Parameters

`totalWidth` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The width to pad to.

`padChar` [char](https://learn.microsoft.com/dotnet/api/system.char)

The character used for padding.

`padLeft` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, pads on the left.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">totalWidth</code> is negative.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_PadStringConverter_Convert_System_String_}

Pads the specified string.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to pad.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The padded string.

