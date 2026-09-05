---
title: "Class TrimStringConverter"
sidebar_label: "TrimStringConverter"
description: "Class TrimStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TrimStringConverter {#Aspid_MVVM_StarterKit_TrimStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Removes surrounding characters from a string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Trim", Tooltip = "Removes surrounding characters from a string")]
public sealed class TrimStringConverter : IConverter<string?, string?>, IConverter, ISerializationCallbackReceiver
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TrimStringConverter](Aspid.MVVM.StarterKit.TrimStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md), 
ISerializationCallbackReceiver


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### TrimStringConverter\(\) {#Aspid_MVVM_StarterKit_TrimStringConverter__ctor}

```csharp
public TrimStringConverter()
```

#### Remarks

Default: trimming whitespace from both ends.

### TrimStringConverter\(TrimSide, string\) {#Aspid_MVVM_StarterKit_TrimStringConverter__ctor_Aspid_MVVM_StarterKit_TrimSide_System_String_}

```csharp
public TrimStringConverter(TrimSide side, string trimChars = "")
```

#### Parameters

`side` [TrimSide](Aspid.MVVM.StarterKit.TrimSide.md)

Which ends to trim.

`trimChars` [string](https://learn.microsoft.com/dotnet/api/system.string)

The characters to remove. When empty, whitespace is removed.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_TrimStringConverter_Convert_System_String_}

Trims the specified string.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to trim.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The trimmed string. An undeclared side reports an error and returns the value unchanged.

