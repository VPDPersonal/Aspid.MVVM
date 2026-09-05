---
title: "Class TruncateStringConverter"
sidebar_label: "TruncateStringConverter"
description: "Class TruncateStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TruncateStringConverter {#Aspid_MVVM_StarterKit_TruncateStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Shortens a string that is too long to fit.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Truncate", Tooltip = "Shortens a string that is too long to fit")]
public sealed class TruncateStringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TruncateStringConverter](Aspid.MVVM.StarterKit.TruncateStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### TruncateStringConverter\(\) {#Aspid_MVVM_StarterKit_TruncateStringConverter__ctor}

```csharp
public TruncateStringConverter()
```

#### Remarks

Default: cutting at twenty characters.

### TruncateStringConverter\(int, TruncateSide, bool\) {#Aspid_MVVM_StarterKit_TruncateStringConverter__ctor_System_Int32_Aspid_MVVM_StarterKit_TruncateSide_System_Boolean_}

```csharp
public TruncateStringConverter(int maxLength, TruncateSide side = TruncateSide.End, bool atWordBoundary = false)
```

#### Parameters

`maxLength` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The longest string allowed through, ellipsis included.

`side` [TruncateSide](Aspid.MVVM.StarterKit.TruncateSide.md)

Which end to cut from.

`atWordBoundary` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, cuts at a space rather than mid-word. [`TruncateSide.End`](Aspid.MVVM.StarterKit.TruncateSide.md) only.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">maxLength</code> is not positive.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_TruncateStringConverter_Convert_System_String_}

Shortens the specified string if it exceeds the limit.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to shorten.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string, no longer than the limit. An undeclared side reports an error and returns the value unchanged.

#### Remarks

Cuts never split a surrogate pair.

