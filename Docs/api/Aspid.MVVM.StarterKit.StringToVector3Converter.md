---
title: "Class StringToVector3Converter"
sidebar_label: "StringToVector3Converter"
description: "Class StringToVector3Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToVector3Converter {#Aspid_MVVM_StarterKit_StringToVector3Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a 3D vector out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Vector", Name = "Parse Vector3", Tooltip = "Reads a 3D vector out of text")]
public sealed class StringToVector3Converter : ITwoWayConverter<string?, Vector3>, IConverter<string?, Vector3>, IConverter, ISerializationCallbackReceiver
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToVector3Converter](Aspid.MVVM.StarterKit.StringToVector3Converter.md)

#### Implements

[ITwoWayConverter\<string?, Vector3\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
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

### StringToVector3Converter\(\) {#Aspid_MVVM_StarterKit_StringToVector3Converter__ctor}

```csharp
public StringToVector3Converter()
```

#### Remarks

Default: reading comma-separated text.

### StringToVector3Converter\(string, Vector3?, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToVector3Converter__ctor_System_String_System_Nullable_UnityEngine_Vector3__Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringToVector3Converter(string separator, Vector3? fallback = null, CultureInfoMode culture = CultureInfoMode.InvariantCulture)
```

#### Parameters

`separator` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed between the components. Empty stands for a comma.

`fallback` Vector3?

Returned when the text is not a vector. When omitted, a zero vector.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the components are read and written with. Falls back to invariant when its decimal separator is the separator.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToVector3Converter_Convert_System_String_}

Reads a vector out of the specified text.

```csharp
public Vector3 Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 Vector3

The vector, or the fallback when the text is not one.

### ConvertBack\(Vector3\) {#Aspid_MVVM_StarterKit_StringToVector3Converter_ConvertBack_UnityEngine_Vector3_}

Writes the specified vector as text.

```csharp
public string ConvertBack(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The three components with the separator between them, without brackets.

