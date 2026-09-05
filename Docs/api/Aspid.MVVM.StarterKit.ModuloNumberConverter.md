---
title: "Class ModuloNumberConverter"
sidebar_label: "ModuloNumberConverter"
description: "Class ModuloNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ModuloNumberConverter {#Aspid_MVVM_StarterKit_ModuloNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Returns the remainder of a number divided by an authored divisor.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Modulo", Tooltip = "Returns the remainder of a number divided by an authored divisor")]
public sealed class ModuloNumberConverter : IConverter<int, int>, IConverter<long, long>, IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ModuloNumberConverter](Aspid.MVVM.StarterKit.ModuloNumberConverter.md)

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

The int and long overloads use the divisor's whole-number part and stay in integers.

## Constructors

### ModuloNumberConverter\(\) {#Aspid_MVVM_StarterKit_ModuloNumberConverter__ctor}

```csharp
public ModuloNumberConverter()
```

#### Remarks

Default: dividing by one.

### ModuloNumberConverter\(double, bool\) {#Aspid_MVVM_StarterKit_ModuloNumberConverter__ctor_System_Double_System_Boolean_}

```csharp
public ModuloNumberConverter(double divisor, bool euclidean = true)
```

#### Parameters

`divisor` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the value is divided by. Zero passes the value through. Integers use its whole-number part.

`euclidean` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, returns a non-negative remainder.

## Methods

### Convert\(double\) {#Aspid_MVVM_StarterKit_ModuloNumberConverter_Convert_System_Double_}

Divides the specified value by the divisor and returns what is left.

```csharp
public double Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value to divide.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The remainder. A divisor of zero reports an error and returns the value unchanged.

### Convert\(float\) {#Aspid_MVVM_StarterKit_ModuloNumberConverter_Convert_System_Single_}

Divides the specified value by the divisor and returns what is left.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to divide.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The remainder. A divisor of zero reports an error and returns the value unchanged.

### Convert\(int\) {#Aspid_MVVM_StarterKit_ModuloNumberConverter_Convert_System_Int32_}

Divides the specified value by the divisor and returns what is left.

```csharp
public int Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value to divide.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The remainder. A divisor of zero reports an error and returns the value unchanged.

### Convert\(long\) {#Aspid_MVVM_StarterKit_ModuloNumberConverter_Convert_System_Int64_}

Divides the specified value by the divisor and returns what is left.

```csharp
public long Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value to divide.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

The remainder. A divisor of zero reports an error and returns the value unchanged.

