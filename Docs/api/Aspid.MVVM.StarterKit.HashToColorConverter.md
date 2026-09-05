---
title: "Class HashToColorConverter"
sidebar_label: "HashToColorConverter"
description: "Class HashToColorConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HashToColorConverter {#Aspid_MVVM_StarterKit_HashToColorConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Derives a stable color from a string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Color", Name = "Hash To Color", Tooltip = "Derives a stable color from a string")]
public sealed class HashToColorConverter : IConverter<string?, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HashToColorConverter](Aspid.MVVM.StarterKit.HashToColorConverter.md)

#### Implements

[IConverter\<string?, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### HashToColorConverter\(\) {#Aspid_MVVM_StarterKit_HashToColorConverter__ctor}

```csharp
public HashToColorConverter()
```

#### Remarks

Default: a soft, bright color, falling back to gray for a blank string.

### HashToColorConverter\(float, float, Color?\) {#Aspid_MVVM_StarterKit_HashToColorConverter__ctor_System_Single_System_Single_System_Nullable_UnityEngine_Color__}

```csharp
public HashToColorConverter(float saturation, float value = 0.9, Color? fallback = null)
```

#### Parameters

`saturation` [float](https://learn.microsoft.com/dotnet/api/system.single)

The saturation of the produced color.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The brightness of the produced color.

`fallback` Color?

Used for a blank string. When omitted, gray.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">saturation</code> or <code class="paramref">value</code> is outside 0..1.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_HashToColorConverter_Convert_System_String_}

Derives a color from the specified string.

```csharp
public Color Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to hash.

#### Returns

 Color

The derived color, or the fallback for a blank string.

