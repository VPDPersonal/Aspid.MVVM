---
title: "Class ByteSizeConverter"
sidebar_label: "ByteSizeConverter"
description: "Class ByteSizeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ByteSizeConverter {#Aspid_MVVM_StarterKit_ByteSizeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a byte count as a readable size.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Byte Size", Tooltip = "Formats a byte count as a readable size")]
public sealed class ByteSizeConverter : IConverter<long, string>, IConverter<int, string>, IConverter<float, string>, IConverter<double, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ByteSizeConverter](Aspid.MVVM.StarterKit.ByteSizeConverter.md)

#### Implements

[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A float or double input is truncated to whole bytes.

## Constructors

### ByteSizeConverter\(\) {#Aspid_MVVM_StarterKit_ByteSizeConverter__ctor}

```csharp
public ByteSizeConverter()
```

#### Remarks

Default: binary units with one decimal.

### ByteSizeConverter\(bool, int\) {#Aspid_MVVM_StarterKit_ByteSizeConverter__ctor_System_Boolean_System_Int32_}

```csharp
public ByteSizeConverter(bool binaryUnits, int decimals = 1)
```

#### Parameters

`binaryUnits` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, uses 1024 and KiB-style units rather than 1000 and KB.

`decimals` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many decimals to show.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">decimals</code> is negative.

## Methods

### Convert\(long\) {#Aspid_MVVM_StarterKit_ByteSizeConverter_Convert_System_Int64_}

Formats the specified byte count.

```csharp
public string Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number of bytes.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted size.

