---
title: "Class DateTimeCompareConverter"
sidebar_label: "DateTimeCompareConverter"
description: "Class DateTimeCompareConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeCompareConverter {#Aspid_MVVM_StarterKit_DateTimeCompareConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Compares a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) with a reference moment.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time/To Bool", Name = "Compare", Tooltip = "Compares a DateTime with a reference moment")]
public sealed class DateTimeCompareConverter : IConverter<DateTime, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DateTimeCompareConverter](Aspid.MVVM.StarterKit.DateTimeCompareConverter.md)

#### Implements

[IConverter\<DateTime, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Compared in UTC when both kinds are known; otherwise by raw ticks.

## Constructors

### DateTimeCompareConverter\(\) {#Aspid_MVVM_StarterKit_DateTimeCompareConverter__ctor}

```csharp
public DateTimeCompareConverter()
```

#### Remarks

Default: comparing whether the bound moment is later than now.

### DateTimeCompareConverter\(ComparisonMode, ReferenceSource\) {#Aspid_MVVM_StarterKit_DateTimeCompareConverter__ctor_Aspid_MVVM_StarterKit_ComparisonMode_Aspid_MVVM_StarterKit_ReferenceSource_}

```csharp
public DateTimeCompareConverter(ComparisonMode comparison, ReferenceSource referenceSource = ReferenceSource.Now)
```

#### Parameters

`comparison` [ComparisonMode](Aspid.MVVM.StarterKit.ComparisonMode.md)

How the bound moment is compared with the reference.

`referenceSource` [ReferenceSource](Aspid.MVVM.StarterKit.ReferenceSource.md)

What the bound moment is compared against. Match it to the bound moment's kind.

### DateTimeCompareConverter\(ComparisonMode, DateTime\) {#Aspid_MVVM_StarterKit_DateTimeCompareConverter__ctor_Aspid_MVVM_StarterKit_ComparisonMode_System_DateTime_}

```csharp
public DateTimeCompareConverter(ComparisonMode comparison, DateTime reference)
```

#### Parameters

`comparison` [ComparisonMode](Aspid.MVVM.StarterKit.ComparisonMode.md)

How the bound moment is compared with the reference.

`reference` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The fixed moment compared against.

## Methods

### Convert\(DateTime\) {#Aspid_MVVM_StarterKit_DateTimeCompareConverter_Convert_System_DateTime_}

Compares the specified moment with the reference.

```csharp
public bool Convert(DateTime value)
```

#### Parameters

`value` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The moment to compare.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result. Out-of-range ticks, an undeclared source or comparison report an error and return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

