---
title: "Class BoolInvertConverter"
sidebar_label: "BoolInvertConverter"
description: "Class BoolInvertConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BoolInvertConverter {#Aspid_MVVM_StarterKit_BoolInvertConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Negates a boolean.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Bool", Name = "Invert", Tooltip = "Negates a boolean")]
public sealed class BoolInvertConverter : ITwoWayConverter<bool, bool>, IConverter<bool, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BoolInvertConverter](Aspid.MVVM.StarterKit.BoolInvertConverter.md)

#### Implements

[ITwoWayConverter\<bool, bool\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<bool, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Methods

### Convert\(bool\) {#Aspid_MVVM_StarterKit_BoolInvertConverter_Convert_System_Boolean_}

Negates the specified value.

```csharp
public bool Convert(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value to negate.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The negated value.

### ConvertBack\(bool\) {#Aspid_MVVM_StarterKit_BoolInvertConverter_ConvertBack_System_Boolean_}

Negates the specified value.

```csharp
public bool ConvertBack(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value to negate.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The negated value.

