---
title: "Class ConcatStringConverter"
sidebar_label: "ConcatStringConverter"
description: "Class ConcatStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConcatStringConverter {#Aspid_MVVM_StarterKit_ConcatStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Wraps a string in authored text, and takes that text back off.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Concat", Tooltip = "Wraps a string in authored text")]
public sealed class ConcatStringConverter : ITwoWayConverter<string?, string?>, IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ConcatStringConverter](Aspid.MVVM.StarterKit.ConcatStringConverter.md)

#### Implements

[ITwoWayConverter\<string?, string?\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ConcatStringConverter\(\) {#Aspid_MVVM_StarterKit_ConcatStringConverter__ctor}

```csharp
public ConcatStringConverter()
```

#### Remarks

Default: with no text to wrap the value in.

### ConcatStringConverter\(string, string, bool\) {#Aspid_MVVM_StarterKit_ConcatStringConverter__ctor_System_String_System_String_System_Boolean_}

```csharp
public ConcatStringConverter(string prefix, string suffix, bool skipWhenEmpty = true)
```

#### Parameters

`prefix` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed before the value.

`suffix` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed after the value.

`skipWhenEmpty` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, leaves a blank value undecorated.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_ConcatStringConverter_Convert_System_String_}

Wraps the specified string.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to wrap.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The wrapped string, or the value unchanged when it is blank and that is configured.

### ConvertBack\(string?\) {#Aspid_MVVM_StarterKit_ConcatStringConverter_ConvertBack_System_String_}

Takes the authored text back off the specified string.

```csharp
public string? ConvertBack(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to undecorate.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string without the prefix and the suffix; text carrying neither comes back unchanged.

