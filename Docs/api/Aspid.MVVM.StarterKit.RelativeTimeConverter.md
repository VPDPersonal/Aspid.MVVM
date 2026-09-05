---
title: "Class RelativeTimeConverter"
sidebar_label: "RelativeTimeConverter"
description: "Class RelativeTimeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelativeTimeConverter {#Aspid_MVVM_StarterKit_RelativeTimeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes how long ago, or how far ahead, a moment is.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time/To String", Name = "Relative Time", Tooltip = "Writes how long ago, or how far ahead, a moment is")]
public sealed class RelativeTimeConverter : IConverter<DateTime, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelativeTimeConverter](Aspid.MVVM.StarterKit.RelativeTimeConverter.md)

#### Implements

[IConverter\<DateTime, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

With more than one unit the whole quantity arrives as <code>\{0\}</code> and <code>\{1\}</code> is empty.

## Constructors

### RelativeTimeConverter\(\) {#Aspid_MVVM_StarterKit_RelativeTimeConverter__ctor}

```csharp
public RelativeTimeConverter()
```

#### Remarks

Default: with English defaults.

### RelativeTimeConverter\(int, CultureInfoMode, bool\) {#Aspid_MVVM_StarterKit_RelativeTimeConverter__ctor_System_Int32_Aspid_MVVM_StarterKit_CultureInfoMode_System_Boolean_}

```csharp
public RelativeTimeConverter(int maxUnits, CultureInfoMode culture = CultureInfoMode.InvariantCulture, bool useUtcNow = false)
```

#### Parameters

`maxUnits` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many units to write, largest first, 1 to 4.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the amounts are written with.

`useUtcNow` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether an [`Unspecified`](https://learn.microsoft.com/dotnet/api/system.datetimekind.unspecified) moment is measured against UTC rather than local time.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">maxUnits</code> is outside 1..4.

## Methods

### Convert\(DateTime\) {#Aspid_MVVM_StarterKit_RelativeTimeConverter_Convert_System_DateTime_}

Writes how far the specified moment is from now.

```csharp
public string Convert(DateTime value)
```

#### Parameters

`value` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The moment to describe.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The description.

