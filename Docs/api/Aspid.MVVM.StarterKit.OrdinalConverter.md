---
title: "Class OrdinalConverter"
sidebar_label: "OrdinalConverter"
description: "Class OrdinalConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OrdinalConverter {#Aspid_MVVM_StarterKit_OrdinalConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a number as an English ordinal: 1 becomes "1st".

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Ordinal", Tooltip = "Formats a number as an English ordinal: 1 becomes '1st'")]
public sealed class OrdinalConverter : IConverter<int, string>, IConverter<long, string>, IConverter<float, string>, IConverter<double, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OrdinalConverter](Aspid.MVVM.StarterKit.OrdinalConverter.md)

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

The suffix stays English whichever culture is picked. A float or double input is truncated.

## Constructors

### OrdinalConverter\(\) {#Aspid_MVVM_StarterKit_OrdinalConverter__ctor}

```csharp
public OrdinalConverter()
```

#### Remarks

Default: writing invariant digits.

### OrdinalConverter\(CultureInfoMode\) {#Aspid_MVVM_StarterKit_OrdinalConverter__ctor_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public OrdinalConverter(CultureInfoMode culture)
```

#### Parameters

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the number is written with. Affects only the negative sign.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_OrdinalConverter_Convert_System_Int32_}

Formats the specified number as an ordinal.

```csharp
public string Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The number with its ordinal suffix.

