---
title: "Class SubstringConverter"
sidebar_label: "SubstringConverter"
description: "Class SubstringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SubstringConverter {#Aspid_MVVM_StarterKit_SubstringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Takes a slice out of a string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Substring", Tooltip = "Takes a slice out of a string")]
public sealed class SubstringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SubstringConverter](Aspid.MVVM.StarterKit.SubstringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### SubstringConverter\(\) {#Aspid_MVVM_StarterKit_SubstringConverter__ctor}

```csharp
public SubstringConverter()
```

#### Remarks

Default: taking the first character.

### SubstringConverter\(int, int\) {#Aspid_MVVM_StarterKit_SubstringConverter__ctor_System_Int32_System_Int32_}

```csharp
public SubstringConverter(int start, int length)
```

#### Parameters

`start` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Where the slice starts.

`length` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many characters to take. Zero takes everything from the start.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">start</code> or <code class="paramref">length</code> is negative.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_SubstringConverter_Convert_System_String_}

Takes the configured slice.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to slice.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The slice, clamped to what the string holds. A blank string comes back unchanged.

