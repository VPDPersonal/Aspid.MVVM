---
title: "Class PowerNumberConverter"
sidebar_label: "PowerNumberConverter"
description: "Class PowerNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PowerNumberConverter {#Aspid_MVVM_StarterKit_PowerNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Raises a number to an authored exponent.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Power", Tooltip = "Raises a number to an authored exponent")]
public sealed class PowerNumberConverter : TwoWayNumberConverter, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, ITwoWayConverter<int, int>, IConverter<int, int>, ITwoWayConverter<long, long>, IConverter<long, long>, ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md) ← 
[PowerNumberConverter](Aspid.MVVM.StarterKit.PowerNumberConverter.md)

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
[ITwoWayConverter\<int, int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<int, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<long, long\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<long, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<float, float\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<double, double\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Preserving the sign makes the curve odd: -2 with exponent 2 gives -4. Off, a negative base with a fractional exponent is NaN.

## Constructors

### PowerNumberConverter\(\) {#Aspid_MVVM_StarterKit_PowerNumberConverter__ctor}

```csharp
public PowerNumberConverter()
```

#### Remarks

Default: squaring the value.

### PowerNumberConverter\(float, bool, ConverterFallback\<double\>?\) {#Aspid_MVVM_StarterKit_PowerNumberConverter__ctor_System_Single_System_Boolean_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback_System_Double___}

```csharp
public PowerNumberConverter(float exponent, bool preserveSign = true, ConverterFallback<double>? convertBackFallback = null)
```

#### Parameters

`exponent` [float](https://learn.microsoft.com/dotnet/api/system.single)

The exponent the value is raised to. Zero cannot be reversed.

`preserveSign` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, raises the magnitude and puts the sign back.

`convertBackFallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<[double](https://learn.microsoft.com/dotnet/api/system.double)\>?

Returned from <code>ConvertBack</code> when the exponent is zero. When omitted, returns the input value unchanged.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_PowerNumberConverter_Apply_System_Double_}

Raises the number to the exponent.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to raise.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The raised number.

### Undo\(double\) {#Aspid_MVVM_StarterKit_PowerNumberConverter_Undo_System_Double_}

Raises the number to the reciprocal exponent.

```csharp
protected override double Undo(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform back.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the forward pass was given, or the fallback for a zero exponent.

