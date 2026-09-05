---
title: "Class WrapNumberConverter"
sidebar_label: "WrapNumberConverter"
description: "Class WrapNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class WrapNumberConverter {#Aspid_MVVM_StarterKit_WrapNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Folds a number back into a range instead of clamping it.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Wrap", Tooltip = "Folds a number back into a range instead of clamping it")]
public sealed class WrapNumberConverter : NumberConverter, IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[WrapNumberConverter](Aspid.MVVM.StarterKit.WrapNumberConverter.md)

#### Implements

[IConverter\<int, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### WrapNumberConverter\(\) {#Aspid_MVVM_StarterKit_WrapNumberConverter__ctor}

```csharp
public WrapNumberConverter()
```

#### Remarks

Default: over 0..1.

### WrapNumberConverter\(NumberWrapMode, float, float\) {#Aspid_MVVM_StarterKit_WrapNumberConverter__ctor_Aspid_MVVM_StarterKit_NumberWrapMode_System_Single_System_Single_}

```csharp
public WrapNumberConverter(NumberWrapMode mode, float min, float max)
```

#### Parameters

`mode` [NumberWrapMode](Aspid.MVVM.StarterKit.NumberWrapMode.md)

How to fold a value that leaves the range.

`min` [float](https://learn.microsoft.com/dotnet/api/system.single)

The low end of the range. Inverted bounds report an error and are swapped.

`max` [float](https://learn.microsoft.com/dotnet/api/system.single)

The high end of the range. Equal to <code class="paramref">min</code>, the range pins the value.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_WrapNumberConverter_Apply_System_Double_}

Folds the number into the range.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to fold.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The folded number. Inverted bounds report an error and fold into the swapped range;
an undeclared mode reports an error and returns the value unchanged.

