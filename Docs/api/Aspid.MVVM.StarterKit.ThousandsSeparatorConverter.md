---
title: "Class ThousandsSeparatorConverter"
sidebar_label: "ThousandsSeparatorConverter"
description: "Class ThousandsSeparatorConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ThousandsSeparatorConverter {#Aspid_MVVM_StarterKit_ThousandsSeparatorConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Groups the digits of a whole number: 1234567 becomes "1,234,567".

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Thousands Separator", Tooltip = "Groups the digits of a whole number: 1234567 becomes '1,234,567'")]
public sealed class ThousandsSeparatorConverter : IConverter<long, string>, IConverter<int, string>, IConverter<float, string>, IConverter<double, string>, IConverter, ISerializationCallbackReceiver
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ThousandsSeparatorConverter](Aspid.MVVM.StarterKit.ThousandsSeparatorConverter.md)

#### Implements

[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md), 
ISerializationCallbackReceiver


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A float or double input is truncated to a whole number.

## Constructors

### ThousandsSeparatorConverter\(\) {#Aspid_MVVM_StarterKit_ThousandsSeparatorConverter__ctor}

```csharp
public ThousandsSeparatorConverter()
```

#### Remarks

Default: using the culture's separator.

### ThousandsSeparatorConverter\(string, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ThousandsSeparatorConverter__ctor_System_String_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public ThousandsSeparatorConverter(string separator, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`separator` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed between groups of digits. When empty, the culture's own is used.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the number is formatted with.

## Methods

### Convert\(long\) {#Aspid_MVVM_StarterKit_ThousandsSeparatorConverter_Convert_System_Int64_}

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

### Convert\(int\) {#Aspid_MVVM_StarterKit_ThousandsSeparatorConverter_Convert_System_Int32_}

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

