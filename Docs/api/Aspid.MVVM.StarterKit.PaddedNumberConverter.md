---
title: "Class PaddedNumberConverter"
sidebar_label: "PaddedNumberConverter"
description: "Class PaddedNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PaddedNumberConverter {#Aspid_MVVM_StarterKit_PaddedNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Pads a number to a fixed width: 7 becomes "007".

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Padded", Tooltip = "Pads a number to a fixed width: 7 becomes '007'")]
public sealed class PaddedNumberConverter : IConverter<int, string>, IConverter<long, string>, IConverter<float, string>, IConverter<double, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PaddedNumberConverter](Aspid.MVVM.StarterKit.PaddedNumberConverter.md)

#### Implements

[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A float or double input is truncated to a whole number.

## Constructors

### PaddedNumberConverter\(\) {#Aspid_MVVM_StarterKit_PaddedNumberConverter__ctor}

```csharp
public PaddedNumberConverter()
```

#### Remarks

Default: padding to two digits.

### PaddedNumberConverter\(int, char\) {#Aspid_MVVM_StarterKit_PaddedNumberConverter__ctor_System_Int32_System_Char_}

```csharp
public PaddedNumberConverter(int digits, char padChar = '0')
```

#### Parameters

`digits` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The minimum number of digits.

`padChar` [char](https://learn.microsoft.com/dotnet/api/system.char)

The character used for padding.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">digits</code> is negative.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_PaddedNumberConverter_Convert_System_Int32_}

Pads the specified number.

```csharp
public string Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to pad.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The padded number; a negative keeps its sign outside the padding.

