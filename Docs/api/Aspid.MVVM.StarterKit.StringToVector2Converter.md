---
title: "Class StringToVector2Converter"
sidebar_label: "StringToVector2Converter"
description: "Class StringToVector2Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToVector2Converter {#Aspid_MVVM_StarterKit_StringToVector2Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a 2D vector out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Vector", Name = "Parse Vector2", Tooltip = "Reads a 2D vector out of text")]
public sealed class StringToVector2Converter : ITwoWayConverter<string?, Vector2>, IConverter<string?, Vector2>, IConverter, ISerializationCallbackReceiver
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToVector2Converter](Aspid.MVVM.StarterKit.StringToVector2Converter.md)

#### Implements

[ITwoWayConverter\<string?, Vector2\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md), 
ISerializationCallbackReceiver


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Reads back what a vector's own [`ToString`](https://learn.microsoft.com/dotnet/api/system.object.tostring) writes, brackets and all.

## Constructors

### StringToVector2Converter\(\) {#Aspid_MVVM_StarterKit_StringToVector2Converter__ctor}

```csharp
public StringToVector2Converter()
```

#### Remarks

Default: reading comma-separated text.

### StringToVector2Converter\(string, Vector2?, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToVector2Converter__ctor_System_String_System_Nullable_UnityEngine_Vector2__Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringToVector2Converter(string separator, Vector2? fallback = null, CultureInfoMode culture = CultureInfoMode.InvariantCulture)
```

#### Parameters

`separator` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed between the components. Empty stands for a comma.

`fallback` Vector2?

Returned when the text is not a vector. When omitted, a zero vector.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the components are read and written with. Falls back to invariant when its decimal separator is the separator.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToVector2Converter_Convert_System_String_}

Reads a vector out of the specified text.

```csharp
public Vector2 Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 Vector2

The vector, or the fallback when the text is not one.

### ConvertBack\(Vector2\) {#Aspid_MVVM_StarterKit_StringToVector2Converter_ConvertBack_UnityEngine_Vector2_}

Writes the specified vector as text.

```csharp
public string ConvertBack(Vector2 value)
```

#### Parameters

`value` Vector2

The vector to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The two components with the separator between them, without brackets.

