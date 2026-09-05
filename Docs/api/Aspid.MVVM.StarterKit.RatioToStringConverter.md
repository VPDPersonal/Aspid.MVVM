---
title: "Class RatioToStringConverter"
sidebar_label: "RatioToStringConverter"
description: "Class RatioToStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RatioToStringConverter {#Aspid_MVVM_StarterKit_RatioToStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a number against a maximum: "35 / 100".

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Ratio", Tooltip = "Formats a number against a maximum: '35 / 100'")]
public sealed class RatioToStringConverter : IConverter<float, string>, IConverter<int, string>, IConverter<long, string>, IConverter<double, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RatioToStringConverter](Aspid.MVVM.StarterKit.RatioToStringConverter.md)

#### Implements

[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### RatioToStringConverter\(\) {#Aspid_MVVM_StarterKit_RatioToStringConverter__ctor}

```csharp
public RatioToStringConverter()
```

#### Remarks

Default: against 100.

### RatioToStringConverter\(float, string\) {#Aspid_MVVM_StarterKit_RatioToStringConverter__ctor_System_Single_System_String_}

```csharp
public RatioToStringConverter(float max, string format = "{0} / {1}")
```

#### Parameters

`max` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value the number is shown against.

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A composite format: <code>\{0\}</code> is the value, <code>\{1\}</code> the maximum. A blank or invalid one falls back to a slash.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_RatioToStringConverter_Convert_System_Single_}

Formats the specified value against the authored maximum.

```csharp
public string Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted ratio, or the default layout when the format is unusable.

