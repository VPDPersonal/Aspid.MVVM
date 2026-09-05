---
title: "Class RepeatStringConverter"
sidebar_label: "RepeatStringConverter"
description: "Class RepeatStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RepeatStringConverter {#Aspid_MVVM_StarterKit_RepeatStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Repeats a piece of text once per count.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Repeat", Tooltip = "Repeats a piece of text once per count")]
public sealed class RepeatStringConverter : IConverter<int, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RepeatStringConverter](Aspid.MVVM.StarterKit.RepeatStringConverter.md)

#### Implements

[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### RepeatStringConverter\(\) {#Aspid_MVVM_StarterKit_RepeatStringConverter__ctor}

```csharp
public RepeatStringConverter()
```

#### Remarks

Default: with five stars.

### RepeatStringConverter\(string, int, string?\) {#Aspid_MVVM_StarterKit_RepeatStringConverter__ctor_System_String_System_Int32_System_String_}

```csharp
public RepeatStringConverter(string unit, int max = 5, string? emptyUnit = "")
```

#### Parameters

`unit` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text repeated once per count.

`max` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The total number of units. Zero means no maximum, with the count capped at 1000.

`emptyUnit` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text used for the remainder up to the maximum.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">unit</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_RepeatStringConverter_Convert_System_Int32_}

Repeats the unit the specified number of times.

```csharp
public string Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many units to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The repeated text.

