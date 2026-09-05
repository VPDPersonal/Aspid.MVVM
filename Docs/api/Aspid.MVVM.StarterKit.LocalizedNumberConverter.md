---
title: "Class LocalizedNumberConverter"
sidebar_label: "LocalizedNumberConverter"
description: "Class LocalizedNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LocalizedNumberConverter {#Aspid_MVVM_StarterKit_LocalizedNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a number with the culture of the selected locale.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Localization", Name = "Localized Number", Tooltip = "Formats a number with the culture of the selected locale")]
public sealed class LocalizedNumberConverter : IConverter<double, string>, IConverter<int, string>, IConverter<long, string>, IConverter<float, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LocalizedNumberConverter](Aspid.MVVM.StarterKit.LocalizedNumberConverter.md)

#### Implements

[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### LocalizedNumberConverter\(\) {#Aspid_MVVM_StarterKit_LocalizedNumberConverter__ctor}

```csharp
public LocalizedNumberConverter()
```

#### Remarks

Default: formatting with thousands separators.

### LocalizedNumberConverter\(string\) {#Aspid_MVVM_StarterKit_LocalizedNumberConverter__ctor_System_String_}

```csharp
public LocalizedNumberConverter(string format)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A standard numeric format string. One .NET refuses is reported as an error and the general
format is used instead.

## Methods

### Convert\(double\) {#Aspid_MVVM_StarterKit_LocalizedNumberConverter_Convert_System_Double_}

Formats the specified number with the selected locale's culture.

```csharp
public string Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted number.

