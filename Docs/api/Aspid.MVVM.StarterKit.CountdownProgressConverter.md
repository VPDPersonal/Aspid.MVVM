---
title: "Class CountdownProgressConverter"
sidebar_label: "CountdownProgressConverter"
description: "Class CountdownProgressConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CountdownProgressConverter {#Aspid_MVVM_StarterKit_CountdownProgressConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts seconds remaining to a 0..1 progress value.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Countdown Progress", Tooltip = "Converts seconds remaining to a 0..1 progress value")]
public sealed class CountdownProgressConverter : IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CountdownProgressConverter](Aspid.MVVM.StarterKit.CountdownProgressConverter.md)

#### Implements

[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### CountdownProgressConverter\(\) {#Aspid_MVVM_StarterKit_CountdownProgressConverter__ctor}

```csharp
public CountdownProgressConverter()
```

#### Remarks

Default: over one second.

### CountdownProgressConverter\(float, bool\) {#Aspid_MVVM_StarterKit_CountdownProgressConverter__ctor_System_Single_System_Boolean_}

```csharp
public CountdownProgressConverter(float totalSeconds, bool elapsed = false)
```

#### Parameters

`totalSeconds` [float](https://learn.microsoft.com/dotnet/api/system.single)

The full duration, in seconds. Zero reads as a finished timer.

`elapsed` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, returns the elapsed fraction.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">totalSeconds</code> is negative.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_CountdownProgressConverter_Convert_System_Single_}

Converts the specified seconds remaining to a progress value.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The seconds remaining.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 progress. A duration of zero reads as a finished timer.

