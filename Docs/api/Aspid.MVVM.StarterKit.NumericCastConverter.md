---
title: "Class NumericCastConverter"
sidebar_label: "NumericCastConverter"
description: "Class NumericCastConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NumericCastConverter {#Aspid_MVVM_StarterKit_NumericCastConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a number to another numeric type under a chosen overflow policy.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Cast", Tooltip = "Converts a number to another numeric type under a chosen overflow policy")]
public sealed class NumericCastConverter : IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumericCastConverter](Aspid.MVVM.StarterKit.NumericCastConverter.md)

#### Implements

[IConverter\<int, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Widening conversions never overflow, so the mode applies to narrowing ones only.

## Constructors

### NumericCastConverter\(\) {#Aspid_MVVM_StarterKit_NumericCastConverter__ctor}

```csharp
public NumericCastConverter()
```

#### Remarks

Default: saturating at the target type's bounds.

### NumericCastConverter\(OverflowMode\) {#Aspid_MVVM_StarterKit_NumericCastConverter__ctor_Aspid_MVVM_StarterKit_OverflowMode_}

```csharp
public NumericCastConverter(OverflowMode mode)
```

#### Parameters

`mode` [OverflowMode](Aspid.MVVM.StarterKit.OverflowMode.md)

What to do with a value the target type cannot hold. [`OverflowMode.Checked`](Aspid.MVVM.StarterKit.OverflowMode.md) throws.

