---
title: "Class NumberFormatConverter"
sidebar_label: "NumberFormatConverter"
description: "Class NumberFormatConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NumberFormatConverter {#Aspid_MVVM_StarterKit_NumberFormatConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a number with a standard .NET format string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Format", Tooltip = "Formats a number with a standard .NET format string")]
public sealed class NumberFormatConverter : IConverter<float, string>, IConverter<double, string>, IConverter<int, string>, IConverter<long, string>, IConverter<decimal, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberFormatConverter](Aspid.MVVM.StarterKit.NumberFormatConverter.md)

#### Implements

[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<decimal, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The format is the specifier itself: <code>N0</code>, not the composite <code>\{0:N0\}</code>.

## Constructors

### NumberFormatConverter\(\) {#Aspid_MVVM_StarterKit_NumberFormatConverter__ctor}

```csharp
public NumberFormatConverter()
```

#### Remarks

Default: formatting with thousands separators.

### NumberFormatConverter\(string, CultureInfoMode\) {#Aspid_MVVM_StarterKit_NumberFormatConverter__ctor_System_String_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public NumberFormatConverter(string format, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A standard numeric format string. One .NET refuses falls back to the general format.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the number is formatted with.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_NumberFormatConverter_Convert_System_Single_}

Converts the specified value.

```csharp
public string Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

### Convert\(double\) {#Aspid_MVVM_StarterKit_NumberFormatConverter_Convert_System_Double_}

Converts the specified value.

```csharp
public string Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

### Convert\(int\) {#Aspid_MVVM_StarterKit_NumberFormatConverter_Convert_System_Int32_}

Converts the specified value.

```csharp
public string Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

### Convert\(long\) {#Aspid_MVVM_StarterKit_NumberFormatConverter_Convert_System_Int64_}

Converts the specified value.

```csharp
public string Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

### Convert\(decimal\) {#Aspid_MVVM_StarterKit_NumberFormatConverter_Convert_System_Decimal_}

Converts the specified value.

```csharp
public string Convert(decimal value)
```

#### Parameters

`value` [decimal](https://learn.microsoft.com/dotnet/api/system.decimal)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

