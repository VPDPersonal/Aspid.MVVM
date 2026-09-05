---
title: "Class ClampNumberConverter"
sidebar_label: "ClampNumberConverter"
description: "Class ClampNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ClampNumberConverter {#Aspid_MVVM_StarterKit_ClampNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Keeps a number inside a range.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Clamp", Tooltip = "Keeps a number inside a range")]
public sealed class ClampNumberConverter : IConverter<int, int>, IConverter<long, long>, IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ClampNumberConverter](Aspid.MVVM.StarterKit.ClampNumberConverter.md)

#### Implements

[IConverter\<int, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

An in-range int or long is returned untouched, so it never round-trips through a floating-point number.

## Constructors

### ClampNumberConverter\(\) {#Aspid_MVVM_StarterKit_ClampNumberConverter__ctor}

```csharp
public ClampNumberConverter()
```

#### Remarks

Default: clamping to 0..1.

### ClampNumberConverter\(double, double, ClampMode\) {#Aspid_MVVM_StarterKit_ClampNumberConverter__ctor_System_Double_System_Double_Aspid_MVVM_StarterKit_ClampMode_}

```csharp
public ClampNumberConverter(double min, double max, ClampMode mode = ClampMode.Both)
```

#### Parameters

`min` [double](https://learn.microsoft.com/dotnet/api/system.double)

The lowest value allowed through. Inverted bounds report an error and are swapped.

`max` [double](https://learn.microsoft.com/dotnet/api/system.double)

The highest value allowed through. Inverted bounds report an error and are swapped.

`mode` [ClampMode](Aspid.MVVM.StarterKit.ClampMode.md)

Which bound to apply.

## Methods

### Convert\(double\) {#Aspid_MVVM_StarterKit_ClampNumberConverter_Convert_System_Double_}

Clamps the specified value.

```csharp
public double Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value to clamp.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The value held inside the bounds; a NaN passes through. Inverted bounds are swapped, an undeclared mode
leaves the value unclamped, both with an error.

### Convert\(float\) {#Aspid_MVVM_StarterKit_ClampNumberConverter_Convert_System_Single_}

Clamps the specified value.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to clamp.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The value held inside the bounds; a NaN passes through. Inverted bounds are swapped, an undeclared mode
leaves the value unclamped, both with an error.

### Convert\(int\) {#Aspid_MVVM_StarterKit_ClampNumberConverter_Convert_System_Int32_}

Clamps the specified value.

```csharp
public int Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value to clamp.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value held inside the bounds; a NaN passes through. Inverted bounds are swapped, an undeclared mode
leaves the value unclamped, both with an error.

### Convert\(long\) {#Aspid_MVVM_StarterKit_ClampNumberConverter_Convert_System_Int64_}

Clamps the specified value.

```csharp
public long Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value to clamp.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value held inside the bounds; a NaN passes through. Inverted bounds are swapped, an undeclared mode
leaves the value unclamped, both with an error.

