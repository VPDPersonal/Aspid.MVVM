---
title: "Class SplitJoinStringConverter"
sidebar_label: "SplitJoinStringConverter"
description: "Class SplitJoinStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SplitJoinStringConverter {#Aspid_MVVM_StarterKit_SplitJoinStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Splits a string and joins the parts back together with different text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Split Join", Tooltip = "Splits a string and joins the parts back together with different text")]
public sealed class SplitJoinStringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SplitJoinStringConverter](Aspid.MVVM.StarterKit.SplitJoinStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### SplitJoinStringConverter\(\) {#Aspid_MVVM_StarterKit_SplitJoinStringConverter__ctor}

```csharp
public SplitJoinStringConverter()
```

#### Remarks

Default: re-spacing a comma-separated list.

### SplitJoinStringConverter\(string, string, int\) {#Aspid_MVVM_StarterKit_SplitJoinStringConverter__ctor_System_String_System_String_System_Int32_}

```csharp
public SplitJoinStringConverter(string splitOn, string joinWith, int maxParts = 0)
```

#### Parameters

`splitOn` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text the value is split on. When empty, the value passes through.

`joinWith` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed between the parts when they are joined back.

`maxParts` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many parts to make. Zero makes as many as there are; the rest stays in the last part.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">maxParts</code> is negative.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_SplitJoinStringConverter_Convert_System_String_}

Splits the specified string and joins the parts back.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to re-split.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The rejoined string, or the value unchanged when it is blank or there is nothing to split on.

